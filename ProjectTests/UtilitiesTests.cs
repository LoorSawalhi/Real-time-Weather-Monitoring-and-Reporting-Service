using System.Text.Json;
using System.Xml.Serialization;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Real_time_Weather_Monitoring_and_Reporting_Service;
using Real_time_Weather_Monitoring_and_Reporting_Service.ConsoleReader;
using Real_time_Weather_Monitoring_and_Reporting_Service.CustomExceptions;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;
using Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;
using Real_time_Weather_Monitoring_and_Reporting_Service.Handeler;
using static Real_time_Weather_Monitoring_and_Reporting_Service.Utilities;

namespace ProjectTests;

[Collection("Sequential")]
public class UtilitiesTests : IDisposable
{
    private readonly IFixture _fixture;
    private readonly TextReader _originalInput;
    private readonly TextWriter _originalOutput;
    private readonly Mock<InputHandling> _inputHandlingMock;
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private Utilities _utilities;

    public UtilitiesTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _inputHandlingMock = _fixture.Freeze<Mock<InputHandling>>();
        _consoleReaderMock = _fixture.Freeze<Mock<IConsoleReader>>();
        _utilities = new Utilities(_inputHandlingMock.Object, _consoleReaderMock.Object);
        _originalInput = Console.In;
        _originalOutput = Console.Out;
    }

    public void Dispose()
    {
        Console.SetIn(_originalInput);
        Console.SetOut(_originalOutput);
    }

    [Fact]
    public void Menu_ShouldReturnWeatherData_WhenInputIsValid()
    {
        // Arrange
        var expectedDataProcessor = Mock.Of<IDataProcessor>();
        var expectedWeatherData = _fixture.Create<WeatherData>();
        var serializer = new XmlSerializer(typeof(WeatherData));
        using var writer = new StringWriter();
        serializer.Serialize(writer, expectedWeatherData);
        var xmlContent = writer.ToString();

        _consoleReaderMock.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns(xmlContent);

        var inputHandler = new InputHandling(_consoleReaderMock.Object);
        _utilities = new Utilities(inputHandler, _consoleReaderMock.Object);

        // Act
        var result = _utilities.Menu();

        // Assert
        Assert.Equal(expectedWeatherData.Location, result.Location);
        Assert.Equal(expectedWeatherData.Temperature, result.Temperature);
        Assert.Equal(expectedWeatherData.Humidity, result.Humidity);
    }

    [Theory]
    [InlineData(1, typeof(XmlDataProcessor))]
    [InlineData(2, typeof(JsonDataProcessor))]
    public void Options_ShouldReturnDataProcessor_BasedOnOption(int option, Type expectedType)
    {
        var result = _utilities.Options(option);

        Assert.IsType(expectedType, result);
    }

    [Fact]
    public void Options_ShouldThrowNotValidUserInputException_ForInvalidOption()
    {
        var invalidOption = _fixture.Create<int>();
        while (invalidOption is 1 or 2)
        {
            invalidOption = _fixture.Create<int>();
        }

        Assert.Throws<NotValidUserInputException>(() => _utilities.Options(invalidOption));
    }

    [Fact]
    public void ReadOption_ShouldReturnIntegerValue_WhenInputIsValid()
    {
        var validOption = _fixture.Create<int>();
        _consoleReaderMock.Setup(c => c.ReadLine()).Returns(validOption+"");
        var result = _utilities.ReadOption();

        Assert.Equal(validOption, result);
    }

    [Fact]
    public void ReadOption_ShouldReturnNotValidUserInputException_ForInvalidInput()
    {
        var invalidInput = _fixture.Create<string>();
        using var stringReader = new StringReader(invalidInput);
        {
            Console.SetIn(stringReader);

            Assert.Throws<NotValidUserInputException>(() => _utilities.ReadOption());
        }
    }

    [Fact]
    public void ReadOption_ShouldReturnNotValidUserInputException_ForEmptyInput()
    {
        using var stringReader = new StringReader("");
        {
            Console.SetIn(stringReader);

            Assert.Throws<NotValidUserInputException>(() => _utilities.ReadOption());
        }
    }

    [Fact]
    public void ReadString_ShouldReturnNotValidUserInputException_ForEmptyInput()
    {
        _consoleReaderMock.Setup(c => c.ReadLine()).Returns("");

        Assert.Throws<NotValidUserInputException>(() => _utilities.ReadString());
    }

    [Fact]
    public void ReadOption_ShouldReturnInputString_WhenInputIsValid()
    {
        var validOption = _fixture.Create<string>();
        _consoleReaderMock.Setup(c => c.ReadLine()).Returns(validOption);
        var result = _utilities.ReadString();

        Assert.Equal(validOption, result);
    }
}