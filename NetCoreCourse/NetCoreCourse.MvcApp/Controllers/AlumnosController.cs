using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.MvcApp.Extensions;
using NetCoreCourse.MvcApp.Models;
using NetCoreCourse.MvcApp.Services;

namespace NetCoreCourse.MvcApp.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly IAlumnoService service;

        public AlumnosController(IAlumnoService service)
        {
            this.service = service;
        }

        // GET: Alumnos
        public IActionResult Index(string search)
        {
            var dbAlumnos = service.GetAll(search);
            var viewmodels = dbAlumnos.ToViewModels();
            return View(viewmodels);
        }

        // GET: Alumnos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = service.GetById(id.Value);
            if (alumno == null)
            {
                return NotFound();
            }

            ViewBag.AsignadoEnDetails = "Este string lo asigne en el action Details del controller";

            return View(alumno.ToViewModel());
        }

        // GET: Alumnos/Create
        public IActionResult Create() //Que raro que es este controller verdad?
        {
            return View();
        }

        // POST: Alumnos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlumnoViewModel alumnoViewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", alumnoViewModel);

            var list = service.GetAll(alumnoViewModel.Nombre); //simulemos que validamos duplicados.
            if (list.Any(a => a.Nombre == alumnoViewModel.Nombre
                    && a.Apellido == alumnoViewModel.Apellido))
            {
                ModelState.AddModelError(String.Empty, "Ya existe un alumno con ese nombre y apellido.");
                return View(alumnoViewModel);
            }

            var entity = alumnoViewModel.ToEntity();
            service.Save(entity);
            return RedirectToAction(nameof(Index));
        }

        // GET: Alumnos/Edit/5
        public IActionResult Edit(int? id) //La accion se llama Edit, pero es un GET? Como es eso?
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = service.GetById(id.Value);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno.ToViewModel());
        }

        // POST: Alumnos/Edit/5
        [HttpPost] //POST para update? REST es un estilo. No siempre se respeta y esto no significa que este mal.
        [ValidateAntiForgeryToken] //Junto con el form TAG nos ayudan a prevenir XSRF/CSRF.
        public async Task<IActionResult> Edit(int id, AlumnoViewModel alumnoViewModel)
        {
            if (id != alumnoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                service.Update(alumnoViewModel.ToEntity());

                return RedirectToAction(nameof(Index));
            }
            return View(alumnoViewModel);
        }

        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var alumno = service.GetById(id.Value);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno.ToViewModel());
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = service.GetById(id);
            if (alumno is null)
            {
                return NotFound();
            }

            service.Delete(alumno);
            return RedirectToAction(nameof(Index));
        }
    }
}