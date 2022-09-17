using Microsoft.AspNetCore.Mvc;
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
            return thingsContext.Categories.Where(c => c.Things.Any()).ToList();
        }
    }
}