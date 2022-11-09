namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public class ForecastService : IForecastService
    {
        public DateTime LastWeatherReportDate => DateTime.UtcNow.Date;

        public int GetTemperature()
        {
            return new Random().Next(-5, 45);
        }

        public string GetWeatherByCity(string city)
        {
            return city.ToUpperInvariant() == "ROSARIO" ?
                    "El clima esta frio en Rosario." :
                    "No conozco esa ciudad";
        }
    }
}
