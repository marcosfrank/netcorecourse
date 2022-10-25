using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public interface IThingRepository : IGenericRepository<Thing>
    {
        //Aca podriamos tener otros metodos relacionados solo a thing
        //GetByCategogyId(int categoryId) -> Por Ejemplo
    }
}
