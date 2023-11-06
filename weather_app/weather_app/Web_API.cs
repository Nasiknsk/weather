using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace weather_app
{
    internal class Web_API
    {
        public string[] GetCity(string city)
        {
            try
            {
                // Read JSON data from file
                string jsonData = File.ReadAllText("D:/C#/weather_app/post_api.json");

                // Replace placeholder with user input city
                jsonData = jsonData.Replace("{city}", city);

                // Parse JSON data
                JObject jsonDataObject = JObject.Parse(jsonData);

                // Extract URL information from JSON
                string apiUrl = (string)jsonDataObject["item"][0]["request"]["url"]["raw"];

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        JObject weatherData = JObject.Parse(jsonResponse);

                        string location = (string)weatherData["location"]["name"];
                        double temperature = (double)weatherData["current"]["temp_c"];
                        string country = (string)weatherData["location"]["country"];

                        string temp = temperature.ToString();

                        string[] data = new string[] { location, temp, country };
                        return data;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        string[] data = new string[] { "Please Insert Correct city" };
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                string[] data = new string[] { "An error occurred while fetching weather data" };
                return data;
            }
        }
    }
}