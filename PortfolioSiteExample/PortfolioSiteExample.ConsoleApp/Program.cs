using Newtonsoft.Json;
using PortfolioSiteExample.ConsoleApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioSiteExample.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = JsonConvert.DeserializeObject<Example>("{\"ExampleId\": 1, \"Test\": \"Test123\"}");
            var list = new List<Example>();
            list.Add(result);
            var item = list.First();
            Console.WriteLine(item.Test);
        }
    }
}
