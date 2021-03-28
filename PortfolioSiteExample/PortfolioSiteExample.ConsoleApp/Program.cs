using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PortfolioSiteExample.ConsoleApp.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PortfolioSiteExample.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = InitializeSettings();
            var records = LoadRecords(settings.DataFileName);      
            

            var overAge50 = records.Where(x => x.age > 50);
            Console.WriteLine("What is the count of individuals over the age of 50?");
            Console.WriteLine($"{overAge50.Count()}");
            Console.WriteLine();


            var lastRegisteredActive = records.OrderByDescending(x => x.RegisteredAsDateTime).First(x => x.isActive);
            Console.WriteLine("Who is last individual that registered who is still active?");
            Console.WriteLine($"{lastRegisteredActive.name.last}, {lastRegisteredActive.name.first}");
            Console.WriteLine();


            Console.WriteLine("What are the counts of each favorite fruit?");
            var distinctFavoriteFruits = records.Select(x => x.favoriteFruit).Distinct().ToList();
            foreach (var favoriteFruit in distinctFavoriteFruits)
            {
                Console.WriteLine($"{favoriteFruit}: {records.Count(x => x.favoriteFruit == favoriteFruit)}");
            }

            Console.WriteLine();


            var mostCommonEyeColor = records.GroupBy(x => x.eyeColor)
                                            .Select(group => new {
                                                EyeColor = group.Key,
                                                Count = group.Count()
                                            })
                                            .OrderByDescending(x => x.Count)
                                            .First().EyeColor;
            Console.WriteLine("What is the most common eye color?");
            Console.WriteLine($"{mostCommonEyeColor}");
            Console.WriteLine();


            var totalBalance = records.Sum(x => x.BalanceAsDecimal);
            Console.WriteLine("What is the total balance of all individuals combined?");
            Console.WriteLine($"{totalBalance.ToString("c")}");
            Console.WriteLine();


            var uniqueIndividual = records.First(x => x.id == "5aabbca3e58dc67745d720b1");
            Console.WriteLine("What is the full name of the individual with the id of 5aabbca3e58dc67745d720b1 in the format of lastname, firstname?");
            Console.WriteLine($"{uniqueIndividual.name.last}, {uniqueIndividual.name.first}");
            Console.WriteLine();
        }

        private static Settings InitializeSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var settings = new Settings();
            ConfigurationBinder.Bind(configuration.GetSection("Settings"), settings);

            return settings;
        }

        private static List<Record> LoadRecords(string dataFileName)
        {
            string rawJson = System.IO.File.ReadAllText(dataFileName);
            return JsonConvert.DeserializeObject<List<Record>>(rawJson);
        }
    }
}
