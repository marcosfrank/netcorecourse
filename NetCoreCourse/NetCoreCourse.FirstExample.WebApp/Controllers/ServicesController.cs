using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.Models;
using NetCoreCourse.FirstExample.WebApp.Services;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IForecastService _forecastService;
        private readonly ITransientRandomValueService transientService;
        private readonly IScopedRandomValueService scopedService;
        private readonly ISingletonRandomValueService singletonService;
        private readonly IServiceUsingServices serviceWithServices;
        private readonly IExcerciseService excerciseService2;

        public ServicesController(
            IForecastService forecastService,
            ITransientRandomValueService transientService,
            IScopedRandomValueService scopedService,
            ISingletonRandomValueService singletonService,
            IServiceUsingServices serviceWithServices
        )

        {
            _forecastService = forecastService;
            this.transientService = transientService;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
            this.serviceWithServices = serviceWithServices;
            //excerciseService2 = excerciseService;
        }

        [HttpGet("forecast")]
        public IActionResult GiveMeWeather(string city = "Rosario")
        {
            //var resultadoService = excerciseService2.MiMetodo();
            var result = _forecastService.GetWeatherByCity(city);
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

            serviceWithServices.GetRandomValues2(fromController);

            var st = new Estructura();
            st.MyProperty = 2;

            serviceWithServices.GetRandomValues3(st);

            return Ok(new RandomServiceResponse(fromController, new RandomServiceValues()));
        }
    }

    public struct Estructura {
        public int MyProperty { get; set; }
    }

    public enum TipoCurso { 
        FE,
        BE
    }
}