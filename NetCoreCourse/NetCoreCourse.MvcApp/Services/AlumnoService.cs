using NetCoreCourse.MvcApp.Entities;

namespace NetCoreCourse.MvcApp.Services
{
    public class AlumnoService : IAlumnoService
    {
        #region Datos de Prueba
        private List<Alumno> stubs = new List<Alumno> { 
            new Alumno{
                Id = 1,
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 25,
                FechaDeCreacion = new DateTime(2022,10,5),
                FechaDeModificacion = new DateTime(2022,10,5),
            },
            new Alumno{
                Id = 2,
                Nombre = "Pedro",
                Apellido = "Juarez",
                Edad = 34,
                FechaDeCreacion = new DateTime(2022,9,4),
                FechaDeModificacion = new DateTime(2022,10,5),
            },
            new Alumno{
                Id = 3,
                Nombre = "Antonio",
                Apellido = "Suarez",
                Edad = 75,
                FechaDeCreacion = new DateTime(2020,10,5),
                FechaDeModificacion = new DateTime(2021,6,6),
            },
        };
        #endregion

        public void Delete(Alumno alumno)
        {
            stubs.Remove(alumno);
        }

        public bool Exists(int id)
        {
            return stubs.Any(stub => stub.Id == id);
        }

        public List<Alumno> GetAll(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return stubs;

            var loweredSearch = search.ToLowerInvariant();
            var filteredResults = stubs
                .Where(a => a.Nombre.ToLowerInvariant().Contains(loweredSearch)
                        || a.Apellido.ToLowerInvariant().Contains(loweredSearch))
                .ToList();
            return filteredResults;
        }

        public Task<List<Alumno>> GetAllAsync()
        {
            return Task.FromResult(stubs);
        }

        public Alumno GetById(int id)
        {
            return stubs.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Alumno alumno)
        {
            alumno.Id = stubs.Max(x => x.Id) + 1;
            stubs.Add(alumno);
        }

        public void Update(Alumno alumno)
        {
            var dbAlumno = GetById(alumno.Id);
            dbAlumno.Apellido = alumno.Apellido;
            dbAlumno.Edad = alumno.Edad;
            dbAlumno.Nombre = alumno.Nombre;
            dbAlumno.FechaDeModificacion = DateTime.UtcNow;
        }
    }
}
