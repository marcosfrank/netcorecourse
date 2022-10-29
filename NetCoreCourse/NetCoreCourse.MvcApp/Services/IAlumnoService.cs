using NetCoreCourse.MvcApp.Entities;

namespace NetCoreCourse.MvcApp.Services
{
    public interface IAlumnoService
    {
        List<Alumno> GetAll();

        Alumno? GetById(int id);

        void Save(Alumno alumno);
        void Update(Alumno alumno);

        bool Exists(int id);

        void Delete(Alumno alumno);
    }
}
