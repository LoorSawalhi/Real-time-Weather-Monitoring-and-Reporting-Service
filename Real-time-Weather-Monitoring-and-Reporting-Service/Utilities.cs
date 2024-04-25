using Real_time_Weather_Monitoring_and_Reporting_Service.CustomExceptions;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;
using Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;
using Real_time_Weather_Monitoring_and_Reporting_Service.Handeler;

namespace Real_time_Weather_Monitoring_and_Reporting_Service;

public class Utilities
{
    private static int _inputLine;
    private const string InvalidOption = "Invalid Option !!! Try again.";
    private readonly InputHandling _inputHandling;

    public Utilities(InputHandling inputHandling)
    {
        _inputHandling = inputHandling;
    }

    public virtual WeatherData Menu()
    {
        var dataProcessor = _inputHandling.HandleUserInput<NotValidUserInputException, IDataProcessor>(() =>
        {
            Console.Write("""
                          Welcome to the Weather Monitoring System
                          Choose the type of text you wish to enter:
                          1) XML
                          2) JSON

                          Option :
                          """);
            _inputLine = ReadOption();
            return Options(_inputLine);
        });

        var data = _inputHandling.HandleUserInput<NotValidUserInputException, WeatherData>(() =>
        {
            Console.Write("Enter Your data :");
            var dataToBeProcessed = ReadString();
            return dataProcessor.ReadData(dataToBeProcessed);
        });
     Console.WriteLine($"Location: {data?.Location}");
     Console.WriteLine($"Temperature: {data?.Temperature}");
     Console.WriteLine($"Humidity: {data?.Humidity}");

     return data;
    }

    public virtual IDataProcessor Options(int option)
    {
        const int XmlOption = 1;
        const int JsonOption = 2;
        IDataProcessor dataProcessor = option switch
        {
            XmlOption => new XmlDataProcessor(),
            JsonOption => new JsonDataProcessor(),
            _ => throw new NotValidUserInputException(InvalidOption)
        };
        return dataProcessor;
    }

    public virtual int ReadOption()
    {
        var readLine = Console.ReadLine();
        Console.WriteLine();
        if (string.IsNullOrWhiteSpace(readLine) || !int.TryParse(readLine, out var option))
            throw new NotValidUserInputException(InvalidOption);

        return option;
    }

    public virtual string ReadString()
    {
        string readLine = Console.ReadLine();
        Console.WriteLine();
        if (string.IsNullOrWhiteSpace(readLine))
            throw new NotValidUserInputException("Empty Input!!");

        return readLine;
    }
}