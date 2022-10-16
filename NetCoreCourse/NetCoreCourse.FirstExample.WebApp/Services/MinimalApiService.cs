using NetCoreCourse.FirstExample.WebApp.Dto;

namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public class MinimalApiService : IMinimalApiService
    {
        public string Execute(object request)
        {
            var castedRequest = (MinimalApiRequest)request; // Esto es un casteo.
            return $"String: {castedRequest.S} | Integer {castedRequest.I} |" +
                $"Decimal: {castedRequest.D} | Datetime: {castedRequest.Dt}";
        }
    }
}
