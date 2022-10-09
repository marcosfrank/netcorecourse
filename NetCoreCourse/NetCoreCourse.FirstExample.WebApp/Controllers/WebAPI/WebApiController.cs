using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.Dto;
using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.Controllers.WebAPI
{
    [Route("api/[controller]/categories")] //Cambie esto para que se vea que siempre apuntamos a una coleccion.
    public class WebApiController : ControllerBase
    {
        #region MockData
        private List<CategoryDto> CategoriesStub = new List<CategoryDto> {
            new CategoryDto{
                Id = 1,
                Description = "Fishing"
            },
            new CategoryDto{
                Id = 2,
                Description = "Books"
            },

            new CategoryDto{
                Id = 3,
                Description = "Tools"
            },
            new CategoryDto{
                Id = 4,
                Description = "Mix"
            },
        }; 
        #endregion

        [HttpPost]
        public IActionResult Create([FromBody] CategoryForCreationDto category) //DTOs vs Entidades. No todas las empresas lo usan del mismo modo.
        {
            //Validaciones de request
            if (category is null
               || string.IsNullOrWhiteSpace(category.Description))
                return BadRequest("Description is mandatory");
            //Aca creariamos la categoria en nuestra BBDD
            var catCreatedOnDb = new CategoryDto { 
                Id = new Random().Next(1, 100),
                Description = category.Description
            };
            //Todo salio bien, es un POST, asi que vamos a devolver CREATED
            return Created($"/categories/{catCreatedOnDb.Id}", catCreatedOnDb);
            //Ver en POSTMAN que el response tiene un header "Location"
        }

        [HttpPut("{id}")]
        public IActionResult FullUpdate(int id, [FromBody] CategoryDto category) //No necesitaria un DTO para Update? Si, quizas podes hasta arrancar con uno que se llame Upsert.
        {
            //Validaciones de request
            if (category is null
               || string.IsNullOrWhiteSpace(category.Description))
                return BadRequest("Description is mandatory");

            if (category.Id != 0 && category.Id != id)
                return BadRequest("URL id does NOT match Body one."); //Nosotros decidimos hacer esto, pero no significa que sea asi en todos lados.
            category.Id = id;
            //Actualizacion Completa de nuestra entidad
            return Ok(category);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]CategoriesFilterDto dto)
        {
            //Calculamos el skip en una linea separada. Usualmente si el calculo es "complejo", ponerlo separado ayuda a la comprension del codigo.
            //E incluso a debuggear. El compilador se encarga de sintetizarlo al maximo cuando hacemos "build" asi que no ganamos nada quitando lineas en este caso.
            // Pero ayudamos a un colega a que entienda mas rápido el código.
            var skip = (dto.Page - 1) * dto.PageSize;
            var categoriesToReturn = 
                CategoriesStub
                    .Where(c => string.IsNullOrEmpty(dto.DescriptionContains) || c.Description.Contains(dto.DescriptionContains))
                    .Skip(skip).Take(dto.PageSize);

            // Para el ordenamiento hay muchas tecnicas. Lo que mas existe son "Enums" o Ids Asignados a ciertas columnas 
            // Existen formas mas avanzadas usando Reflection o utilizando metodos de la clase Expression.
            
            if(dto.OrderBy == "Description desc")
                categoriesToReturn = categoriesToReturn.OrderByDescending(c => c.Description);

            return Ok(categoriesToReturn);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Validaciones de request
            if (id == 0)
                return BadRequest("ID is mandatory and must be greater than 0");

            //imaginemos que tenemos 100 Categorias y cualquier ID mayor no existe
            // (esto normalmente se hace una busqueda en BBDD y la misma no arroja resultados)
            if (id > 100)
                return NotFound($"The category with id '{id}' was not found."); //Algunas empresas hasta no dan tanto detalle en la respuesta de sus APIs para no mostrar el diseño de sus APIs

            //Aca obtendriamos la categoria desde la base de datos
            return Ok(new Category
            {
                Id = id,
                Description = "Searched By Id"
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Intentamos eliminar la categoria. En caso exitoso, devolvemos NoContent
            
            return NoContent();
        }

        [HttpPost("error")]
        public IActionResult Error()
        {
            throw new InvalidOperationException("You cannot send errors to the app."); //Como estamos en Development nos muestra el stacktrace.
            //Intentemos cambiar el ambiente a PRODUCTION y veamos el resultado.
        }

    }
}