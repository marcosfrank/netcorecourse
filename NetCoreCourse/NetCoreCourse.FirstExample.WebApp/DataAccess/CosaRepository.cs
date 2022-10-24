using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public class CosaRepository : RepositorioBase<Cosa>, ICosaRepository
    {
        public override Cosa Add(Cosa entidad)
        {
            throw new NotSupportedException();
        }
    }
}
