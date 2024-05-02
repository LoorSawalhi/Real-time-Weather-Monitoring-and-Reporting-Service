using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Real_time_Weather_Monitoring_and_Reporting_Service.ConsoleReader;
using Real_time_Weather_Monitoring_and_Reporting_Service.CustomExceptions;
using Real_time_Weather_Monitoring_and_Reporting_Service.Handeler;

namespace ProjectTests;

[Collection("Sequential")]
public class InputHandlingTests : IDisposable
{
    private readonly IFixture _fixture;
    private readonly Mock<Func<string>> _funcStringMock;
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private InputHandling _inputHandling;
    private readonly TextReader _originalInput;
    private readonly TextWriter _originalOutput;

    public InputHandlingTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _funcStringMock = _fixture.Freeze<Mock<Func<string>>>();
        _consoleReaderMock = _fixture.Freeze<Mock<IConsoleReader>>();
        _inputHandling = new InputHandling(_consoleReaderMock.Object);
        _originalInput = Console.In;
        _originalOutput = Console.Out;
    }

    public void Dispose()
    {
        Console.SetIn(_originalInput);
        Console.SetOut(_originalOutput);
    }

    [Theory]
    [InlineData("e")]
    [InlineData("E")]
    public void ExitCond_ShouldReturnMinusOne_ForExitInput(string condition)
    {
        _consoleReaderMock.SetupSequence(x => x.ReadLine())
            .Returns(condition);

        var inputHandler = new InputHandling(_consoleReaderMock.Object);
        var result = inputHandler.ExitCond();

        Assert.Equal(-1, result);
    }

    [Fact]
    public void ExitCond_ShouldReturnZero_ForWrongExitInput()
    {
        var nonValidInput = _fixture.Create<string>();

        _consoleReaderMock.SetupSequence(x => x.ReadLine())
            .Returns(nonValidInput);

        var inputHandler = new InputHandling(_consoleReaderMock.Object);
        var nullResult = _inputHandling.HandleUserInput<NotValidUserInputException, string>(_funcStringMock.Object);
        var result = inputHandler.ExitCond();

        Assert.Equal(0, result);
        Assert.Null(nullResult);
    }


    [Fact]
    public void HandleUserInput_ShouldReturnValue_ForSuccessfulExecution()
    {
        var expectedResult = "Test";
        _funcStringMock.Setup(f => f()).Returns(expectedResult);

        var result = _inputHandling.HandleUserInput<NotValidUserInputException, string>(_funcStringMock.Object);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("continue")] // Input that doesn't trigger an exit
    public void HandleUserInput_ShouldReturnValueAndRepeatLoop_ForSuccessfulExecution(string condition)
    {
        var expectedResult = "Test";

        _funcStringMock.SetupSequence(f => f())
            .Throws<NotValidUserInputException>()
            .Throws<NotValidUserInputException>()
            .Returns(expectedResult);

        _consoleReaderMock.SetupSequence(x => x.ReadLine())
            .Returns(condition)
            .Returns(condition)
            .Returns("e");

        _inputHandling = new InputHandling(_consoleReaderMock.Object);

        var result = _inputHandling.HandleUserInput<NotValidUserInputException, string>(() => _funcStringMock.Object());

        _funcStringMock.Verify(f => f(), Times.Exactly(3));
        Assert.Equal(expectedResult, result);
    }
}