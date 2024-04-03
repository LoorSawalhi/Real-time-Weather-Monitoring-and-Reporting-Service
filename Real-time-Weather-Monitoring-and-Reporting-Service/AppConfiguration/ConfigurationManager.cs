using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherBot;

namespace Real_time_Weather_Monitoring_and_Reporting_Service.AppConfiguration;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class ConfigurationManager
{
    private Dictionary<string, JsonElement>? _config;

    public ConfigurationManager(string configFilePath)
    {
        string json = File.ReadAllText(configFilePath);
        _config = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
        // foreach (var observerConfig in _config)
        // {
        //     Console.WriteLine($"Observer: {observerConfig.Key}");
        //     Console.WriteLine($"  Enabled: {observerConfig.Value.GetProperty("enabled")}");
        //     if (observerConfig.Value.GetProperty("humidityThreshold").ValueKind != JsonValueKind.Undefined)
        //     {
        //         Console.WriteLine($"  Humidity Threshold: {observerConfig.Value.humidityThreshold}");
        //     }
        //     if (observerConfig.Value.GetProperty("temperatureThreshold").ValueKind != JsonValueKind.Undefined)
        //     {
        //         Console.WriteLine($"  Temperature Threshold: {observerConfig.Value.temperatureThreshold}");
        //     }
        //     Console.WriteLine($"  Message: {observerConfig.Value.message}");
        //     Console.WriteLine();
        // }
    }

    public void AttachObservers(WeatherPublisher.WeatherPublisher publisher)
    {
        if (_config["RainBot"].GetProperty("enabled").GetBoolean())
        {
            publisher.Attach(new RainBot(
                _config["RainBot"].GetProperty("humidityThreshold").GetInt32(),
                _config["RainBot"].GetProperty("message").GetString()));
        }

        if (_config["SunBot"].GetProperty("enabled").GetBoolean())
        {
            publisher.Attach(new SunBot(
                _config["SunBot"].GetProperty("temperatureThreshold").GetInt32(),
                _config["SunBot"].GetProperty("message").GetString()));
        }

        if (_config["SnowBot"].GetProperty("enabled").GetBoolean())
        {
            publisher.Attach(new SnowBot(
                _config["SnowBot"].GetProperty("temperatureThreshold").GetInt32(), 
                _config["SnowBot"].GetProperty("message").GetString()));
        }
    }

}