using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

public sealed class WeatherPublisher : IWeatherPublisher
{
    private List<IWeatherBot> _weatherBots = [];

    public List<IWeatherBot> WeatherBots => _weatherBots;

    private readonly Utilities _utilities;

    public WeatherPublisher(Utilities utilities)
    {
        _utilities = utilities;
    }

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

    public void InteractWithUsers()
    {
        var data = _utilities.Menu();

        HumidityThreshold = data.Humidity;
        TemperatureThreshold = data.Temperature;

        Notify();
    }
}