using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

public sealed class RainBot : IWeatherBot
{
    public RainBot(double humidityThreshold, string message)
    {
        HumidityThreshold = humidityThreshold;
        Message = message;
    }

    public double HumidityThreshold { get; set; }
    public string Message { get; set; }

    public void Update(IWeatherPublisher publisher)
    {
        if (publisher.HumidityThreshold > HumidityThreshold)
        {
            Console.WriteLine($"""
                              Rain Bot Activated!!
                              {Message}
                              """);
        }
    }
}