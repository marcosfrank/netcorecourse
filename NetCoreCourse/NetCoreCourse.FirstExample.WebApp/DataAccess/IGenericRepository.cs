using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public interface IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        TEntity Add(TEntity entity);

        bool Delete(int id);

        TEntity Update(TEntity entity);

        TEntity GetById(int id);

        List<TEntity> GetAll();

        //Podemos agregar mas metodos, como Any, o GetConFiltros
    }
}
