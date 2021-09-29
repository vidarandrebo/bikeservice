using System;

namespace BikeHistory
{
    public interface IWeatherForecast {
        DateTime Date {get; set;}
        int TemperatureC {get; set;}
        int TemperatureF();
        string Summary {get; set;}
    }

    public class WeatherForecast : IWeatherForecast
    {
        public DateTime Date {get; set;}

        public int TemperatureC { get; set; }

        public int TemperatureF(){
            return 32 + (int)(TemperatureC / 0.5556);
        }
        public string Summary { get; set; }
    }
}
