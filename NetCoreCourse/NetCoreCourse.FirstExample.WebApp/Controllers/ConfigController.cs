using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreCourse.FirstExample.WebApp.Configuration;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly FirstConfigurationOptions configuration; //This is what we usually use.

        public ConfigController(IOptions<FirstConfigurationOptions> options)

        {
            configuration = options?.Value ?? throw new ArgumentNullException("FirstConfigurationOptions was not properly set.");
        }

        [HttpGet]
        public IActionResult Config()
        {
            //Where does 'overwritten string' comes?
            //Which is our current environment?
            return Ok(configuration);
        }
    }
}