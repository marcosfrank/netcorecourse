using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.MvcApp.Services;
using System.Diagnostics;

namespace NetCoreCourse.MvcApp.Controllers.api
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> logger;
        private readonly IDelayService delayService;

        public TasksController(ILogger<TasksController> logger,
                                IDelayService delayService)
        {
            this.logger = logger;
            this.delayService = delayService;
        }

        

        [HttpGet("tasksstatuses")]
        public IActionResult TaskStatuses()
        {
            Task tarea = new Task(() => {
                Debug.WriteLine("Comenzo");
                Thread.Sleep(5000);
            Debug.WriteLine("Finalizo");
            });
            var primerEstado = tarea.Status.ToString();

            tarea.Start();
            var segundoEstado = tarea.Status.ToString();
            
            Thread.Sleep(1000);
            var tercerEstado = tarea.Status.ToString();

            tarea.Wait();
            var cuartoEstado = tarea.Status.ToString();

            return Ok(new { 
                primerEstado,
                segundoEstado,
                tercerEstado,
                cuartoEstado,
                tarea.Id
            });
        }

        [HttpGet("taskrun")]
        public IActionResult TaskRun()
        {
            Task tarea = Task.Run(() => {
                logger.LogInformation("Comenzo");
                Thread.Sleep(5000);
                logger.LogInformation("Finalizo");
            });
            var primerEstado = tarea.Status.ToString();

            Thread.Sleep(1000);
            var segundoEstado = tarea.Status.ToString();

            return Ok(new
            {
                primerEstado,
                segundoEstado,
                tarea.Id
            });
        }

        [HttpGet("taskrun2")]
        public IActionResult TaskRun2()
        {
            Task tarea = Task.Run(() => {
                Debug.WriteLine("Comenzo");
                Thread.Sleep(5000);
                Debug.WriteLine("Finalizo");
            });

            return Ok();
        }

        [HttpGet("continuewith")]
        public IActionResult ContinueWith()
        {
            
            Task.Run(() => {
                Debug.WriteLine("Tarea 1");
                Thread.Sleep(1500);
            }).ContinueWith((anterior) => {
                Debug.WriteLine("Tarea 2");
            }).ContinueWith((segunda) => {
                Debug.WriteLine("Tarea 3");
            });

            return Ok();
        }

        [HttpGet("collections")]
        public IActionResult Collections() //Mostrar este metodo debuggeando. Ejecutar hasta cada definicion de las tasks
        {

            Task[] tasks = new Task[3]
            {
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 1", 3000)),
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 2", 2000)),
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 3", 1000))
            };

            Task.WaitAll(tasks); //retorna void cuando todas las tareas finalizan

            Task[] tasks2 = new Task[3]
            {
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 1", 3000)),
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 2", 2000)),
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 3", 1000))
            };

            Task.WaitAny(tasks2); // retorna void cuando la primera tarea finaliza
            Debug.WriteLine("Entre Waits");
            Task.WaitAll(tasks2);

            Task[] tasks3 = new Task[3]
            {
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 1", 3000)),
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 2", 2000)),
                Task.Factory.StartNew(() => DebugWithDelay("Tarea 3", 1000))
            };

            Task.WhenAll(tasks3) // retorna Task, entonces podemos hacer un Continue With o esperarla.
                .ContinueWith((t) => Debug.WriteLine("Terminaron las tareas")); 


            return Ok();
        }

        private void DebugWithDelay(string message, int delay)
        {
            Debug.WriteLine($"{message} - Inicio");
            Thread.Sleep(delay);
            Debug.WriteLine($"{message} - Fin");
        }

        [HttpGet("asyncawait")]
        public async Task<IActionResult> AsyncAwait()//Ver async en la firma de este metodo.
        {
            var firstString = await delayService.Delay(1000); //ver await en este metodo
            return Ok(firstString); //Porque si devolvemos OK, estamos en un Task<IActionResult> 
        }

    }
}
