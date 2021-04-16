using AccuWeatherConsoleApp.ViewModels;
using System;

namespace AccuWeatherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherViewModel vm = new();
            vm.Run().Wait();
        }
    }
}
