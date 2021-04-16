using AccuWeatherConsole.Models;
using AccuWeatherConsole.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccuWeatherConsoleApp.ViewModels
{
    public class WeatherViewModel
    {

        public string Query { get; set; }

        public CurrentConditions CurrentConditions { get; set; }

        public City SelectedCity { get; set; }

        public List<City> Cities { get; set; } = new List<City>();

        public WeatherViewModel()
        {

        }

        public async Task Run()
        {
            bool flag = false;
            while (!flag)
            {
                Console.Clear();
                DisplayHeaderMessage("Enter the name of the city");
                Query = Console.ReadLine();
                Console.WriteLine(new string('-', Query.Length));

                await MakeQueryAsync();
                DisplayCityList(Cities);
                bool exitWhileLoop = false;

                while (!exitWhileLoop)
                {
                    DisplayHeaderMessage("Enter the number associated with the city");


                    if (int.TryParse(Console.ReadLine(), out int value) && value >= 1 && value <= Cities.Count)
                    {
                        SelectedCity = Cities[value - 1];
                        await GetCurrentConditionsAsync();
                        DisplayResult();
                        exitWhileLoop = true;
                    }
                    else
                    {
                        DisplayErrorMessage($"Please enter a value between {1} to {Cities.Count}");
                    }
                }

                DisplayHeaderMessage("Press any key to continue or press space-bar key to exit");

                if (Console.ReadKey().Key ==  ConsoleKey.Spacebar)
                {
                    Environment.Exit(0);
                }
            }
        }
        
        private static void DisplayHeaderMessage(string msg)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
            Console.ResetColor();
            Console.WriteLine(new string('-', msg.Length));
            Console.WriteLine();
        }

        private static void DisplayErrorMessage(string error)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(error);
            Console.ResetColor();
            Console.WriteLine(new string('-', error.Length));
            Console.WriteLine();
        }

        private void DisplayResult()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"{SelectedCity.LocalizedName} - {CurrentConditions.Temperature.Metric.Value}°C");
            Console.WriteLine($"{CurrentConditions.WeatherText}");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void DisplayCityList(List<City> cities)
        {
            int count = 1;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var city in cities)
            {
                Console.WriteLine($"{count} - {city.LocalizedName} [{city.Country.LocalizedName}]");
                count++;
            }
            Console.WriteLine();
            Console.ResetColor();
        }


        private async Task GetCurrentConditionsAsync()
        {            
            Query = string.Empty;
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
            Cities.Clear();
        }
        public async Task MakeQueryAsync()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
            Cities.Clear();
            cities.ForEach(c => Cities.Add(c));
        }

    }
}
