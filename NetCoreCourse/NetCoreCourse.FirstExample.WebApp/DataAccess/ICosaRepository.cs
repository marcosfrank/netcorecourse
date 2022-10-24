using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public interface ICosaRepository : IGenericRepository<Cosa>
    {
    }

    public interface IGenericRepository<T>
        where T : EntidadBase
    {
        T Add(T entidad);
        //get
        //
    }
}
