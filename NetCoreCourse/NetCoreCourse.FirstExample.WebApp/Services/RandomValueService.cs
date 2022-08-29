namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public class RandomValueService : ITransientRandomValueService, ISingletonRandomValueService, IScopedRandomValueService
    {
        public int RandomValue { get; }
        public RandomValueService()
        {
            RandomValue = new Random().Next(1, 1000);
        }
    }
}
