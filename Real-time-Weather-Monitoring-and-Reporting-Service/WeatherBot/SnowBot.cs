using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

public class SnowBot(double temperatureThreshold, string message) : IWeatherBot
{
    private string _message = message;

    public void Update(IWeatherPublisher publisher)
    {
        if ((publisher as WeatherPublisher.WeatherPublisher)!.TemperatureThreshold < temperatureThreshold)
        {
            Console.WriteLine($"""
                               Snow Bot Activated!!
                               {message}
                               """);
        }
    }
}