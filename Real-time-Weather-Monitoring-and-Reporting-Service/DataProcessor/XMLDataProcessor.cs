using System.Xml.Serialization;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;

public class XmlDataProcessor : IDataProcessor
{
    public WeatherData ReadData(string content)
    {
        var serializer = new XmlSerializer(typeof(WeatherData));
        using var reader = new StringReader(content);
        var weatherForecast = (WeatherData)serializer.Deserialize(reader)!;
        return weatherForecast;
    }
}