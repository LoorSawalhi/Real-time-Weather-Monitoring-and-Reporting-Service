namespace Real_time_Weather_Monitoring_and_Reporting_Service.Handeler;

public class InputHandling
{
    public static T? HandleUserInput<TException, T>(Func<T> optionAction) where TException : Exception
    {
        while (true)
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

        return default(T);
    }

    private static int ExitCond()
    {
        Console.Write("To exit type e or E => ");
        var e = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        if (e.ToLower().Trim().Equals("e")) return -1;

        return 0;
    }
}