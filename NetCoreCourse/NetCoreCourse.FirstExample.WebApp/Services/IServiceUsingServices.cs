using NetCoreCourse.FirstExample.WebApp.Controllers;
using NetCoreCourse.FirstExample.WebApp.Models;

namespace NetCoreCourse.FirstExample.WebApp.Services
{
    public interface IServiceUsingServices
    {
        RandomServiceValues GetRandomValues();

        void GetRandomValues2(RandomServiceValues values);

        void GetRandomValues3(Estructura values);
    }
}
