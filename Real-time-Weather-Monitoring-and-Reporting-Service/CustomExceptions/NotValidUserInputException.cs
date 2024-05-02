namespace Real_time_Weather_Monitoring_and_Reporting_Service.CustomExceptions;

public sealed class NotValidUserInputException : Exception
{
     public NotValidUserInputException() : base()
    {
    }
    public NotValidUserInputException(string message) : base(message)
    {
    }
}