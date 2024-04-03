using Real_time_Weather_Monitoring_and_Reporting_Service.Data;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;

public interface IDataProcessor
{
    WeatherData ReadFile(string content);
}