using NetCoreCourse.MvcApp.Entities;
using NetCoreCourse.MvcApp.Models;

namespace NetCoreCourse.MvcApp.Extensions
{
    /// <summary>
    /// Se animan a utilizar Automapper en vez de estos extensions methods?
    /// <see cref="https://docs.automapper.org/en/stable/index.html"/>
    /// </summary>
    public static class AlumnoExtensions
    {
        public static AlumnoViewModel ToViewModel(this Alumno alumno)
        {
            return new AlumnoViewModel { 
                Id = alumno.Id,
                Apellido = alumno.Apellido,
                Edad = alumno.Edad,
                Nombre = alumno.Nombre
            };
        }

        public static List<AlumnoViewModel> ToViewModels(this List<Alumno> alumnos)
        {
            var list = new List<AlumnoViewModel>();
            alumnos.ForEach(a => list.Add(a.ToViewModel()));

            return list;
        }

        public static Alumno ToEntity(this AlumnoViewModel alumno)
        {
            return new Alumno 
            {
                Id = alumno.Id,
                Apellido = alumno.Apellido,
                Edad = alumno.Edad,
                Nombre = alumno.Nombre
            };
        }
    }
}
