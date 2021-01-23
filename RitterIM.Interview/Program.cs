using Newtonsoft.Json;
using RitterIM.Interview.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RitterIM.Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people;
            using (StreamReader r = new StreamReader(@"..\..\..\..\data.json"))
            {
                var json = r.ReadToEnd();
                people = JsonConvert.DeserializeObject<List<Person>>(json);
            }

            Console.WriteLine($"The count of individuals over the age of 50 is {people.Where(x => x.age > 50).Count()}");
            Console.WriteLine($"The last active person to register was {people.NewestActiveRegistered()}");
            people.FavoriteFruitsCount();
            Console.WriteLine("The total balance of all individuals combined is " + people.TotalBalance().ToString("C"));
            Console.WriteLine($"The most common eye color is {people.CommonEyeColor()}");
            Console.WriteLine($"The person with the Id of 5aabbca3e58dc67745d720b1 is {people.FindPersonById("5aabbca3e58dc67745d720b1")}");

            Console.ReadLine();
        }

        
    }
    
}
