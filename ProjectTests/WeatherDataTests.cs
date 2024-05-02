using System.Text.Json;
using System.Xml.Serialization;
using AutoFixture;
using FluentAssertions;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;
using Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;
using Xunit.Abstractions;

namespace ProjectTests;

[Collection("Sequential")]
public class WeatherDataTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly TextWriter _originalOutput;
    private readonly TextReader _originalInput;

    public WeatherDataTests(ITestOutputHelper output)
    {
        _output = output;
        this._originalOutput = Console.Out;
        this._originalInput = Console.In;
    }

    public void Dispose()
    {
        Console.SetIn(_originalInput);
        Console.SetOut(_originalOutput);
    }

    [Fact]
    public void ReadData_ShouldCorrectlyDeserializeJsonString_ToWeatherData()
    {
        var fixture = new Fixture();
        var jsonDataProcessor = new JsonDataProcessor();

        var expectedWeatherData = fixture.Create<WeatherData>();
        var jsonContent = JsonSerializer.Serialize(expectedWeatherData);

        var weatherData = jsonDataProcessor.ReadData(jsonContent);

        expectedWeatherData.Should().BeEquivalentTo(weatherData);
    }

    [Fact]
    public void ReadData_ShouldCorrectlyDeserializeXMLString_ToWeatherData()
    {
        var fixture = new Fixture();
        var xmlDataProcessor = new XmlDataProcessor();

        var expectedWeatherData = fixture.Create<WeatherData>();

        var serializer = new XmlSerializer(typeof(WeatherData));
        using var writer = new StringWriter();
        {
            serializer.Serialize(writer, expectedWeatherData);
            var xmlContent = writer.ToString();

            var weatherData = xmlDataProcessor.ReadData(xmlContent);

            expectedWeatherData.Should().BeEquivalentTo(weatherData);
        }
    }
}