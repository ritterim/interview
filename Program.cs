using Newtonsoft.Json;
using RimDevInterview.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RimDevInterview
{
  class Program
  {
    static void Main(string[] args) {
      var file = "data.json";

      // Import data.json into memory
      if (!File.Exists(file)) {
        Console.WriteLine($"File import failed. File '{file}' must exist in same directory as executable.");
        return;
      }
      Console.Write("Importing data.json...");
      var json = File.ReadAllText(file).ToString();
      Console.WriteLine("Done!");

      // Convert json to objects via Json.NET (https://www.newtonsoft.com/json)
      // Referenced: https://stackoverflow.com/questions/13204663/parsing-nested-json-objects-with-json-net
      Console.Write("Deserializing json...");
      var people = JsonConvert.DeserializeObject<List<Person>>(json);
      Console.WriteLine("Done!");
      Console.WriteLine();

      // Answer questions...
      Console.WriteLine("What is the count of individuals over the age of 50?");
      Console.WriteLine(AnswerCalculator.GetNumberOver50(people));
      Console.WriteLine();

      Console.WriteLine("Who is last individual that registered who is still active?");
      Console.WriteLine(AnswerCalculator.GetLastRegisteredActivePerson(people));
      Console.WriteLine();

      Console.WriteLine("What are the counts of each favorite fruit?");
      Console.WriteLine(AnswerCalculator.GetFruitGroups(people));
      Console.WriteLine();

      Console.WriteLine("What is the most common eye color?");
      Console.WriteLine(AnswerCalculator.GetMostCommonEyeColor(people));
      Console.WriteLine();

      Console.WriteLine("What is the total balance of all individuals combined?");
      Console.WriteLine(AnswerCalculator.GetTotalBalance(people));
      Console.WriteLine();

      Console.WriteLine("What is the full name of the individual with the id of 5aabbca3e58dc67745d720b1 in the format of lastname, firstname?");
      Console.WriteLine(AnswerCalculator.GetFullNameForId(people));
      Console.WriteLine();
    }
  }
}
