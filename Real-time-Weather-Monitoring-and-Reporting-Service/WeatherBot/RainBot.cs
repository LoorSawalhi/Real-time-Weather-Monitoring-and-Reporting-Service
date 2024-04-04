using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

public sealed class RainBot(double humidityThreshold, string message) : IWeatherBot
{
    private double _humidityThreshold = humidityThreshold;
    private string _message = message ?? String.Empty;

    public void Update(IWeatherPublisher publisher)
    {
        if ((publisher as WeatherPublisher.WeatherPublisher)!.HumidityThreshold > _humidityThreshold)
        {
            Console.WriteLine($"""
                              Rain Bot Activated!!
                              {message}
                              """);
        }
    }
}