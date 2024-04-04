﻿using Real_time_Weather_Monitoring_and_Reporting_Service.AppConfiguration;
using Real_time_Weather_Monitoring_and_Reporting_Service.WeatherPublisher;

var weatherPublisher = new WeatherPublisher();
var manager = new ConfigurationManager("/home/loor/Desktop/Foothill Training/C#/Real-time Weather Monitoring and Reporting Service/Real-time-Weather-Monitoring-and-Reporting-Service/Real-time-Weather-Monitoring-and-Reporting-Service/AppConfiguration/configurationDetails.json");

manager.AttachObservers(weatherPublisher);
weatherPublisher.InteractWithUsers();

// string xmlString = @"<WeatherData><Location>City Name</Location><Temperature>23.0</Temperature><Humidity>85.0</Humidity></WeatherData>";
