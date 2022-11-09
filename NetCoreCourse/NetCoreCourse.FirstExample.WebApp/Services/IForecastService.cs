namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public interface IForecastService
    {
        int GetTemperature();
        string GetWeatherByCity(string city);

        DateTime LastWeatherReportDate { get; }
    }
}
