using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.Statics;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ExtensionMethodsController : ControllerBase
    {
        [HttpGet]
        public IActionResult ExtensionMethodsExamples()
        {
            string cuil1 = "20307507508";
            string cuil2 = "2030750708";

            var formattedCuil1 = cuil1.ToCUILFormat(); //Llamo al metodo desde un string directamente.
            var formattedCuil2 = cuil2.ToCUILFormat();

            var is8Even = 8.IsEven(); //Llamo al metodo desde un entero directamente. Incluso si es una constante.
            var is11Even = 11.IsEven();
            var is15Odd = 15.IsOdd();

            return Ok(new { 
                formattedCuil1,
                formattedCuil2,
                is8Even,
                is11Even,
                is15Odd
            });
            //Que tipo es este ^^^^? 
        }
    }
}