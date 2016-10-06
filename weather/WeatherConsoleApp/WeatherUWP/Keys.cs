namespace WeatherUWP
{
	using System;

	static class Keys
	{
		public const string OpenWeatherMapApiKey = "";
		public const string BingMapsToken = "";

		public static void ThrowIfKeysNotSet()
		{
			if (string.IsNullOrWhiteSpace(OpenWeatherMapApiKey)
				|| string.IsNullOrWhiteSpace(BingMapsToken))
			{
				throw new ArgumentException("You need to add your API keys");
			}
		}
	}
}
