namespace XamarinWeather
{
	using System.Threading.Tasks;
	using OpenWeatherMapClient;
	using Xamarin.Forms;

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await Run();
		}

		private async Task Run()
		{
			Weather.Text = "";

			var city = "Lincoln, NE";
			var client = new OpenWeatherMapClient();
			var weather = await client.GetCurrentWeatherByCityAsync(city);

			Weather.Text += $"Temp: {weather?.Main?.Temperature}\n";
			Weather.Text += $"Low: {weather?.Main?.MinTemperature}\n";
			Weather.Text += $"High: {weather?.Main?.MaxTemperature}\n";
			Icon.Source = weather.WeatherIconUrl();
		}
	}
}