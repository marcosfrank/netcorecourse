namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public class ForecastService : IForecastService
    {
        public string GetWeatherByCity(string city)
        {
            return city.ToUpperInvariant() == "ROSARIO" ?
                    "El clima esta frio en Rosario." :
                    "No conozco esa ciudad";
        }
    }
}
