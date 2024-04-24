using System.Text.Json;
using System.Xml.Serialization;
using AutoFixture;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;
using Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;
using Xunit.Abstractions;

namespace ProjectTests;

public class WeatherDataTests
{
    private readonly ITestOutputHelper _output;

    public WeatherDataTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ReadData_Should_CorrectlyDeserializeJsonString_To_WeatherData()
    {
        var fixture = new Fixture();
        var jsonDataProcessor = new JsonDataProcessor();

        var expectedWeatherData = fixture.Create<WeatherData>();
        var jsonContent = JsonSerializer.Serialize(expectedWeatherData);

        var weatherData = jsonDataProcessor.ReadData(jsonContent);

        Assert.Equal(expectedWeatherData.Temperature, weatherData.Temperature);
        Assert.Equal(expectedWeatherData.Humidity, weatherData.Humidity);
        Assert.Equal(expectedWeatherData.Location, weatherData.Location);
    }

    [Fact]
    public void ReadData_Should_CorrectlyDeserializeXMLString_To_WeatherData()
    {
        var fixture = new Fixture();
        var xmlDataProcessor = new XmlDataProcessor();

        var expectedWeatherData = fixture.Create<WeatherData>();

        var serializer = new XmlSerializer(typeof(WeatherData));
        using var writer = new StringWriter();
        serializer.Serialize(writer, expectedWeatherData);
        var xmlContent = writer.ToString();

        var weatherData = xmlDataProcessor.ReadData(xmlContent);

        Assert.Equal(expectedWeatherData.Temperature, weatherData.Temperature);
        Assert.Equal(expectedWeatherData.Humidity, weatherData.Humidity);
        Assert.Equal(expectedWeatherData.Location, weatherData.Location);
    }
}