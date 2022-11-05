using System.ComponentModel.DataAnnotations;

namespace NetCoreCourse.FirstExample.WebApp.Dto
{
    public class ValidationsDto
    {
        [Required]
        public string Filter { get; set; }
    }
}
