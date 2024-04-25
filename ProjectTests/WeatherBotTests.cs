using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;
using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

namespace ProjectTests;

[Collection("Sequential")]
public class WeatherBotTests : IDisposable
{
    private readonly RainBot _rainBot;
    private readonly SnowBot _snowBot;
    private readonly SunBot _sunBot;
    private readonly IFixture _fixture;
    private readonly Mock<IWeatherPublisher> _weatherPublisherMock;
    private readonly TextWriter _originalOutput;
    private readonly TextReader _originalInput;


    public WeatherBotTests()
    {
        this._fixture = new Fixture().Customize(new AutoMoqCustomization());
        this._rainBot = _fixture.Create<RainBot>();
        this._snowBot = _fixture.Create<SnowBot>();
        this._sunBot = _fixture.Create<SunBot>();
        this._weatherPublisherMock = _fixture.Freeze<Mock<IWeatherPublisher>>();
        this._originalOutput = Console.Out;
        this._originalInput = Console.In;
    }

    public void Dispose()
    {
        Console.SetIn(_originalInput);
        Console.SetOut(_originalOutput);
    }

    [Theory]
    [InlineData(60, 50)]
    public void Update_ShouldPrintMessage_WhenHumidityExceedsThreshold(double humidity, double threshold)
    {
        // Arrange
        var message = _fixture.Create<string>();
        _weatherPublisherMock.Setup(p => p.HumidityThreshold).Returns(humidity);
        _rainBot.HumidityThreshold = threshold;
        _rainBot.Message = message;

        using var consoleOutput = new StringWriter();
        {
            Console.SetOut(consoleOutput);

            _rainBot.Update(_weatherPublisherMock.Object);
            var actualMessage = consoleOutput.ToString().Trim();
            actualMessage.Should().Be($"""
                                       Rain Bot Activated!!
                                       {message}
                                       """);
        }
    }

    [Theory]
    [InlineData(50, 60)]
    public void Update_ShouldPrintMessage_WhenTempreatureIsBelowThreshold(double tempreature, double threshold)
    {
        // Arrange
        var message = _fixture.Create<string>();
        _weatherPublisherMock.Setup(p => p.TemperatureThreshold).Returns(tempreature);
        _snowBot.TemperatureThreshold = threshold;
        _snowBot.Message = message;

        using var consoleOutput = new StringWriter();
        {
            Console.SetOut(consoleOutput);

            _snowBot.Update(_weatherPublisherMock.Object);
            var actualMessage = consoleOutput.ToString().Trim();
            actualMessage.Should().Be($"""
                                       Snow Bot Activated!!
                                       {message}
                                       """);
        }
    }


    [Theory]
    [InlineData(70, 60)]
    public void Update_ShouldPrintMessage_WhenTemperatureExceedsThreshold(double temperature, double threshold)
    {
        // Arrange
        var message = _fixture.Create<string>();
        _weatherPublisherMock.Setup(p => p.TemperatureThreshold).Returns(temperature);
        _sunBot.TemperatureThreshold = threshold;
        _sunBot.Message = message;

        using var consoleOutput = new StringWriter();
        {
            Console.SetOut(consoleOutput);

            _sunBot.Update(_weatherPublisherMock.Object);
            var actualMessage = consoleOutput.ToString().Trim();
            actualMessage.Should().Be($"""
                                       Sun Bot Activated!!
                                       {message}
                                       """);
        }
    }
}