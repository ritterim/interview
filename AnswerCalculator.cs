using RimDevInterview.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimDevInterview
{
  class AnswerCalculator
  {
    public static int GetNumberOver50(List<Person> people) {
      return (from p in people where p.Age > 50 select p).Count();
    }

    public static string GetLastRegisteredActivePerson(List<Person> people) {
      return (from p in people where p.IsActive orderby p.Registered descending select p.Name.FullName).FirstOrDefault();
    }

    // Referenced: https://docs.microsoft.com/en-us/dotnet/csharp/linq/group-query-results
    public static string GetFruitGroups(List<Person> people) {
      var fruitGroups  = from p in people group p by p.FavoriteFruit into g orderby g.Key select g;
      
      var fruitGroupsString = string.Empty;
      foreach (var fg in fruitGroups) {
        fruitGroupsString += $"{fg.Key}: {fg.Count()}\n";
      }

      return fruitGroupsString;
    }

    // Referenced: https://stackoverflow.com/questions/7720747/how-do-i-select-the-value-that-occurs-most-frequently-in-queue-via-linq
    public static string GetMostCommonEyeColor(List<Person> people) {
      return (from p in people group p by p.EyeColor into g orderby g.Count() descending select g.Key).FirstOrDefault();
    }

    public static string GetTotalBalance(List<Person> people) {
      var total = people.Sum(p => p.BalanceAsDecimal);
      return $"${total:#,#.##}";
    }

    public static string GetFullNameForId(List<Person> people) {
      return people.FirstOrDefault(p => p.ID == "5aabbca3e58dc67745d720b1").Name.FullNameLastFirst;
    }
  }
}
