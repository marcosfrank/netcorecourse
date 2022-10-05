using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.Models;
using NetCoreCourse.FirstExample.WebApp.Services;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IForecastService forecastService;
        private readonly ITransientRandomValueService transientService;
        private readonly IScopedRandomValueService scopedService;
        private readonly ISingletonRandomValueService singletonService;
        private readonly IServiceUsingServices serviceWithServices;

        public ServicesController(
            IForecastService forecastService,
            ITransientRandomValueService transientService,
            IScopedRandomValueService scopedService,
            ISingletonRandomValueService singletonService,
            IServiceUsingServices serviceWithServices
        )

        {
            this.forecastService = forecastService;
            this.transientService = transientService;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
            this.serviceWithServices = serviceWithServices;
        }

        [HttpGet("forecast")]
        public IActionResult GiveMeWeather(string city = "Rosario")
        {
            var result = forecastService.GetWeatherByCity(city);
            return Ok(result);
        }

        [HttpGet("random")]
        public IActionResult RandomValue()
        {
            //Observar la diferencia entre las respuestas. Podes determinar cuando los valores cambiaron?
            var fromController = new RandomServiceValues
            {
                Transient = transientService.RandomValue,
                Scoped = scopedService.RandomValue,
                Singleton = singletonService.RandomValue
            };

            var fromService = serviceWithServices.GetRandomValues();

            return Ok(new RandomServiceResponse(fromController, fromService));
        }
    }
}