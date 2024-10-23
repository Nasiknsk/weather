# Weather Application

## Overview
The Weather Application is a standalone C# MVC project that allows users to fetch current weather data for various cities using a Weather API. The application supports full CRUD (Create, Read, Update, Delete) operations for managing weather-related data stored in a local database.

## Features
- **Fetch Weather Data**: Retrieve current weather information from an external Weather API.
- **CRUD Operations**: Manage weather data (Create, Read, Update, Delete) stored in a local database.
- **User-Friendly Interface**: Intuitive UI built with Windows Forms or WPF.

## Technologies Used
- **C# (.NET Core MVC)**: Backend development.
- **Windows Forms / WPF**: User interface development.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server / PostgreSQL**: Database for storing weather data.
- **Weather API**: External API to fetch real-time weather data.

## Prerequisites
Before running this project, ensure you have the following installed:
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-download) or [PostgreSQL](https://www.postgresql.org/download/)
- A Weather API Key (e.g., from [OpenWeatherMap](https://openweathermap.org/))

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/weather-application.git
cd weather-application
2. Configure Database
Create a SQL Server or PostgreSQL database and configure the connection string in the appsettings.json file:

json
Copy code
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=WeatherDB;User Id=your-username;Password=your-password;"
}
Run migrations (if applicable) to set up the database schema.

3. Get Your Weather API Key
Sign up at OpenWeatherMap or any other weather data provider to get an API key. In the appsettings.json, add your API key:

json
Copy code
"WeatherApi": {
    "BaseUrl": "https://api.openweathermap.org/data/2.5/",
    "ApiKey": "your-api-key-here"
}
4. Build and Run the Project
You can build and run the project using Visual Studio:

Open the solution file in Visual Studio.
Build the project.
Run the application.
API Endpoints
Fetch Current Weather from External API

GET /api/weather/{city}
Fetch current weather data for the given city from the external Weather API.
Example Request:
http
Copy code
GET /api/weather/London
Create Weather Data (Manual Entry)

POST /api/weather
Store weather data in the database.
Request Body Example:
json
Copy code
{
    "city": "London",
    "temperature": 15.5,
    "humidity": 80,
    "description": "Cloudy"
}
Read All Stored Weather Data

GET /api/weather
Retrieve all stored weather data from the database.
Update Weather Data

PUT /api/weather/{id}
Update weather data entry for a specific ID.
Request Body Example:
json
Copy code
{
    "city": "New York",
    "temperature": 20.3,
    "humidity": 65,
    "description": "Sunny"
}
Delete Weather Data

DELETE /api/weather/{id}
Remove a specific weather data entry from the database.
Running Unit Tests
To run the unit tests (if any are included), use the following command:

bash
Copy code
dotnet test
Contributing
If you'd like to contribute to this project, feel free to submit a pull request. Please ensure all tests pass and the code is properly documented.

License
This project is licensed under the MIT License. See the LICENSE file for details.

Author
Mohammed Naseek
GitHub
