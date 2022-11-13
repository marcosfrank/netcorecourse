namespace NetCoreCourse.MvcApp.Services
{
    public class DelayService : IDelayService
    {
        public Task<string> Delay(int milisecods)
        {
            Thread.Sleep(milisecods);
            return Task.FromResult($"This method was delayed by {milisecods}ms.");
        }
    }
}
