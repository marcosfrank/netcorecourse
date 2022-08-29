namespace NetCoreCourse.FirstExample.WebApp.Models
{
    public class RandomServiceResponse
    {
        public RandomServiceResponse(RandomServiceValues fromController, RandomServiceValues fromService)
        {
            FromController = fromController;
            FromService = fromService;
        }

        public RandomServiceValues FromController { get; set; }
        public RandomServiceValues FromService { get; set; }
    }
}
