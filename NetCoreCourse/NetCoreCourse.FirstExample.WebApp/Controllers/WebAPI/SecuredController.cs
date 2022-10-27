using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreCourse.FirstExample.WebApp.Controllers.WebAPI
{
    /// <summary>
    /// Controller que demuestra como diferentes roles pueden acceder a diferentes recursos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        [HttpGet("both")]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult BothAllowed()
        {
            return Ok("Este metodo puede accederlo AMBOS tipos de usuario");
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult OnlyAdmin()
        {
            return Ok("Este metodo es solo para los ADMINISTRADORES del sistema");
        }

        [HttpGet("finaluser")]
        [Authorize(Roles = "User")]
        public IActionResult OnlyFinalUser()
        {
            return Ok("Este metodo es solo para los USUARIOS finales del sistema");
        }
    }
}