namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    public interface IUnitOfWork
    {
        IThingRepository ThingRepository { get; }

        int CompleteAsync();
    }
}