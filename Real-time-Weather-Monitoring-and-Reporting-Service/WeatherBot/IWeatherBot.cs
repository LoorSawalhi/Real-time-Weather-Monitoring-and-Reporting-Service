using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

public interface IWeatherBot
{
    void Update(IWeatherPublisher publisher);
}