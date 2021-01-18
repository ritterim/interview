using RitterIM.Interview.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RitterIM.Interview
{
    public static class Extensions
    {
        public static string NewestActiveRegistered(this List<Person> people)
        {
            var latestPerson = people.Where(x => x.isActive == true)
                .OrderByDescending(x => x.registered)
                .FirstOrDefault();

            return $"{latestPerson.name.first} {latestPerson.name.last}";
        }
        public static void FavoriteFruitsCount(this List<Person> people)
        {
            foreach (var person in people.GroupBy(x => x.favoriteFruit)
                .Select(g => new
                {
                    Fruit = g.Key,
                    Count = g.Count()
                }))
            {
                Console.WriteLine($"{person.Fruit} has a total of {person.Count}");
            }
        }
        public static decimal TotalBalance(this List<Person> people)
        {
            decimal total = 0;
            people.Select(x => x.balance)
                .ToList()
                .ForEach(x =>
                    total += decimal.Parse(x, NumberStyles.Currency)
                );
            return total;
        }
        public static string CommonEyeColor(this List<Person> people)
        {
            return people.GroupBy(g => g.eyeColor)
                .Select(g => new
                {
                    EyeColor = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault()
                .EyeColor;
        }
        public static string FindPersonById(this List<Person> people, string id)
        {
            var person = people.Where(x => x.id == id).FirstOrDefault();
            return $"{person.name.last}, {person.name.first}";
        }
    }
}
