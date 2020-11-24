using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RIMdevInterview.Objects;

namespace RIMdevInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Prompt for the path of the JSON file to process
            var path = GetDataPath();

            //  If the file is there, then process it, otherwise exit with an error
            if (!FoundJsonFile(ref path))
            {
                Exit("File does not exist in that folder!");
            }
            else
            {
                ProcessFile(path);
            }

        }

        /// <summary>
        /// GetDataPath gets the path of the json file through which we'll be parcing 
        /// </summary>
        /// <returns>String path of the file</returns>
        private static String GetDataPath()
        {
            //  Prompt for the folder
            Console.Write("In what folder is your data.json file? ");
            return Console.ReadLine();
        }

        /// <summary>
        /// FoundJsonFile looks for a data.jason file in the folder entered in GetDataPath
        /// </summary>
        /// <param name="pathToFile">Path to the filename, passed in by reference so it can be updated</param>
        /// <returns>true - found the file; false - didn't find the file</returns>
        private static bool FoundJsonFile(ref String pathToFile)
        {
            //  Just in case an extra \ got put on the end of the folder name
            pathToFile += "\\data.json";
            pathToFile.Replace("\\\\", "\\");
            return File.Exists(pathToFile);
        }

        /// <summary>
        /// Exit: This method exits with a message
        /// </summary>
        /// <param name="message">Exit message</param>
        private static void Exit(String message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// ProcessFile does all the processing.  Wanted to keep main clean.  This performs the required tasks
        /// </summary>
        /// <param name="pathToFIle">Path of the JSON file we're processing</param>
        private static void ProcessFile(String pathToFIle)
        {
            //  List of clients to iterate through from the JSON file
            var clientList = new List<Client>();
            try
            {
                //  Using Newtonsoft to deserialize the JSON
                string json = File.ReadAllText(pathToFIle);
                clientList = JsonConvert.DeserializeObject<List<Client>>(json);
            }
            catch (Exception e)
            {
                Exit(e.ToString());
            }

            int overFifty = 0;
            var eyeColor = new Hashtable();
            var favoriteFruit = new Hashtable();
            int winningNumEyecolor = 0;
            Decimal runningBalance = 0;
            var registeredAndActive = DateTime.MinValue;
            var nameOfMostRecentRegistered = new FullName();

            //  Only iterate through the client list once to save processing time
            foreach (Client cl in clientList)
            {
                //  Objective:  What is the count of individuals over the age of 50?
                if (cl.Age > 50)
                {
                    overFifty++;
                }

                //  Objective: What is the full name of the individual with the id of 5aabbca3e58dc67745d720b1 in the format of lastname, firstname?
                if (cl.Id.Equals("5aabbca3e58dc67745d720b1"))
                {
                    Console.WriteLine("The client with ID 5aabbca3e58dc67745d720b1 is {0}, {1}", cl.Name.Last, cl.Name.First);
                }

                //  Objective:  What is the most common eye color?
                //  Used a hashtable for speed
                if (!eyeColor.ContainsKey(cl.EyeColor))
                {
                    eyeColor[cl.EyeColor] = 1;
                    if (winningNumEyecolor == 0)
                    {
                        winningNumEyecolor++;
                    }
                }
                else
                {
                    int old = (int)eyeColor[cl.EyeColor];
                    eyeColor[cl.EyeColor] = old + 1;
                    if ((int)eyeColor[cl.EyeColor] > winningNumEyecolor)
                    {
                        winningNumEyecolor = (int)eyeColor[cl.EyeColor];
                    }
                }

                //  Objective:  What are the counts of each favorite fruit?
                //  Again using a hashtable for speed
                if (!favoriteFruit.ContainsKey(cl.FavoriteFruit))
                {
                    favoriteFruit[cl.FavoriteFruit] = 1;
                }
                else
                {
                    int old = (int)favoriteFruit[cl.FavoriteFruit];
                    favoriteFruit[cl.FavoriteFruit] = old + 1;
                }

                //  Obnjective: What is the total balance of all individuals combined?
                var trimmedBalance = cl.Balance.Trim(new Char[] { ' ', ',', '$' });
                //  Trim the string for processing and then tally up the total balance
                runningBalance += Decimal.Parse(trimmedBalance,
                    NumberStyles.AllowThousands |
                    NumberStyles.AllowDecimalPoint |
                    NumberStyles.AllowCurrencySymbol);

                //  Objective:  Who is last individual that registered who is still active ?
                if (cl.IsActive && cl.Registered > registeredAndActive)
                {
                    nameOfMostRecentRegistered = cl.Name;
                }
            }

            //  Write the results to the console line and wait for input so the user has time to read the results
            Console.WriteLine("The number of clients over fifty is {0}", overFifty.ToString());
            Console.WriteLine("The total balance is ${0}", String.Format("{0:n}", runningBalance));
            Console.WriteLine("The person who is active and registered most recently is {0} {1}", nameOfMostRecentRegistered.First, nameOfMostRecentRegistered.Last);
            WriteMostCommonEyeColor(winningNumEyecolor, eyeColor);
            WriteCountsOfFavoriteFruits(favoriteFruit);
            Exit("\nThis completes the interview code challenge output.  It was fun!");
        }

        /// <summary>
        /// WriteMostCommonEyeColor prints the most common eye color(s) to the console window
        /// </summary>
        /// <param name="numOfMostCommon">THe number of occurances of the most common eyecolor</param>
        /// <param name="eyeColors">Hashtable containing all the eye colors</param>
        private static void WriteMostCommonEyeColor(int numOfMostCommon, Hashtable eyeColors)
        {
            //  In case there are more than one
            int howManyMostCommon = 0;
            var output = "The most common eye colors are ";
            
            //  In case there's one
            String colorMostCommon = "";

            //  Iterate through the hastable
            foreach (DictionaryEntry eyeColor in eyeColors)
            {
                if ((int)eyeColor.Value == numOfMostCommon)
                {
                    howManyMostCommon++;
                    colorMostCommon = eyeColor.Key.ToString();
                }
            }
            //  We have one most common eye color
            if (howManyMostCommon == 1)
            {
                Console.WriteLine("The most common eye color is: {0}", colorMostCommon);
            }
            //  We have a tie for the most common eye color
            else
            {
                foreach (DictionaryEntry eyeColor in eyeColors)
                {

                    if ((int)eyeColor.Value == numOfMostCommon)
                    {
                        output += eyeColor.Key.ToString();
                        if (--howManyMostCommon > 0)
                        {
                            output += ",";
                        }
                        output += " ";
                    }
                }
                Console.WriteLine(output);
            }
        }

        /// <summary>
        /// WriteCountsOfFavoriteFruits prints the fruit list to the console.
        /// </summary>
        /// <param name="favoriteFruits">Hashtable of favorite fruits</param>
        private static void WriteCountsOfFavoriteFruits(Hashtable favoriteFruits)
        {
            Console.WriteLine("Favorite fruit list:");
            foreach (DictionaryEntry favFruit in favoriteFruits)
            {
                Console.WriteLine("{0}: {1}", favFruit.Key.ToString(), favFruit.Value.ToString());
            }
        }

    }

}