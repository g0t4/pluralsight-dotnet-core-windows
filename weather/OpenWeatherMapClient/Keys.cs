namespace OpenWeatherMapClient
{
	using System;

	public static class Keys
	{
		public const string OpenWeatherMapApiKey = "";

		public static void ThrowIfKeysNotSet()
		{
			if (string.IsNullOrWhiteSpace(OpenWeatherMapApiKey))
			{
				throw new ArgumentException("You need to add your API keys");
			}
		}
	}
}
