using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public class ThingRepository : GenericRepository<Thing>, IThingRepository
    {
        public ThingRepository(ThingsContext context) 
            : base(context) //Pasamos el contexto al ctor del padre.
        {
        }
    }
}
