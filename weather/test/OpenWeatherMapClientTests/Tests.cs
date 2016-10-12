using Xunit;
using OpenWeatherMapClient;

namespace Tests
{
	public class Tests
	{
		[Fact]
		public void Test1()
		{
			var condition = new WeatherCondition();

			condition.Icon = "2d";

			Assert.Equal(condition.IconUrl, "http://openweathermap.org/img/w/2d.png");
		}
	}
}
