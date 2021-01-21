using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Interview
{
    class Program
    {
        public class UserInfo
        {
            [JsonProperty(PropertyName = "favoriteFruit")]
            public string FavFruit { get; set; }
            [JsonProperty(PropertyName = "greeting")]
            public string Greeting { get; set; }
            [JsonProperty(PropertyName = "longitude")]
            public double Longitude { get; set; }
            [JsonProperty(PropertyName = "latitude")]
            public double Latitude { get; set; }
            [JsonProperty(PropertyName = "registered")]
            public string Registered { get; set; }
            [JsonProperty(PropertyName = "address")]
            public string Address { get; set; }
            [JsonProperty(PropertyName = "phone")]
            public string Phone { get; set; }
            [JsonProperty(PropertyName = "email")]
            public string Email { get; set; }
            [JsonProperty(PropertyName = "company")]
            public string CompanyName { get; set; }
            [JsonProperty(PropertyName = "name")]
            public Name FullName { get; set; }
            [JsonProperty(PropertyName = "eyeColor")]
            public string EyeColor { get; set; }
            [JsonProperty(PropertyName = "age")]
            public int Age { get; set; }
            [JsonProperty(PropertyName = "balance")]
            public string Balance { get; set; }
            [JsonProperty(PropertyName = "isActive")]
            public bool IsActive { get; set; }
            [JsonProperty(PropertyName = "id")]
            public string ID { get; set; }
        }

        public class Name
        {
            [JsonProperty(PropertyName = "last")]
            public string Last { get; set; }
            [JsonProperty(PropertyName = "first")]
            public string First { get; set; }
        }

        static void Main(string[] args)
        {

            int overFifty = 0;
            string lastActive = "";
            Dictionary<string, int> favFruits = new Dictionary<string, int>();
            Dictionary<string, int> eyeColors = new Dictionary<string, int>();
            decimal totalBalance = 0;
            string vipName = "";

            JsonSerializer serializer = new JsonSerializer
            {
                //Formatting = Formatting.Indented
            };
            UserInfo ui;
            FileStream s = File.Open("data.json", FileMode.Open);
            StreamReader sr = new StreamReader(s);
            using (JsonReader reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    // deserialize only when there's "{" character in the stream
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        ui = serializer.Deserialize<UserInfo>(reader);

                        //Check for users over 50
                        if (ui.Age > 50)
                            overFifty++;

                        //Check for last active individual 
                        if (ui.IsActive)
                        {
                            lastActive = ui.FullName.First + " " + ui.FullName.Last;
                        }

                        //Count favorite fruit
                        if (!favFruits.ContainsKey(ui.FavFruit))
                        {
                            favFruits.Add(ui.FavFruit, 1);
                        }
                        else
                        {
                            favFruits.TryGetValue(ui.FavFruit, out int count);
                            favFruits[ui.FavFruit] = count + 1;
                        }

                        //Count for most common eye color
                        if (!eyeColors.ContainsKey(ui.EyeColor))
                        {
                            eyeColors.Add(ui.EyeColor, 1);
                        }
                        else
                        {
                            eyeColors.TryGetValue(ui.EyeColor, out int count);
                            eyeColors[ui.EyeColor] = count + 1;
                        }

                        //Total balance of individuals
                        totalBalance = totalBalance + decimal.Parse(ui.Balance, NumberStyles.Currency);

                        //Find user with specified id
                        if (ui.ID == "5aabbca3e58dc67745d720b1")
                        {
                            vipName = ui.FullName.Last + ", " + ui.FullName.First;
                        }
                    }
                }
            }

            //Figure most common eye color
            int max = 0;
            string commonColor = "";
            foreach (var color in eyeColors)
            {
                if (color.Value > max)
                {
                    commonColor = color.Key;
                }
            }

            //Display results
            Console.WriteLine("Individuals over 50: " + overFifty.ToString());
            Console.WriteLine("Last individual active: " + lastActive);

            foreach (var fruit in favFruits)
            {
                Console.WriteLine(fruit.Key + ": " + fruit.Value);
            }

            Console.WriteLine("Most common color: " + commonColor);
            Console.WriteLine("Total Balance: $" + totalBalance.ToString());
            Console.WriteLine("Individual with ID of `5aabbca3e58dc67745d720b1`: " + vipName);
            Console.ReadKey();
        }
    }
}
