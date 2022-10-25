using Microsoft.EntityFrameworkCore;
using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        protected ThingsContext context;
        internal DbSet<TEntity> dbSet; //DbSet correspondiente a la entidad de este repositorio.

        public GenericRepository(ThingsContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            var savedEntity = dbSet.Add(entity);
            return savedEntity.Entity;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException(); //Implementar
        }

        public List<TEntity> GetAll()
        {
            return dbSet.ToList(); //Implementar
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException(); //Implementar
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException(); //Implementar
        }
    }
}
