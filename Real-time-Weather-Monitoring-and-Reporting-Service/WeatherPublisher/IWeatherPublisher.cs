using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

public interface IWeatherPublisher
{
    public double HumidityThreshold { get; set; }

    public double TemperatureThreshold { get; set; }

    void Attach(IWeatherBot weatherBot);
    void Detach(IWeatherBot weatherBot);
    void Notify();
    public void InteractWithUsers();
}