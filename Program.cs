using System;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using interview.models;
using interview.services;

namespace interview
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();  
        }

        public static async Task MainAsync()
        {
            string currentDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()
                .Location);

            string path = Path.GetFullPath(Path.Combine(currentDir, @"../../../"));

            try
            {
                Console.WriteLine("#1. Try to import data.json from file.");
                string data = await ImportData(path, "data.json");

                if (!String.IsNullOrEmpty(data))
                {
                    Console.WriteLine("data.json successfully imported.");

                    var jsonData = JsonConvert.DeserializeObject<IEnumerable<Data>>(data);
                    DataService dataService = new DataService(jsonData);

                    Console.WriteLine("#2. What is the count of individuals over the age of 50?");
                    
                    var overFifty = dataService.GetIndividualsOverUnderAge(50, true);
                    Console.WriteLine(overFifty);

                    Console.WriteLine("#3. Who is last individual that registered who is still active?");
                    
                    var lastRegisteredActive = dataService.GetIndividuals(true)
                        .FirstOrDefault();

                    Console.WriteLine($"{lastRegisteredActive.Name.First} {lastRegisteredActive.Name.Last}");

                    Console.WriteLine("#4. What are the counts of each favorite fruit?");

                    var favoriteFruitCounts = dataService.GetCountsOfEachFavoriteFruit();
                    
                    foreach (var fruit in favoriteFruitCounts)
                    {
                        Console.WriteLine($"{fruit.Key}: {fruit.Value}");
                    }

                    Console.WriteLine("#5. What is the most common eye color?");

                    var mostCommonEyeColor = dataService.GetMostCommonEyeColor();
                    Console.WriteLine(mostCommonEyeColor);

                    Console.WriteLine("#6. What is the total balance of all individuals combined");

                    var totalBalance = dataService.GetTotalBalanceAllIndividuals();
                    Console.WriteLine($"${totalBalance}");

                    Console.WriteLine("#7. What is the full name of the individual with the id of 5aabbca3e58dc67745d720b1 in the format of lastname, firstname?");

                    var individual = dataService.GetIndividualById("5aabbca3e58dc67745d720b1");
                    Console.WriteLine($"{individual.Name.Last}, {individual.Name.First}");
                }

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<string> ImportData(string filePath, string fileName)
        {
            using (StreamReader streamReader = new StreamReader($"{filePath}//{fileName}"))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}
