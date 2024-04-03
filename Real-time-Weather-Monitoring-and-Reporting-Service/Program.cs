using System.Xml.Serialization;
using Real_time_Weather_Monitoring_and_Reporting_Service;
using Real_time_Weather_Monitoring_and_Reporting_Service.Data;

Utilities.Menu();
// string xmlString = @"<WeatherData><Location>City Name</Location><Temperature>23.0</Temperature><Humidity>85.0</Humidity></WeatherData>";
//
// XmlSerializer serializer = new XmlSerializer(typeof(WeatherData));
// using (StringReader reader = new StringReader(xmlString))
// {
//     var weatherForecast = (WeatherData)serializer.Deserialize(reader);
//     Console.WriteLine($"Location: {weatherForecast?.Location}");
//     Console.WriteLine($"Temperature: {weatherForecast?.Temperature}");
//     Console.WriteLine($"Humidity: {weatherForecast?.Humidity}");
// }