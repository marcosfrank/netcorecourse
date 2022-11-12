using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NetCoreCourse.MvcApp.Entities;
using NetCoreCourse.MvcApp.Services;

namespace NetCoreCourse.MvcApp.Protos
{
    //De donde sale la clase NetCoreCourseServiceBase????
    public class GrpcNetCoreCourseService : NetCoreCourseService.NetCoreCourseServiceBase 
        //Verificar que tenemos que llamar al base.
    {
        private readonly ILogger<GrpcNetCoreCourseService> logger;
        private readonly IAlumnoService alumnosService;

        public GrpcNetCoreCourseService(ILogger<GrpcNetCoreCourseService> logger,
                                        IAlumnoService alumnoService)
        {
            this.logger = logger;
            this.alumnosService = alumnoService;
        }

        //override? Significa que los metodos como estan definidos en la clase NetCoreCourseServiceBase?
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            logger.LogInformation("Saying hello to {Name}", request.Name);
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<AlumnoResponse> DoSomethingForTheCourse(CourseRequest request, ServerCallContext context)
        {
            try
            {
                var alumno = alumnosService.GetById(request.AlumnoId);
                return Task.FromResult(new AlumnoResponse { 
                    Success = true,
                    Alumno = new AlumnoGrpc
                    {
                        Id = alumno.Id,
                        Name = alumno.Nombre
                    }
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Surgio un error al buscar el alumno.");
            }
            return Task.FromResult(new AlumnoResponse
            {
                Success = false
            });

        }

        public override async Task<AlumnosResponse> DoSomethingForTheCourse2(Empty request, ServerCallContext context)
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
