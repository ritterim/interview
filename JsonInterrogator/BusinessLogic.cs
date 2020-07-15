using System.Collections.Generic;
using System.Linq;
using JsonInterrogator.Models;

namespace JsonInterrogator
{
    public static class BusinessLogic
    {
        public static int GetCountOverAge50(this IEnumerable<Person> people)
        {
            return people.Where(x => x.Age > 50).Count();
        }
        public static Person GetLastActivePerson(this IEnumerable<Person> people)
        {
            return people.Last(x => x.IsActive);
        }
        public static IEnumerable<ReportViewModel> GetFruitReport(this IEnumerable<Person> people)
        {
            return people.GroupBy(x => x.FavoriteFruit).Select(x => new ReportViewModel(x.Key, x.Count()));
        }
        public static string GetCommonEyeColor(this IEnumerable<Person> people)
        {
            return people.GroupBy(x => x.EyeColor).Select(x => new { EyeColor = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count).First().EyeColor;
        }
        public static decimal GetTotalBalance(this IEnumerable<Person> people)
        {
            return people.Sum(x => x.ConvertedBalance);
        }
        public static string GetFullNameById(this IEnumerable<Person> people, string id)
        {
            return people.First(x => x.Id == id).Name.FullName;
        }
    }
}