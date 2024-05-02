using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

public sealed class SnowBot(double temperatureThreshold, string message) : IWeatherBot
{
    public double TemperatureThreshold { get; set; } = temperatureThreshold;
    public string Message { get; set; } = message;

    public void Update(IWeatherPublisher publisher)
    {
        if (publisher.TemperatureThreshold < TemperatureThreshold)
        {
            Console.WriteLine($"""
                               Snow Bot Activated!!
                               {Message}
                               """);
        }
    }
}