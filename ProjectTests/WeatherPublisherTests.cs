using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Real_time_Weather_Monitoring_and_Reporting_Service;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;
using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;
using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

namespace ProjectTests;

public class WeatherPublisherTests
{
    private readonly IFixture _fixture;
    private readonly Mock<Utilities> _utilitiesMock;
    private readonly Mock<IWeatherBot> _weatherBotMock;
    private WeatherPublisher _weatherPublisher;

    public WeatherPublisherTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _utilitiesMock = _fixture.Freeze<Mock<Utilities>>();
        _weatherBotMock = _fixture.Freeze<Mock<IWeatherBot>>();
        _weatherPublisher = new WeatherPublisher(_utilitiesMock.Object);
    }

    [Fact]
    public void Attach_ShouldAddWeatherBot_ToWeatherBots()
    {
        var weatherBot = _fixture.Create<IWeatherBot>();

        _weatherPublisher.Attach(weatherBot);
        _weatherPublisher.WeatherBots.Should().Contain(weatherBot);
    }

    [Fact]
    public void Detach_ShouldRemoveWeatherBot_FromWeatherBots()
    {
        var weatherBots = _fixture.Create<List<IWeatherBot>>();
        var weatherBot = weatherBots[0];
        //Act
        _weatherPublisher.Detach(weatherBot);

        //Assert
        _weatherPublisher.WeatherBots.Should().NotContain(weatherBot);
    }

    [Fact]
    public void Notify_CallsUpdateOnEachWeatherBot()
    {
        var weatherBot = new Mock<IWeatherBot>();
        _weatherPublisher.Attach(weatherBot.Object);

        // Act
        _weatherPublisher.Notify();

        //Assert
        weatherBot.Verify(bot => bot.Update(_weatherPublisher), Times.Once);
    }

    [Fact]
    public void InteractWithUsers_ShouldNotifyBots_WhenCalled()
    {
        var data = _fixture.Create<WeatherData>();
        _utilitiesMock.Setup(menu => menu.Menu()).Returns(data);

        //Act
        _weatherPublisher.InteractWithUsers();
        
        //Assert
        Assert.Equal(_weatherPublisher.HumidityThreshold, data.Humidity);
        Assert.Equal(_weatherPublisher.TemperatureThreshold, data.Temperature);
    }
}