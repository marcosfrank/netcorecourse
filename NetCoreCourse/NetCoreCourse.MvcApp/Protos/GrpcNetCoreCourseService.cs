using Grpc.Core;

namespace NetCoreCourse.MvcApp.Protos
{
    //De donde sale la clase NetCoreCourseServiceBase????
    public class GrpcNetCoreCourseService : NetCoreCourseService.NetCoreCourseServiceBase
    //Verificar que tenemos que llamar al base.
    {
        private readonly ILogger<GrpcNetCoreCourseService> logger;

        public GrpcNetCoreCourseService(ILogger<GrpcNetCoreCourseService> logger)
        {
            this.logger = logger;
        }

        //override? Significa que los metodos como estan definidos en la clase NetCoreCourseServiceBase?
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            logger.LogInformation("Le decimos hola a {Name}", request.Name);
            return Task.FromResult(new HelloReply
            {
                Message = "Hola " + request.Name + " | El campo extra es: " + request.ExtraField
            });
        }
    }
}