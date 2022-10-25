namespace NetCoreCourse.FirstExample.WebApp.Entities
{
    /// <summary>
    /// Esta es la clase base de la cual todas nuestras entidades deberian heredar
    /// Solo lo agregue a la clase Thing
    /// </summary>
    public abstract class EntityBase
    {
        public int Id { get; set; }
    }
}
