using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreCourse.MvcApp.Controllers;
using NetCoreCourse.MvcApp.Entities;
using NetCoreCourse.MvcApp.Models;
using NetCoreCourse.MvcApp.Services;

namespace NetCoreCourse.XUnitTests
{
    /*
     * Veamos Fluent Assertions
     */
    public class MvcAlumnosControllerTests
    {
        private AlumnosController target;
        private Mock<IAlumnoService> mockAlumnoService;

        public MvcAlumnosControllerTests()
        {
            mockAlumnoService = new Mock<IAlumnoService>();
            //GetAll
            mockAlumnoService.Setup(x => x.GetAll(It.IsAny<string>())).Returns(new List<Alumno> { 
                new Alumno{ 
                    Nombre = "Test",
                    Apellido = "Test Apellido",
                }
            });

            target = new AlumnosController(
                mockAlumnoService.Object
            );
        }

        [Fact]
        public void Create_Without_Parameters_Returns_A_View_And_The_View_Is_Create()
        {
            var result = target.Create();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().BeNull();
        }

        [Fact]
        public void Index_Should_Return_View_With_Alumnos()
        {
            var search = string.Empty;

            var result = target.Index(search);
            
            //Assert del tipo de respuesta
            result.Should().BeOfType<ViewResult>();

            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();

            result.As<ViewResult>().Model.Should().BeOfType<List<AlumnoViewModel>>();
            //Assert de los valores de las respuestas
            viewResult.Model.As<List<AlumnoViewModel>>()
                .Should().Contain(a => a.Nombre == "Test");
        }


        [Fact]
        public void CreateMethod_ConError_Retorna_Create_View()
        {
            target.ModelState.AddModelError(String.Empty, "Simulando Que tiene un error");
            var alumno = new AlumnoViewModel();

            var result = (ViewResult)target.Create(alumno);

            result.ViewName.Should().Be("Create");
        }

        [Fact]
        public void CreateMethod_ConElMismoAlumno_Retorna_Error_En_ModelState()
        {
            var alumno = new AlumnoViewModel{ Nombre= "Test", Apellido = "Test Apellido" }; //Mismo que configuramos

            var result = target.Create(alumno);
            
            target.ModelState.Count.Should().BeLessThan(2).And.BeGreaterThan(0);

            //Otras Aserciones
            result.Should().BeOfType(typeof(ViewResult));
            var viewResult = result as ViewResult;
            viewResult.ViewName.Should().BeNull();
        }

        [Fact]
        public void CreateMethod_ConAlumnoValida_Retorna_RedirectToActionResult()
        {
            var alumno = new AlumnoViewModel { Nombre = "Nuevo", Apellido = "No Existe" }; 

            var resultado = target.Create(alumno);

            //Assert tipo
            resultado.Should().BeOfType<RedirectToActionResult>();
            //Assert extra
            var redirectResult = (RedirectToActionResult)resultado;
            redirectResult.ActionName.Should().Be("Index");

        }
    }
}