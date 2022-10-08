using System.Text.Json.Serialization;

namespace NetCoreCourse.FirstExample.WebApp.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }

        //Esta lista es lo unico que necesitamos para que EF CORE entienda que la relacion entre categoria y cosas
        // es 1:N. Es una de las muchas convenciones que tiene EF CORE para representar este tipo de relaciones. 
        // Siempre que sea necesario, se pueden reemplazar estas convenciones configurando las relaciones en el DbContext.
        public IList<Thing> Things { get; set; }
    }
}
