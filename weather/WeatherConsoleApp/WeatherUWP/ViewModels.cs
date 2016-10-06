namespace WeatherUWP
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using Windows.Devices.Geolocation;

	public class WeatherViewModel : ViewModelBase
	{
		private string _Messages;
		private Geocoordinate _Coordinate;
		private List<ForecastViewModel> _Forecast;
		private CurrentWeatherViewModel _Current;
		private bool _IsLoading;

		public string Messages
		{
			get { return _Messages; }
			set
			{
				_Messages = value;
				RaisePropertyChanged();
			}
		}

		public Geocoordinate Coordinate 
		{
			get { return _Coordinate; }
			set
			{
				_Coordinate = value;
				RaisePropertyChanged();
			}
		}

		public CurrentWeatherViewModel Current
		{
			get { return _Current; }
			set
			{
				_Current = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(IsLoading));
			}
		}

		public List<ForecastViewModel> Forecast
		{
			get { return _Forecast; }
			set
			{
				_Forecast = value;
				RaisePropertyChanged();
			}
		}

		public bool IsLoading => _Current == null;
	}

	public class ForecastViewModel
	{
		public ForecastViewModel(DailyForecast forecast)
		{
			var time = GetTimeFromUnixTime(forecast.Date);
			Day = $"{time:ddd MM/dd}";

			var max = forecast.Temp.Max;
			var min = forecast.Temp.Min;
			Temperature = $"{max:0}° / {min:0}°";

			IconUrl = forecast.Weathers[0]?.IconUrl;
		}

		public string Day { get; set; }
		public string Temperature { get; set; }
		public string IconUrl { get; set; }

		public static DateTime GetTimeFromUnixTime(int unixTimestamp)
		{
			// todo needs testing!
			return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
		}
	}

	public class CurrentWeatherViewModel
	{
		public CurrentWeatherViewModel(CurrentWeather weather)
		{
			IconUrl = weather.WeatherIconUrl();
			BigLetters = $"{weather.Main.Temperature:0}°";
			City = weather.CityName;
		}

		public string IconUrl { get; set; }
		public string BigLetters { get; set; }
		public string City { get; set; }
	}

	//*********************************************************
	// ViewModelBase:
	//
	// Copyright (c) Microsoft. All rights reserved.
	// This code is licensed under the MIT License (MIT).
	// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
	// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
	// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
	// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
	//
	//*********************************************************
	public class ViewModelBase : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Fires an event when called. Used to update the UI in the MVVM world.
		/// [CallerMemberName] Ensures only the peoperty that calls it gets the event
		/// and not every property
		/// </summary>
		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}