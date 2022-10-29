using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NetCoreCourse.MvcApp.Models
{
    public class AlumnoViewModel
    {
		public int Id { get; set; }
        [MaxLength(10, ErrorMessage = "El nombre solo puede tener 10 caracteres.")] //DataAnnotations. Intentemos quitar el max lenght del HTML.
        public string Nombre { get; set; }
        [Display(Name = "Todos los apellidos")]
        public string Apellido { get; set; }
        [Range(0,100, ErrorMessage = "La edad debe estar entre 0 y 100")]
        public int Edad { get; set; }
    }
}
