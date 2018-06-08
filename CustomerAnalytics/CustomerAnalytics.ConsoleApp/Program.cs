using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CustomerAnalytics.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonString = File.ReadAllText("../../data.json");

            var customerDtos = JsonConvert.DeserializeObject<ICollection<CustomerDto>>(jsonString);

            var customers = customerDtos.MapToCustomers();

            var analyser = new CustomerAnalyser();

            var results = new
            {
                CountOfCustomersOver50 = analyser.GetCountTheNumberOfPeopleOverAnAge(customers, 50),
                LastPersonToRegisterAndIsStillActive = analyser.GetNewestCustomerWhoIsStillActive(customers),
                FavoriteFruitCounts = analyser.CountOfEachFavoriteFruit(customers),
                MostCommonEyeColor = analyser.MostCommonEyeColor(customers),
                TotalBalance = analyser.CalculateTotalBalance(customers),
                PersonWhoWeLookedUp = analyser.GetUsersFullName(customers, "5aabbca3e58dc67745d720b1")
            };

            var resultsJson = JsonConvert.SerializeObject(results, Formatting.Indented);

            Console.WriteLine("Sorry about the poor formatting. I didn't want to have to write everything by hand.");
            Console.WriteLine();
            Console.WriteLine(resultsJson);

            File.Create("../../results.json");

            Console.WriteLine();
            Console.WriteLine("Please press any key to close out. The results should be saved in a json file in the root of your project.");
            Console.ReadKey();
        }
    }
}
