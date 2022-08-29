using Microsoft.AspNetCore.Mvc;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ILogger categoryLogger;

        public LogController(ILoggerFactory loggerFactory, ILogger<LogController> categoryLogger)
        {
            this.logger = loggerFactory.CreateLogger("NetCoreCourse");
            this.categoryLogger = categoryLogger;
        }

        [HttpGet]
        public IActionResult LogAction(bool? error)
        {
            //Check the console
            logger.LogInformation("We are logging information.");
            logger.LogWarning("WarningsAlso.");

            //Check the ID and the logger
            categoryLogger.LogInformation(1050, "Logging with Category and Id.");

            //Check what happens with Traces
            logger.LogTrace("Trace Default Logger.");
            categoryLogger.LogTrace("Trace with Category.");

            if (error != null && error.Value)
            {
                ThrowExceptionMethod();
            }

            return Ok();
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
