using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreCourse.FirstExample.WebApp.Configuration;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly FirstConfigurationOptions configuration;

        public ConfigController(IOptions<FirstConfigurationOptions> options)

        {
            configuration = options?.Value ?? throw new ArgumentNullException("FirstConfigurationOptions was not properly set.");
        }

        [HttpGet]
        public IActionResult Config()
        {
            //De donde viene el valor de 'overwritten string'?
            //Cual es nuestro ambiente actual?
            return Ok(configuration);
        }
    }
}