using NetCoreCourse.FirstExample.WebApp.Dto;

namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public interface IMinimalApiService
    {
        string Execute(MinimalApiRequest request);
    }
}
