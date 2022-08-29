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

            var formattedCuil1 = cuil1.ToCUILFormat(); //calling directly from string
            var formattedCuil2 = cuil2.ToCUILFormat();

            var is8Even = 8.IsEven(); //calling directly from int. Even a constant.
            var is11Even = 11.IsEven();
            var is15Odd = 15.IsOdd();

            return Ok(new { 
                formattedCuil1,
                formattedCuil2,
                is8Even,
                is11Even,
                is15Odd
            });
            //What kind of class is this one ^^^^?
        }
    }
}