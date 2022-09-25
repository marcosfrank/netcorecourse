using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.DataAccess;
using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ThingsController : ControllerBase
    {
        private readonly ThingsContext thingsContext;

        public ThingsController(ThingsContext context)
        {
            thingsContext = context;
        }

        [HttpPost]
        public Thing Create([FromBody]Thing thing) //Tengamos presente que normalmente las entidades NO se utilizan como request de APIs
        {
            thingsContext.Things.Add(thing);
            thingsContext.SaveChanges();

            return thing; //Observar que devuelve el ID. Como es posible?
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            var cat = thingsContext.Things.Find(id);
            if(cat is null)
                return NotFound();

            thingsContext.Things.Remove(cat);
            thingsContext.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Thing>> GetAll()
        {
            return thingsContext.Things.ToList();
        }
    }
}