using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace AccuWeatherConsole.Models.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";

        // Please grab your AccuWeather API key and paste the value of API_KEY variable

        public const string API_KEY = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";


        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}&details=false";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new();
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using(HttpClient client = new())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new();
            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }
            return currentConditions;
        }
    }
}
