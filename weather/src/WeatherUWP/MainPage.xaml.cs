namespace WeatherUWP
{
	using System;
	using System.Linq;
	using Windows.Devices.Geolocation;
	using Windows.Foundation.Metadata;
	using Windows.Phone.Devices.Power;
	using Windows.Services.Maps;
	using Windows.UI.Xaml;
	using Windows.UI.Xaml.Controls;

	/// <summary>
	///     An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public WeatherViewModel Weather { get; set; }

		public MainPage()
		{
			InitializeComponent();
			Keys.ThrowIfKeysNotSet();
			MapService.ServiceToken = Keys.BingMapsToken;
			Weather = new WeatherViewModel();
			Loaded += OnLoaded;
		}

		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			var accessStatus = await Geolocator.RequestAccessAsync();
			if (accessStatus != GeolocationAccessStatus.Allowed)
			{
				Weather.Messages = $"Gelocation access was denied {accessStatus}";
				return;
			}
			var position = await new Geolocator().GetGeopositionAsync();
			var coordinate = position?.Coordinate;
			Weather.Coordinate = coordinate;
			GetWeather(coordinate?.Point?.Position);
		}

		private async void GetWeather(BasicGeoposition? position)
		{
			if (!position.HasValue)
			{
				Weather.Messages = "Can't find your location";
				return;
			}

			var hasApiContract = ApiInformation
				.IsApiContractPresent("Windows.Phone.PhoneContract", 1);

			Weather.Messages = hasApiContract
				? $"{Battery.GetDefault().RemainingDischargeTime.TotalHours:0.0} hours remaining"
				: "";

			var client = new OpenWeatherMapClient();
			var weather = await client.GetCurrentWeatherAsync(position.Value.Latitude, position.Value.Longitude);
			Weather.Current = new CurrentWeatherViewModel(weather);

			var forecast = await client.GetSevenDayForecastAsync(position.Value.Latitude, position.Value.Longitude);
			Weather.Forecast = forecast.List
				.Select(f => new ForecastViewModel(f))
				.ToList();
		}
	}
}