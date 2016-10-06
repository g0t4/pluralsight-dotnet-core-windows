namespace WeatherConsole
{
	using System;
	using System.Net.Http;
	using System.Runtime.Serialization.Json;
	using System.Threading.Tasks;

	public class OpenWeatherMapClient
	{
		public static string AppId = "1196acb7a410b707cc632377d33161c3";
		private readonly string _Units;
		private readonly HttpClient _Client;
		private const string ApiRoot = "http://api.openweathermap.org/data/2.5";

		public OpenWeatherMapClient(string units = "imperial")
		{
			_Units = units;
			_Client = new HttpClient();
		}

		public async Task<CurrentWeather> GetCurrentWeatherByCity(string city)
		{
			// note: no error handling
			var currentWeatherApiUrl = $"{ApiRoot}/weather?q={city}&appid={AppId}&units={_Units}";
			var response = await _Client.GetAsync(currentWeatherApiUrl);
			var responseString = await response.Content.ReadAsStringAsync();
			Console.WriteLine("\nJSON response:");
			Console.WriteLine(responseString);
			var serializer = new DataContractJsonSerializer(typeof(CurrentWeather));
			return serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as CurrentWeather;
		}
	}
}