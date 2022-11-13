namespace NetCoreCourse.MvcApp.Services
{
    public interface IDelayService
    {
        Task<string> Delay(int milisecods);
    }
}
