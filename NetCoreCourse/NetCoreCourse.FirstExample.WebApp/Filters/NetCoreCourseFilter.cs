using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreCourse.FirstExample.WebApp.Filters
{
    public class NetCoreCourseFilter : IActionFilter
    {
        private readonly ILogger logger;

        public NetCoreCourseFilter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger("NetCoreCourse");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Antes de que llegue a la accion del controller
            //logger.LogInformation("SE esta ejecutando nuestro filtro.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Luego de que finalizo la ejecucion del controller
            //logger.LogInformation("Se ejecuto el endpoint, ahora el request vuelve a pasar for el filtro");
        }
    }
}