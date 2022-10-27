using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.Dto;
using NetCoreCourse.FirstExample.WebApp.Handlers;

namespace NetCoreCourse.FirstExample.WebApp.Controllers.WebAPI
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IJwtHandler jwtHandler;

        public AccountsController(IJwtHandler jwtHandler)
        {
            this.jwtHandler = jwtHandler;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody]UserForLoginDto user) //Es raro que esto sea un POST no?
        {
            //Aca deberiamos buscar en nuestra base de datos si existe un usuario con ese username y password
            // para simplificar, si el usuario es 'admin' va a tener el rol admin, si el usuario es 'user' va a tener el rol user.
            var roles = user.UserName == "admin" ?
                    new List<string> { "Admin" } :
                    new List<string> { "User" };


            var bearer = jwtHandler.GenerateToken(user, roles);
            return Ok(new {
                token = bearer,
            });
        }
    }
}
