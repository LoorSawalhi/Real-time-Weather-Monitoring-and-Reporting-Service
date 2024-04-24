namespace Real_time_Weather_Monitoring_and_Reporting_Service.CustomExceptions;

public sealed class NotValidUserInputException(string message) : Exception(message);