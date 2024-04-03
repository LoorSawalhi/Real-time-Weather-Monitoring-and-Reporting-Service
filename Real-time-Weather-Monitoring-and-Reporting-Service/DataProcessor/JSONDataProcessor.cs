using System.Text.Json;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;

public class JsonDataProcessor : IDataProcessor
{
    public WeatherData ReadData(string content)
    {
        var weatherData =
            JsonSerializer.Deserialize<WeatherData>(content);
        return weatherData;
    }
}