using AutoFixture;
using Moq;
using Real_time_Weather_Monitoring_and_Reporting_Service.CustomExceptions;
using Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;
using static Real_time_Weather_Monitoring_and_Reporting_Service.Utilities;

namespace ProjectTests;

public class UtilitiesTests
{
    private readonly Fixture _fixture;

    public UtilitiesTests()
    {
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData(1, typeof(XmlDataProcessor))]
    [InlineData(2, typeof(JsonDataProcessor))]
    public void Options_Should_Return_Correct_DataProcessor(int option, Type expectedType)
    {
        var result = Options(option);

        Assert.IsType(expectedType, result);
    }

    [Fact]
    public void Options_Should_Throw_NotValidUserInputException_For_InvalidOption()
    {
        var invalidOption = _fixture.Create<int>();
        while (invalidOption == 1 || invalidOption == 2)
        {
            invalidOption = _fixture.Create<int>();
        }

        Assert.Throws<NotValidUserInputException>(() => Options(invalidOption));
    }

    [Fact]
    public void ReadOption_Should_Return_Integer_Value_When_Input_Is_Valid()
    {
        var validOption = _fixture.Create<int>();
        var stringReader = new StringReader(validOption + "");
        Console.SetIn(stringReader);
        var result = ReadOption();

        Assert.Equal(validOption, result);
    }

    [Fact]
    public void ReadOption_Should_Return_NotValidUserInputException_For_InvalidInput()
    {
        var invalidInput = _fixture.Create<string>();
        var stringReader = new StringReader(invalidInput);
        Console.SetIn(stringReader);

        Assert.Throws<NotValidUserInputException>(() => ReadOption());
    }

    [Fact]
    public void ReadOption_Should_Return_NotValidUserInputException_For_EmptyInput()
    {
        var stringReader = new StringReader("");
        Console.SetIn(stringReader);

        Assert.Throws<NotValidUserInputException>(() => ReadOption());
    }

    [Fact]
    public void ReadString_Should_Return_NotValidUserInputException_For_EmptyInput()
    {
        var stringReader = new StringReader(string.Empty);
        Console.SetIn(stringReader);

        Assert.Throws<NotValidUserInputException>(() => ReadString());
    }

    [Fact]
    public void ReadOption_Should_Return_InputString_When_Input_Is_Valid()
    {
        var validOption = _fixture.Create<string>();
        var stringReader = new StringReader(validOption);
        Console.SetIn(stringReader);
        var result = ReadString();

        Assert.Equal(validOption, result);
    }
}