using NetCoreCourse.FirstExample.WebApp.Models;

namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public class ServiceUsingServices : IServiceUsingServices
    {
        private readonly ITransientRandomValueService transientService;
        private readonly IScopedRandomValueService scopedService;
        private readonly ISingletonRandomValueService singletonService;

        public ServiceUsingServices(
            ITransientRandomValueService transientService,
            IScopedRandomValueService scopedService,
            ISingletonRandomValueService singletonService
        )
        {
            this.transientService = transientService;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
        }
        public RandomServiceValues GetRandomValues()
        {
            return new RandomServiceValues
            {
                Transient = transientService.RandomValue,
                Scoped = scopedService.RandomValue,
                Singleton = singletonService.RandomValue
            };
        }
    }
}
