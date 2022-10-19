using Microsoft.AspNetCore.Mvc;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ILogger configuredLogger;

        public LogController(ILoggerFactory loggerFactory, ILogger<LogController> configuredLogger)
        {
            this.logger = loggerFactory.CreateLogger("NetCoreCourse");
            this.configuredLogger = configuredLogger;
        }

        [HttpGet]
        public IActionResult LogAction(bool? error)
        {
            //Check the console
            logger.LogInformation("We are logging information.");
            logger.LogWarning("WarningsAlso.");

            //Check the ID and the logger
            configuredLogger.LogInformation(1050, "Logging with Category and Id.");

            //Check what happens with Traces
            logger.LogTrace("Trace Default Logger.");
            configuredLogger.LogTrace("Trace with Category.");

            if (error != null && error.Value)
            {
                ThrowExceptionMethod();
            }

            return Ok();
        }

        [HttpGet("clients")]
        public void GetClients()
        {
            logger.LogDebug("Comenzo el metodo GetClients");

            //Ir a la base de datos y recuperar los clientes
            var clients = new List<string> { "Marcos", "Frank" };
            //Ir a la base de datos y recuperar los clientes

            logger.LogInformation($"En la base de datos, encontramos {clients.Count} clientes");

            //Validar que todos los clientes hayan pagado
            var goodClients = clients.Where(x => x != "Frank").ToList();
            logger.LogDebug("La cantidad de clientes que realmente pagaron es: " + goodClients.Count);
            //Validar que todos los clientes hayan pagado

            logger.LogDebug("Finalizo el metodo GetClients.");

            //return Ok();
        }

        private void ThrowExceptionMethod()
        {
            //Check the StackTrace
             var ex = new InvalidOperationException("You cannot send errors to the app.");
            logger.LogError(ex, "We received an error");
            throw ex;
        }
    }
}
