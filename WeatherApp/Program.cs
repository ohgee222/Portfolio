Console.WriteLine("hellooooo");
//using System.Net.Http;
//using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;

//namespace WeatherApp
//{
//    class Program
//    {
//        // 🔑 REPLACE THIS WITH YOUR API KEY
//        private const string API_KEY = "api key";
//        private const string BASE_URL = "https://api.openweathermap.org/data/2.5/weather";

//        static async Task Main(string[] args)
//        {
//            Console.WriteLine("=== Weather App ===\n");

//            while (true)
//            {
//                Console.Write("Enter city name (or 'exit' to quit): ");
//                string city = Console.ReadLine();

//                if (string.IsNullOrWhiteSpace(city))
//                {
//                    Console.WriteLine("❌ Please enter a valid city name.\n");
//                    continue;
//                }

//                if (city.ToLower() == "exit")
//                {
//                    Console.WriteLine("Goodbye! ☀️");
//                    break;
//                }

//                await GetWeatherAsync(city);
//                Console.WriteLine();
//            }
//        }

//        static async Task GetWeatherAsync(string city)
//        {
//            try
//            {
//                using (HttpClient client = new HttpClient())
//                {
//                    // Build the API URL
//                    string url = $"{BASE_URL}?q={city}&appid={API_KEY}&units=metric";

//                    // Make the API request
//                    HttpResponseMessage response = await client.GetAsync(url);

//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Parse the JSON response
//                        string jsonResponse = await response.Content.ReadAsStringAsync();
//                        JObject data = JObject.Parse(jsonResponse);

//                        // Extract weather data
//                        string cityName = data["name"].ToString();
//                        string country = data["sys"]["country"].ToString();
//                        double temp = (double)data["main"]["temp"];
//                        double feelsLike = (double)data["main"]["feels_like"];
//                        string description = data["weather"][0]["description"].ToString();
//                        int humidity = (int)data["main"]["humidity"];
//                        double windSpeed = (double)data["wind"]["speed"];

//                        // Display weather information
//                        Console.WriteLine($"\n🌤️  Weather in {cityName}, {country}");
//                        Console.WriteLine($"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
//                        Console.WriteLine($"Temperature: {temp}°C (feels like {feelsLike}°C)");
//                        Console.WriteLine($"Condition: {CapitalizeFirstLetter(description)}");
//                        Console.WriteLine($"Humidity: {humidity}%");
//                        Console.WriteLine($"Wind Speed: {windSpeed} m/s");
//                    }
//                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
//                    {
//                        Console.WriteLine($"❌ City '{city}' not found. Please check the spelling.");
//                    }
//                    else
//                    {
//                        Console.WriteLine($"❌ Error: {response.StatusCode}");
//                    }
//                }
//            }
//            catch (HttpRequestException)
//            {
//                Console.WriteLine("❌ Network error. Please check your internet connection.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"❌ An error occurred: {ex.Message}");
//            }
//        }

//        static string CapitalizeFirstLetter(string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return text;

//            return char.ToUpper(text[0]) + text.Substring(1);
//        }
//    }
//}
 
 
