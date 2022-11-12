using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NetCoreCourse.MvcApp.Services;

namespace NetCoreCourse.MvcApp.Protos
{
    public class GrpcProtoAlumnoService : ProtoAlumnoService.ProtoAlumnoServiceBase
    {
        private readonly IAlumnoService alumnosService;

        public GrpcProtoAlumnoService(IAlumnoService alumnoService)
        {
            alumnosService = alumnoService;
        }

        public override Task<AlumnoResponse> GetById(CourseRequest request, ServerCallContext context)
        {
            try
            {
                var alumno = alumnosService.GetById(request.AlumnoId);
                return Task.FromResult(new AlumnoResponse
                {
                    Success = true,
                    Alumno = new AlumnoGrpc
                    {
                        Id = alumno.Id,
                        Name = alumno.Nombre
                    }
                });
            }
            catch
            {
                //Loguear excepcion.
                return Task.FromResult(new AlumnoResponse
                {
                    Success = false
                });
            }
        }

        //async! 
        public override async Task<AlumnosResponse> GetAll(Empty request, ServerCallContext context)
        {
            var alumnos = await alumnosService.GetAllAsync();
            var response = new AlumnosResponse { Success = true };
            alumnos.ForEach(a =>
            {
                response.Alumnos.Add(new AlumnoGrpc
                {
                    Id = a.Id,
                    Name = a.Nombre
                });
            });

            return response;
        }
    }
}