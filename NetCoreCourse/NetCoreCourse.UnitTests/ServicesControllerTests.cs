using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreCourse.FirstExample.WebApp.Controllers;
using NetCoreCourse.FirstExample.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreCourse.UnitTests
{
    [TestClass]
    public class ServicesControllerTests
    {
        private ServicesController target;

        [TestInitialize]
        public void Init()
        {
            target = new ServicesController(
                Mock.Of<IForecastService>(),
                Mock.Of<ITransientRandomValueService>(),
                Mock.Of<IScopedRandomValueService>(),
                Mock.Of<ISingletonRandomValueService>(),
                Mock.Of<IServiceUsingServices>()
            );
        }

        [TestMethod]
        public void ElClimaDeRosario_Deveulve_OK()
        {
            var ciudad = "Rosario";

            var resultado = target.GiveMeWeather(ciudad);

            Assert.IsInstanceOfType(resultado, typeof(OkObjectResult));
        }

        [TestMethod]
        public void ElClimaDeRosario_Deveulve_Null_Como_Valor()
        {
            var ciudad = "Rosario";

            var resultado = target.GiveMeWeather(ciudad);
            var resultadoTransformado = (OkObjectResult)resultado;
            
            Assert.IsNull(resultadoTransformado.Value);
        }
    }
}
