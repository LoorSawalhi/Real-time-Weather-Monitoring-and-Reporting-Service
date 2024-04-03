using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

public class WeatherPublisher : IWeatherPublisher
{
    private List<IWeatherBot> _weatherBots = [];

    public double HumidityThreshold { get; set; }

    public double TemperatureThreshold { get; set; }

    public void Attach(IWeatherBot weatherBot)
    {
        Console.WriteLine($"Attaching weather bot");
        _weatherBots.Add(weatherBot);
    }

    public void Detach(IWeatherBot weatherBot)
    {
        _weatherBots.Remove(weatherBot);
        Console.WriteLine($"De-attaching weather bot");
    }

    public void Notify()
    {
        foreach (var weatherBot in _weatherBots)
        {
            weatherBot.Update(this);
        }
    }
}