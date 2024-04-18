# Real-time Weather Monitoring Sysytem
C# console application that simulates a real-time weather monitoring and reporting service. The system receives and processes raw weather data in multiple formats (JSON, XML, etc.) from various weather stations for different locations. 
The application includes different types of 'weather bots' each of which is configured to behave differently based on the weather updates it receives.

## Data Format
1 - XML

2 - JSON

## File Configuration
All the bot's settings should be controlled via a configuration file, including whether it is enabled, the threshold that activates it, and the message it outputs when activated. The configuration file is in a JSON format.
```
{
  "RainBot": {
    "enabled": true,
    "humidityThreshold": 70,
    "message": "It looks like it's about to pour down!"
  },
  "SunBot": {
    "enabled": true,
    "temperatureThreshold": 30,
    "message": "Wow, it's a scorcher out there!"
  },
  "SnowBot": {
    "enabled": false,
    "temperatureThreshold": 0,
    "message": "Brrr, it's getting chilly!"
  }
}
```

## Code Structure
The code structure is based on the Observer and the Strategy design patterns.
