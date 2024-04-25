using Real_time_Weather_Monitoring_and_Reporting_Service.ConsoleReader;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.Handeler;


public class InputHandling
{
    private readonly IConsoleReader _consoleReader;

    public InputHandling(IConsoleReader consoleReader)
    {
        _consoleReader = consoleReader;
    }

    public virtual T? HandleUserInput<TException, T>(Func<T> optionAction) where TException : Exception
    {
        while (true)
        {
            try
            {
                return optionAction();
            }
            catch (TException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                if (ExitCond() == -1)
                    break;
            }
        }

        return default(T);
    }

    public virtual int ExitCond()
    {
        Console.Write("To exit type e or E => ");
        var e = _consoleReader.ReadLine() ?? string.Empty;
        Console.WriteLine();
        if (e.ToLower().Trim().Equals("e")) return -1;

        return 0;
    }
}
