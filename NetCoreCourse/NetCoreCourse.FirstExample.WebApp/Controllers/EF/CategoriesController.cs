using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetCoreCourse.FirstExample.WebApp.DataAccess;
using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ThingsContext thingsContext;

        public CategoriesController(ThingsContext context)
        {
            thingsContext = context;
        }

        [HttpPost]
        public void Create()
        {
            var categories = new List<Category> {
                 new Category {
                    Description = "Books"
                },
                 new Category {
                    Description = "Tools"
                },
                 new Category {
                    Description = "Others"
                }
            };
            thingsContext.Categories.AddRange(categories);
            thingsContext.SaveChanges();
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            return thingsContext.Categories.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Category? GetCategoryById(int id)
        {
            return thingsContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        // Ver que este metodo nos devuelve ActionResult<Category>.
        // Esto nos permite devolver metodos como BadRequest & NotFound que utilizan diferentes estados del protocolo HTTP.
        // Esto lo vamos a ver con mas profundidad en el modulo de HTTP.
        [HttpPatch]
        [Route("{id}")]
        public ActionResult<Category> UpdateCategoryDescription (int id, [FromBody]Category category) //Tengamos presente que normalmente las entidades NO se utilizan como request de APIs
        {
            if (string.IsNullOrWhiteSpace(category?.Description))
                return BadRequest("Description must be provided.");

            var dbCategory = thingsContext.Categories.FirstOrDefault(c => c.Id == id);
            if (dbCategory == null)
                return NotFound();

            dbCategory.Description = category.Description;
            thingsContext.SaveChanges(); //El ChangeTracker conoce a esta entidad, por lo que llamando al metodo SaveChanges podremos impactar los cambios en BBDD.

            return dbCategory;
        }

        [HttpGet]
        [Route("filters")]
        public List<Category> GetCategoriesByFilters([FromQuery]string description)
        {
            return thingsContext.Categories.Where(c => c.Description.Contains(description)).ToList();
        }

        [HttpGet]
        [Route("withItems")]
        public List<Category> GetCategoriesThatHasThings()
        {
            return thingsContext.Categories
                .Include(c => c.Things)
                .Where(c => c.Things.Any())
                .ToList();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Category> FullUpdate(int id, [FromBody]Category category)
        {
            if (id <= 0)
                return BadRequest("Id must be higher than 0");

            //En proyectos mas grandes es posible que primero busquen si el registro existe, si no existe, se puede devolver NotFound(). Si existe, se realiza el update.

            category.Id = id; 
            var savedEntity = thingsContext.Update(category);
            thingsContext.SaveChanges();

            return savedEntity.Entity;
        }

        [HttpGet]
        [Route("first")]
        public ActionResult<Category> GetFirstCategory()
        {
            //En esta accion vamos a probar varias funcionalidades.
            //FirstOrDefault -> Devuelve (null) si el registro no existe
            //First          -> lanza excepcion si el registro no existe
            //Permiten filtrar. Ver debajo.
            var catNotReturned = thingsContext.Categories
                .FirstOrDefault(cat => cat.Description.StartsWith("NonExisting"));
            if (catNotReturned is not null)
                return StatusCode(500);

            return thingsContext.Categories.First(); 
        }

        [HttpGet]
        [Route("rawsql")]
        public List<Category> RawSql()
        {
            return thingsContext
                .Categories
                .FromSqlRaw("SELECT Id, Description FROM Categories")
                .ToList();
        }

        [HttpGet]
        [Route("storedprocedure")]
        public List<Category> FromStoredProcedure(string description)
        {
            //Normalmente la creacion y actualizacion de Stored Procedures son parte de migraciones.
            /*
             CREATE OR ALTER PROCEDURE GetCategories 
	            @Description nvarchar(100) = NULL
             AS
            BEGIN
	            SELECT Id, [Description] FROM Categories
	            WHERE @Description IS NULL
	            OR [Description] = @Description;
            END;
             
             */

            object parameterValue = string.IsNullOrWhiteSpace(description)?
                                            DBNull.Value :
                                            description;
            var sqlParameter = new SqlParameter[] { new SqlParameter("Description", parameterValue) };

            return thingsContext
                    .Categories
                    .FromSqlRaw($"GetCategories @Description", sqlParameter)
                    .ToList();
        }
    }
}