using NetCoreCourse.FirstExample.WebApp.Dto;

namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public class MinimalApiService : IMinimalApiService
    {
        public string Execute(MinimalApiRequest request)
        {
            return $"String: {request.S} | Integer {request.I} | " +
                $"Decimal: {request.D} | Datetime: {request.Dt}";
        }
    }
}
