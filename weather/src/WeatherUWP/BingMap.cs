
namespace OpenWeatherMapClient
{
	using System;

	public static class BingKeys
	{
		public const string BingMapsToken = "";

		public static void ThrowIfKeysNotSet()
		{
			if (string.IsNullOrWhiteSpace(BingMapsToken))
			{
				throw new ArgumentException("You need to add your API keys");
			}
		}
	}
}
