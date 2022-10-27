using NetCoreCourse.FirstExample.WebApp.Dto;

namespace NetCoreCourse.FirstExample.WebApp.Handlers
{
    public interface IJwtHandler
    {
        string GenerateToken(UserForLoginDto user, IEnumerable<string> roles);
    }
}