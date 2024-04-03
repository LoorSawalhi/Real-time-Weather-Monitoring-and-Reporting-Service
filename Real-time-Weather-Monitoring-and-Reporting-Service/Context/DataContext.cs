using Real_time_Weather_Monitoring_and_Reporting_Service.DataProcessor;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.Context;

public class DataContext(IDataProcessor reader)
{
    public object ReadData(string inputData)
    {
        return reader.ReadData(inputData);
    }
}