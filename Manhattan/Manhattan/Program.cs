using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Manhattan
{
    class Program
    {
        static void Main(string[] args)
        {
            JObject json;
            try
            {
                //Code taken from https://www.newtonsoft.com/json/help/html/ParsingLINQtoJSON.htm
                using (StreamReader reader = File.OpenText(@"../../../../../data/data.json"))
                {
                    json = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); ;
            }
            JArray data = (JArray)json["features"];
            List<Neighborhood> hoodList = new List<Neighborhood>();
            foreach(var thing in data)
            {
                hoodList.Add(new Neighborhood(thing["properties"]["neighborhood"].ToString()));
            }
            Console.WriteLine("Output all of the neighborhoods in this data list: ");
            IEnumerable<string> allHoodNames = hoodList.Select(hood => hood.Name);
            int counterOne = 1;
            foreach (string name in allHoodNames)
            {
                Console.WriteLine(counterOne + ": " + name);
                counterOne++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Filter out all the neighborhoods that do not have any names: ");
            IEnumerable<string> allHoodFilledNames = hoodList
                .Select(hood => hood.Name)
                .Where(name => name != "");
            int counterTwo = 1;
            foreach(string name in allHoodFilledNames)
            {
                Console.WriteLine(counterTwo + ": " + name);
                counterTwo++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Removed Duplicate names from the list: ");
            IEnumerable<string> allHoodDistinctNames = hoodList
                .Select(hood => hood.Name)
                .Where(name => name != "")
                .Distinct();
            int counterThree = 1;
            foreach (string name in allHoodDistinctNames)
            {
                Console.WriteLine(counterThree + ": " + name);
                counterThree++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Same as previous in one long query: ");
            IEnumerable<string> allHoodDistinctNamesAgain = hoodList
                .Select(hood => hood.Name)
                .Where(name => name != "")
                .Distinct();
            int counterFour = 1;
            foreach (string name in allHoodDistinctNamesAgain)
            {
                Console.WriteLine(counterFour + ": " + name);
                counterFour++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("All hood names (not distinct) query using Opposing: ");
            IEnumerable<string> allHoodNamesWithNewQueryNotDistinct =
                from hood in hoodList
                where hood.Name != ""
                select  hood.Name;
            int counterFive = 1;
            foreach (string name in allHoodNamesWithNewQueryNotDistinct)
            {
                Console.WriteLine(counterFive + ": " + name);
                counterFive++;
            }
        }
    }
}
