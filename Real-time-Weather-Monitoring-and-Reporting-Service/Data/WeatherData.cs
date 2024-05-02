namespace Real_time_Weather_Monitoring_and_Reporting_Service.Data;

[Serializable]
public class WeatherData
{
    private string _location;
    private double _temperature;
    private double _humidity;

    public string Location
    {
        get => _location;
        set => _location = value;
    }

    public double Temperature
    {
        get => _temperature;
        set => _temperature = value;
    }

    public double Humidity
    {
        get => _humidity;
        set => _humidity = value;
    }
}