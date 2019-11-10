using System;
using System.Linq;
using System.Collections.Generic;

class PopulationCounter
{
    static void ReadInput(Dictionary<string, Dictionary<string, long>> countriesPopulationInfo)
    {
        string input;

        while ((input = Console.ReadLine()) != "report")
        {
            string[] inputParams = input.Split('|');

            string city = inputParams[0];
            string country = inputParams[1];
            int population = int.Parse(inputParams[2]);

            if (!countriesPopulationInfo.ContainsKey(country))
            {
                countriesPopulationInfo[country] = new Dictionary<string, long>();
            }

            if (!countriesPopulationInfo[country].ContainsKey(city))
            {
                countriesPopulationInfo[country][city] = 0;
            }

            countriesPopulationInfo[country][city] += population;
        }
    }

    static void PrintStatistics(Dictionary<string, Dictionary<string, long>> countriesPopulationInfo)
    {
        foreach (KeyValuePair<string, Dictionary<string, long>> countryInfo in countriesPopulationInfo.OrderByDescending(p => p.Value.Values.Sum()))
        {
            string country = countryInfo.Key;
            long countryPopulation = countryInfo.Value.Values.Sum();

            Console.WriteLine($"{country} (total population: {countryPopulation})");

            foreach (KeyValuePair<string, long> cityInfo in countryInfo.Value.OrderByDescending(city => city.Value))
            {
                string city = cityInfo.Key;
                long cityPopulation = cityInfo.Value;

                Console.WriteLine($"=>{city}: {cityPopulation}");
            }
        }
    }

    static void Main(string[] args)
    {
        Dictionary<string, Dictionary<string, long>> countriesPopulationInfo = new Dictionary<string, Dictionary<string, long>>();

        ReadInput(countriesPopulationInfo);
        PrintStatistics(countriesPopulationInfo);
    }
}