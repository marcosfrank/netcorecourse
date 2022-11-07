using NetCoreCourse.FirstExample.WebApp.Statics;

namespace NetCoreCourse.UnitTests
{
    [TestClass]
    public class TestsIniciales
    {
        [TestMethod]
        public void ElOperadorSuma_Funciona()
        {
            //arrange
            var valor1 = 1;
            var valor2 = 2;
            //act
            var suma = valor1 + valor2;
            //assert
            Assert.AreEqual(3, suma);
        }

        [TestMethod]
        public void ElNumero3_No_Es_Par()
        {
            //arrange
            var valor = 3;
            //act
            var resultado = valor.IsEven();
            //assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ElNumero2_Es_Par()
        {
            //arrange
            var valor = 2;
            //act
            var resultado = valor.IsEven();
            //assert
            Assert.IsTrue(resultado);
        }


    }
}