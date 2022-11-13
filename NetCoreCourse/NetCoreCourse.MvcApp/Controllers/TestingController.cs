using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace NetCoreCourse.MvcApp.Controllers
{
    public class TestingController : Controller
    {
        //public string Index()
        //{
        //    return "Bienvenidos al curso de Net Core";
        //}

        /// <summary>
        /// Como este metodo es un Get, los parametros son pasados por QueryString
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="cantidadAlumnos"></param>
        /// <returns></returns>
        public string Curso(string nombre, int cantidadAlumnos = 25)
        {
            return HtmlEncoder.Default.Encode($"Este curso es {nombre}, y tiene {cantidadAlumnos} alumnos");
        }

        //Ahora usemos vistas.
        public IActionResult Index()
        {
            return View();
        }

        //Ahora usemos vistas.
        public IActionResult ViewDataExample(string curso, int alumnos)
        {
            ViewData["Curso"] = curso;
            ViewData["Alumnos"] = alumnos; 
            return View();
        }
    }
}
