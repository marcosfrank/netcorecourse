namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ThingsContext context;
        public IThingRepository ThingRepository { get; private set;}
        public UnitOfWork(ThingsContext context)
        {
            this.context = context;
            ThingRepository = new ThingRepository(context);
        }
        public int CompleteAsync()
        {
            return context.SaveChanges();
        }
    }
}
