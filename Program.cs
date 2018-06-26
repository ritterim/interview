using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Threading.Tasks;
using interview.Models;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace interview
{
    class Program
    {
        /*
            INSTRUCTIONS: 
            *******************************************************
            * To run, download repo and restore packages (from VS/IDE or using `dotnet restore` in terminal) and build the application (VS/IDE build or if using .NET core CLI => `dotnet build` in terminal/console from project root).
            * 
            * Can run resulting interview.dll file from terminal using `dotnet interview.dll` or directory run with Debug through IDE
            * 
            * Requires .NET core runtime (project built using 2.1.200)
            *******************************************************

            TECHNICAL CHOICES:
            *******************************************************
            * Built using C# with .NET core. .NET core chosen for portability (project could be built and run regardless of OS as long as .NET core runtime is present) 
            * 
            * Used LINQ queries to query the data from data.json, created a model (best described as a data transfer object in this case) to get a strongly typed object 
            * from the file data.json
            * 
            * Since the Name object within data.json is only two properties, put both classes in the same file for brevity. Both inside interview.Models namespace
            * 
            * Used third-party NuGet package Newtownsoft.Json for deserializing data.json into an object
            *
            * When importing data.json file, assumes .dll/assembly is executing in bin/Debug/netcoreapp2.0 folder. Hence the file path manipulation in MainAsync()
            *
            * ImportJson and ConvertCurrencyToDecimal in own methods as they could be resused in multiple places. Other functionality felt too specific to necessitate its own methods
            *
            * ImportJson utilizes StreamReader.ReadToEndAsync() which meant calling inside an async method which must be awaited from caller. 
            * Because console app Main() method must not be async, moved all code to its own async method and .Wait() that to finish from the void Main() method.
            * 
            * Included links for StackOverflow posts that helped where necessary.
            *******************************************************
        */

        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        public static async Task MainAsync()
        {
            var currentDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var projectPath = Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\"));

            // #1
            Console.WriteLine("#1. Import data.json from file.");
            var jsonString = await ImportJson(projectPath, "data.json");
            var data = JsonConvert.DeserializeObject<List<Data>>(jsonString);

            // #2
            Console.WriteLine("#2. What is the count of individuals over the age of 50?");

            var overAgeFifty = data.Where(d => d.Age > 50)
                .Count();

            Console.WriteLine(overAgeFifty);

            // #3
            Console.WriteLine("#3. Who is last individual that registered who is still active?");

            // SOURCE: https://stackoverflow.com/questions/5200877/how-to-select-min-and-max-date-values-in-linq-query
            // had trouble finiding MAX Registered and order of methods chained in LINQ calls
            var ind = data.Where(d => d.IsActive == true)
                .Select(d => d)
                .OrderByDescending(d => Convert.ToDateTime(d.Registered))
                .FirstOrDefault();

            Console.WriteLine($"{ind.Name.First} {ind.Name.Last}");

            // #4
            Console.WriteLine("#4. What are the counts of each favorite fruit?");

            var countFruit = data.GroupBy(d => d.FavoriteFruit)
                .Select(d => new
                {
                    Fruit = d.Key,
                    Count = d.Select(x => x.FavoriteFruit).Count()
                }).OrderByDescending(d => d.Count);

            foreach (var item in countFruit)
            {
                Console.WriteLine($"{item.Fruit} {item.Count}");
            }

            // #5
            Console.WriteLine("#5. What is the most common eye color?");

            var countEyeColors = data.GroupBy(d => d.EyeColor)
                .Select(d => new
                {
                    Color = d.Key,
                    Count = d.Select(x => x.EyeColor).Count()
                }).OrderByDescending(d => d.Count).Take(1);

            foreach (var item in countEyeColors)
            {
                Console.WriteLine($"{item.Color} {item.Count}");
            }

            // #6
            Console.WriteLine("#6. What is the total balance of all individuals combined");

            var sum = data.Select(d => ConvertCurrencyToDecimal(d.Balance))
                .Sum();

            Console.WriteLine(sum);

            // #7
            Console.WriteLine("#7. What is the full name of the individual with the id of 5aabbca3e58dc67745d720b1 in the format of lastname, firstname?");

            var person = data.Where(d => d.Id == "5aabbca3e58dc67745d720b1")
                .Select(d => d.Name)
                .FirstOrDefault();

            Console.WriteLine($"{person.Last}, {person.First}");
        }

        public static async Task<string> ImportJson(string filePath, string fileName)
        {
            using (StreamReader streamReader = new StreamReader($"{filePath}\\{fileName}"))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        public static decimal ConvertCurrencyToDecimal(string amount)
        {
            // SOURCE: https://stackoverflow.com/questions/35792717/convert-currency-232-680-00-to-decimal
            // hadn't actually encountered currency conversion from C# before. usually work with that from client side (JavaScript) and store at rest as decimals/floats
            return Decimal.Parse(amount, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
        }
    }
}