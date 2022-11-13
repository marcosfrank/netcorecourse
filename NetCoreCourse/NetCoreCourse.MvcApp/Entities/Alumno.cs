namespace NetCoreCourse.MvcApp.Entities
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaDeCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaDeModificacion { get; set; } = DateTime.UtcNow;
    }
}
