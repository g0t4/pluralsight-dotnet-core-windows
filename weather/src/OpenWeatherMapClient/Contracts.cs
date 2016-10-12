namespace OpenWeatherMapClient
{
	using System.Runtime.Serialization;

	[DataContract]
	public class Forecast
	{
		[DataMember(Name = "city")]
		public City City { get; set; }

		[DataMember(Name = "list")]
		public DailyForecast[] List { get; set; }
	}


	/// <summary>
	///     Forecast information for a single day
	/// </summary>
	[DataContract]
	public class DailyForecast
	{
		/// <summary>
		///     Time of data forecasted.
		/// </summary>
		[DataMember(Name = "dt")]
		public int Date { get; set; }

		[DataMember(Name = "temp")]
		public TempInfo Temp { get; set; }

		[DataMember(Name = "pressure")]
		public double Pressure { get; set; }

		[DataMember(Name = "humidity")]
		public double Humidity { get; set; }

		[DataMember(Name = "weather")]
		public WeatherCondition[] Weathers { get; set; }

		/// <summary>
		/// Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
		/// </summary>
		[DataMember(Name = "speed")]
		public double WindSpeed { get; set; }

		/// <summary>
		/// Wind direction, degrees (meteorological)
		/// </summary>
		[DataMember(Name = "deg")]
		public double WindDirection { get; set; }

		/// <summary>
		/// Cloudiness %
		/// </summary>
		[DataMember(Name = "clouds")]
		public double Cloudiness { get; set; }

		/// <summary>
		/// Temperature Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
		/// </summary>
		[DataContract]
		public class TempInfo
		{
			[DataMember(Name = "day")]
			public double Day { get; set; }

			[DataMember(Name = "min")]
			public double Min { get; set; }

			[DataMember(Name = "max")]
			public double Max { get; set; }

			[DataMember(Name = "night")]
			public double Night { get; set; }

			[DataMember(Name = "eve")]
			public double Evening { get; set; }

			[DataMember(Name = "morn")]
			public double Morning { get; set; }
		}
	}

	[DataContract]
	public class City
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Country code i.e. US, GB
		/// </summary>
		[DataMember(Name = "country")]
		public string Country { get; set; }

		[DataMember(Name = "coord")]
		public Coordinates Coordinates { get; set; }
	}

	[DataContract]
	public class Coordinates
	{
		[DataMember(Name = "lat")]
		public string Latituide { get; set; }

		[DataMember(Name = "long")]
		public string Longitude { get; set; }
	}

	[DataContract]
	public class CurrentWeather
	{
		[DataMember(Name = "coord")]
		public Coordinates Coordinates { get; set; }

		[DataMember(Name = "weather")]
		public WeatherCondition[] Weathers { get; set; }

		/// <summary>
		/// Grab first element out of Weathers array, usually only one on current weather.
		/// </summary>
		public WeatherCondition FirstCondition => Weathers?[0];

		[DataMember(Name = "main")]
		public Main Main { get; set; }

		[DataMember(Name = "wind")]
		public WindInfo Wind { get; set; }

		[DataContract]
		public class WindInfo
		{
			/// <summary>
			///     Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
			/// </summary>
			[DataMember(Name = "speed")]
			public double WindSpeed { get; set; }

			/// <summary>
			///     Wind direction, degrees (meteorological)
			/// </summary>
			[DataMember(Name = "deg")]
			public double WindDirection { get; set; }
		}

		[DataMember(Name = "clouds")]
		public CloudsInfo Clouds { get; set; }

		[DataContract]
		public class CloudsInfo
		{
			/// <summary>
			///     % Cloudiness
			/// </summary>
			[DataMember(Name = "all")]
			public int Cloudiness { get; set; }
		}

		[DataMember(Name = "snow")]
		public VolumeInfo Snow { get; set; }

		[DataContract]
		public class VolumeInfo
		{
			[DataMember(Name = "3h")]
			public double LastThreeHours { get; set; }
		}

		[DataMember(Name = "rain")]
		public VolumeInfo Rain { get; set; }

		/// <summary>
		///     Time of day calculation, unix, UTC
		/// </summary>
		[DataMember(Name = "dt")]
		public int Date { get; set; }

		[DataMember(Name = "name")]
		public string CityName { get; set; }

		public string WeatherIconUrl() => Weathers?[0]?.IconUrl;
	}

	[DataContract]
	public class Main
	{
		[DataMember(Name = "temp")]
		public double Temperature { get; set; }

		/// <summary>
		/// Humidity percent
		/// </summary>
		[DataMember(Name = "humidity")]
		public int Humidity { get; set; }

		/// <summary>
		///     Minimum temperature at the moment. This is deviation from current temp that is possible for large cities and
		///     megalopolises geographically expanded (use these parameter optionally). Unit Default: Kelvin, Metric: Celsius,
		///     Imperial: Fahrenheit.
		/// </summary>
		[DataMember(Name = "temp_min")]
		public double MinTemperature { get; set; }

		/// <summary>
		///     Maximum temperature at the moment. This is deviation from current temp that is possible for large cities and
		///     megalopolises geographically expanded (use these parameter optionally). Unit Default: Kelvin, Metric: Celsius,
		///     Imperial: Fahrenheit.
		/// </summary>
		[DataMember(Name = "temp_max")]
		public double MaxTemperature { get; set; }

	}

	[DataContract]
	public class WeatherCondition
	{
		/// <summary>
		///     Weather condition id.
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; set; }


		/// <summary>
		///     Weather icon id
		/// </summary>
		[DataMember(Name = "icon")]
		public string Icon { get; set; }

		/// <summary>
		///     Group of weather parameters (Rain, Snow, Extreme etc.)
		/// </summary>
		[DataMember(Name = "main")]
		public string Main { get; set; }

		/// <summary>
		///     Weather condition within the group
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		///     Weather icon url
		/// </summary>
		public string IconUrl => $"http://openweathermap.org/img/w/{Icon}.png";
	}
}