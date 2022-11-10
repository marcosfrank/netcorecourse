using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.Controllers.WebAPI;
using NetCoreCourse.FirstExample.WebApp.Dto;
using NetCoreCourse.FirstExample.WebApp.Entities;

namespace NetCoreCourse.UnitTests
{
    [TestClass]
    public class WebApiControllerTests
    {
        private WebApiController target = new WebApiController();
        //Como no tiene dependencias vamos a ver que es dificil testearlo
        // La lista de datos de prueba esta en la misma clase controller, por lo que no podemos manejar desde afuera como se comporta

        [TestMethod]
        public void CreateMethod_Returns_Created()
        {
            var dto = new CategoryForCreationDto
            {
                Description = "Test"
            };

            var result = target.Create(dto);

            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public void CreateMethod_Assigns_Id_From_0_To_100()
        {
            var dto = new CategoryForCreationDto
            {
                Description = "Test"
            };

            var result = target.Create(dto);
            var parsedObject = (CreatedResult)result;
            var returnedCategory = (Category)parsedObject.Value;

            Assert.IsNotNull(returnedCategory);
            Assert.IsTrue(returnedCategory.Id >= 1 && returnedCategory.Id <= 100);
        }

        [TestMethod]
        public void CreateMethod_Returns_BadRequest_WithEmpty_Description()
        {
            var dto = new CategoryForCreationDto(); //Empty Description

            var result = target.Create(dto);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }



        //TODO: Hacer los UnitTests de GetById de AlumnosController
    }
}