namespace OpenWeatherMapClient
{
	using System.Net.Http;
	using System.Runtime.Serialization.Json;
	using System.Threading.Tasks;

	public class OpenWeatherMapClient
	{
		private readonly string _Units;
		private readonly HttpClient _Client;
		private const string ApiRoot = "http://api.openweathermap.org/data/2.5";

		public OpenWeatherMapClient(string units = "imperial")
		{
			_Units = units;
			_Client = new HttpClient();
		}

		public Task<CurrentWeather> GetCurrentWeatherByCityAsync(string city)
		{
			var uri = $"{ApiRoot}/weather?q={city}";
			return RequestData<CurrentWeather>(uri);
		}

		public Task<CurrentWeather> GetCurrentWeatherAsync(double latitude, double longitude)
		{
			var uri = $"{ApiRoot}/weather?lat={latitude}&lon={longitude}";
			return RequestData<CurrentWeather>(uri);
		}

		public Task<Forecast> GetSevenDayForecastAsync(double latitude, double longitude)
		{
			var uri = $"{ApiRoot}/forecast/daily?cnt=7&lat={latitude}&lon={longitude}";
			return RequestData<Forecast>(uri);
		}

		private async Task<TResponse> RequestData<TResponse>(string uri) where TResponse : class
		{
			var uriWithKey = uri + $"&appid={Keys.OpenWeatherMapApiKey}&units={_Units}";
			var response = await _Client.GetAsync(uriWithKey);
			response.EnsureSuccessStatusCode();

			//var responseString = await response.Content.ReadAsStringAsync();
			//Console.WriteLine("\nJSON response:");
			//todo logging in UWP vs Console - Console.WriteLine(responseString);

			var serializer = new DataContractJsonSerializer(typeof(TResponse));
			return serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as TResponse;
		}
	}
}