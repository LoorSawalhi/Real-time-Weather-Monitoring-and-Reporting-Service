using Real_time_Weather_Monitoring_and_Reporting_Service.CustomExceptions;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;
using Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;
using Real_time_Weather_Monitoring_and_Reporting_Service.Handeler;

namespace Real_time_Weather_Monitoring_and_Reporting_Service;

internal class Utilities
{
    private static int _inputLine;
    private const string InvalidOption = "Invalid Option !!! Try again.";

    public static WeatherData Menu()
    {
        var dataProcessor = InputHandling.HandleUserInput<Exception, IDataProcessor>(() =>
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

        var data = InputHandling.HandleUserInput<Exception, WeatherData>(() =>
        {
            Console.WriteLine("Enter Your data :");
            var dataToBeProcessed = ReadString();
            return dataProcessor.ReadData(dataToBeProcessed);
        });
     Console.WriteLine($"Location: {data?.Location}");
     Console.WriteLine($"Temperature: {data?.Temperature}");
     Console.WriteLine($"Humidity: {data?.Humidity}");

     return data;
    }

    private static IDataProcessor Options(int option)
    {
        IDataProcessor dataProcessor = option switch
        {
            1 => new XmlDataProcessor(),
            2 => new JsonDataProcessor(),
            _ => throw new NotValidUserInputException(InvalidOption)
        };
        return dataProcessor;
    }

    private static int ReadOption()
    {
        var readLine = Console.ReadLine();
        Console.WriteLine();
        if (readLine == null || !int.TryParse(readLine, out var option))
            throw new NotValidUserInputException(InvalidOption);

        return option;
    }

    private static string ReadString()
    {
        var readLine = Console.ReadLine();
        Console.WriteLine();
        if (readLine == null)
            throw new NotValidUserInputException("Empty Input!!");

        return readLine;
    }
}