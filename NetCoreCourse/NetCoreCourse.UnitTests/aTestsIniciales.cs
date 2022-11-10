using NetCoreCourse.FirstExample.WebApp.Statics;

namespace NetCoreCourse.UnitTests
{
    [TestClass]
    public class aTestsIniciales
    {
        [TestMethod]
        public void ElOperadorSuma_Funciona()
        {
            //arrange
            var valor1 = 2;
            var valor2 = 1;
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

        [TestMethod]
        [Ignore("Revisemos con el equipo porque falla.")]
        public void EsteTest_Va_a_ignorarse()
        {
            //arrange
            var valor = 7;
            //act
            var resultado = valor.IsEven();
            //assert
            Assert.IsTrue(resultado); //Aca nos falla
        }
    }
}