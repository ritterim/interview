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
            var result = File.ReadAllText("../../data.json");

            var stuff = JsonConvert.DeserializeObject<ICollection<CustomerDto>>(result);


            Console.ReadKey();
        }
    }
}
