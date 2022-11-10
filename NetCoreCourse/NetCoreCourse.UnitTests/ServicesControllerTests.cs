using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreCourse.FirstExample.WebApp.Controllers;
using NetCoreCourse.FirstExample.WebApp.Services;
using Newtonsoft.Json.Linq;
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
        private Mock<IForecastService> mockForecast; //porque este mock lo agregamos como campo privado de la clase?

        [TestInitialize]
        public void Init()
        {
            mockForecast = new Mock<IForecastService>();
            mockForecast.Setup(m => m.GetTemperature()).Returns(2);
            mockForecast.Setup(m => m.GetWeatherByCity("LanzaException")).Throws(() => new NotSupportedException());

            target = new ServicesController(
                //new Mock<IForecastService>().Object,
                mockForecast.Object,
                new Mock<ITransientRandomValueService>().Object,
                new Mock<IScopedRandomValueService>().Object,
                new Mock<ISingletonRandomValueService>().Object,
                Mock.Of<IServiceUsingServices>() //Veamos la diferencia en este caso. LINQ to mocks. Nos permite definirlo de forma mas funcional
            );

            // Vamos a hacerlo un poco mas interesante
            // y empecemos a configurar nuestras dependencias.


        }

        [TestMethod]
        public void GiveMeWeather_ConRosario_Deveulve_OK()
        {
            var ciudad = "Ciudad";

            var resultado = target.GiveMeWeather(ciudad);

            Assert.IsInstanceOfType(resultado, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GiveMeWeather_Devuelve_Null_Como_Valor()
        {
            var ciudad = "Ciudad";

            var resultado = target.GiveMeWeather(ciudad);
            var resultadoTransformado = (OkObjectResult)resultado;

            Assert.IsNull(resultadoTransformado.Value);
        }



        [TestMethod]
        public void GiveMeWeather_Llama_Al_Metodo_GetWeatherByCity_De_IForecastService()
        {
            var ciudad = "Prueba";

            target.GiveMeWeather(ciudad);

            mockForecast.Verify(x => x.GetWeatherByCity(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GiveMeWeather_Llama_Al_Metodo_GetWeatherByCity_De_IForecastService_MasEspecificos()
        {
            var ciudad = "Prueba";

            target.GiveMeWeather(ciudad);

            mockForecast.Verify(x => x.GetWeatherByCity("Prueba"), Times.Once); //veamos que aca especificamos cual es el string
        }

        

        [TestMethod]
        public void GetCurrentTemperature_Deveulve_Frio_Cuando_Temperatura_Es_Menor_A_10()
        {
            var resultado = target.GetCurrentTemperature();
            var resultadoTransformado = (OkObjectResult)resultado;

            Assert.AreEqual("Frio", resultadoTransformado.Value);
        }

        //Como hacemos con los demas? Es decir Templado y Caluroso?

        
        [TestMethod]
        public void GetCurrentTemperature_Deveulve_Frio_Cuando_Temperatura_Es_Menor_A_25()
        {
            mockForecast.Setup(m => m.GetTemperature()).Returns(20);

            var resultado = target.GetCurrentTemperature();
            var resultadoTransformado = (OkObjectResult)resultado;

            Assert.AreEqual("Templado", resultadoTransformado.Value);
        }

        [TestMethod]
        public void GetCurrentTemperature_Deveulve_Frio_Cuando_Temperatura_Es_Mayor_A_25()
        {
            mockForecast.Setup(m => m.GetTemperature()).Returns(45);

            var resultado = target.GetCurrentTemperature();
            var resultadoTransformado = (OkObjectResult)resultado;

            Assert.AreEqual("Caluroso", resultadoTransformado.Value);
        }



        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GiveMeWeather_Lanza_Excepcion()
        {
            
            var ciudad = "LanzaException";

            var resultado = target.GiveMeWeather(ciudad);

        }

        [TestMethod]
        public void GetCurrentTemperature_Multiple_Returns()
        {
            mockForecast.SetupSequence(x => x.GetTemperature())
                .Returns(1)
                .Returns(20)
                .Returns(30)
                .Throws(new IndexOutOfRangeException("Devolviste mas datos de los esperados")); //Nunca se va a ejecutar

            var temp1 = ((OkObjectResult)target.GetCurrentTemperature());
            var temp2 = ((OkObjectResult)target.GetCurrentTemperature());
            var temp3 = ((OkObjectResult)target.GetCurrentTemperature());

            Assert.AreEqual("Frio", temp1.Value);
            Assert.AreEqual("Templado", temp2.Value);
            Assert.AreEqual("Caluroso", temp3.Value);
        }

        [TestMethod]
        public void GetCurrentTemperature_Con_Propiedades()
        {
            var expectedDatetime = new DateTime(2022, 11, 8);
            mockForecast.Setup(x => x.LastWeatherReportDate).Returns(expectedDatetime);

            var temp1 = target.GetLastWeatherReportDate();

            Assert.AreEqual(temp1, expectedDatetime);
        }


        [TestMethod]
        public void GiveMeWeather_Assigns_Rosario_AsDefault_With_Callback()
        {
            string ciudadEnviada = string.Empty;
            mockForecast.Setup(x => x.GetWeatherByCity(It.IsAny<string>()))
                .Callback((string ciudad) => ciudadEnviada = ciudad); //Configuro para que me guarde la ciudad con la que estoy llamando al metodo.

            target.GiveMeWeather(); //Lo envio sin ciudad.

            Assert.AreEqual("Rosario", ciudadEnviada);
        }

    }
}
