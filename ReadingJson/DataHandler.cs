using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Elasticsearch.Net;
using Nest;

namespace ReadingJson
{
    /// Pushes JSON into elastic and provides some functionality to analyze the json.
    class DataHandler
    {
        private ElasticClient client;
        private string index = "people";

        public DataHandler() {
            var settings = new ConnectionSettings().DefaultIndex(index);
            client = new ElasticClient(settings);
        } 

        // Grabs the total number of people over the age of 50
        private String overFiftyAge() {
            var searchResponse = client.Search<Person>(s => s
                .Query(q => q
                    .Range(c => c
                        .Field(f => f.age)
                        .GreaterThan(50)
                    )
                )
            );

            return "Number of people over 50 years old: " + searchResponse.Total;
        }

        /// Returns the latest person that registered and is active
        private String registeredAndActive() {
            var searchResponse = client.Search<Person>(s => s
                .Query(q => q
                    .Term(p => p.isActive, true)
                )
                .Sort(ss => ss 
                .Descending(d => d.registered)
                )
                .Size(1)
            );

            String latestRegistered = "";
            // Will only get one hit back
            foreach (var doc in searchResponse.Documents) {
                latestRegistered = "The last individual registered and still active is: " + doc.name;
            }

            return latestRegistered;
        }

        /// Count of each favorite fruit
        private String countFruit() {
            var appleCountResponse = client.Search<Person>(s => s
                .Query(q => q
                    .Term(p => p.favoriteFruit, "apple")
                )
            );

            var bananaCountResponse = client.Search<Person>(s => s
                .Query(q => q
                    .Term(p => p.favoriteFruit, "banana")
                )
            );

            var strawberryCountResponse = client.Search<Person>(s => s
                .Query(q => q
                    .Term(p => p.favoriteFruit, "strawberry")
                )
            );

            return "Apple count is: " + appleCountResponse.Total + "\nBanana count is: " + bananaCountResponse.Total + "\nStrawberry count is: " + strawberryCountResponse.Total;
        }

        /// Grabs the most common eye color
        private String mostCommonEyeColor() {
            var searchResponse = client.Search<Person>(s => s
                .Aggregations(a => a
                    .Terms("eyeColor", st => st
                    .Field(p => p.eyeColor.Suffix("keyword"))
                    )
                )
            );
            var eyeColors = searchResponse.Aggregations.Terms("eyeColor");

            string mostCommonEyeColor = "";
            long? maxValue = 0;

            // Go through each eye color and keep the most common one
            foreach (var item in eyeColors.Buckets) {
                if (item.DocCount > maxValue) {
                    maxValue = item.DocCount;
                    mostCommonEyeColor = "The most common eye color is " + item.Key + " with " + item.DocCount;
                }
            }

            return mostCommonEyeColor;
        }


        /// Get the total balance of each person
        private string totalbalance() {
            var searchResponse = client.Search<Person>(s => s
                .Query(q => q
                    .MatchAll(m => m
                    )
                )
            );
        
            double totalbalance = 0;

            // Go through each doc, remove the '$', convert to a double and add it up
            foreach(var doc in searchResponse.Documents){ 
                totalbalance += Convert.ToDouble(doc.balance.Remove(0, 1));
            }

            return "Total Balance: $" + String.Format("{0:.00}", totalbalance);
        }

        /// Gets the name of a person with a given Id
        private String getName(String id) {
            Name name = null;
            var searchResponse = client.Search<Person>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.id)
                        .Query(id)
                    )
                )
            );

            // We should only ever have one hit here.
            foreach (var person in searchResponse.Documents) {
                name = person.name;
            }

            return "Name of person with id=" + id + " is: " + name;
        }

        /// Pushes data into elastic
        private void IndexIntoElastic(List<Person> people) {
            foreach (Person person in people) {
                var indexResponse = client.LowLevel.Index<BytesResponse>(index, person.id, PostData.Serializable(person));
            }
        }

        static void Main(string[] args) {
            string fullPath = Path.GetFullPath("../data.json");
            if (File.Exists(fullPath)) {
                // Read the json into memory
                List<Person> personArray = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(fullPath));

                // Index the data into Elasticsearch
                DataHandler dataHandler = new DataHandler();
                dataHandler.IndexIntoElastic(personArray);

                // Requested analysis
                Console.WriteLine(dataHandler.overFiftyAge());
                Console.WriteLine(dataHandler.registeredAndActive());
                Console.WriteLine(dataHandler.countFruit());
                Console.WriteLine(dataHandler.mostCommonEyeColor());
                Console.WriteLine(dataHandler.totalbalance());
                Console.WriteLine(dataHandler.getName("5aabbca3e58dc67745d720b1"));

            }
            else {
                Console.WriteLine("Error: Couldn't find file: " + fullPath);
            }
        }
    }
}
