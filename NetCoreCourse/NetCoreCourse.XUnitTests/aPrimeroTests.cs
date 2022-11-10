using NetCoreCourse.FirstExample.WebApp.Dto;
using NetCoreCourse.FirstExample.WebApp.Services;
using NetCoreCourse.FirstExample.WebApp.Statics;

namespace NetCoreCourse.XUnitTests
{
    public class aPrimeroTests
    {
        [Fact]
        public void Es_2_Par()
        {
            var numero_2 = 2;

            var resultado = numero_2.IsEven();

            Assert.True(resultado);
        }

        [Theory] //Se imaginan como hacerlo con MSTests
        [InlineData(2, "dos")]
        [InlineData(4, "cuatro")]
        [InlineData(14, "catorce")]
        [InlineData(20, "veinte")]
        [InlineData(200, "doscientos")]
        public void Son_Par(int numero, string nombreNumero)
        {
            var resultado = numero.IsEven();

            Assert.True(resultado);
        }

        [Theory]
        [MemberData(nameof(TestingValues))]
        public void Minimal_Api_Service_Member_Data(MinimalApiRequest request, int index)
        {
            //index se agrego solo para demostrar que podemos recibir mas de un parametro
            var target2 = new MinimalApiService();
            request.I = index;

            var resultado = target2.Execute(request);

            Assert.Contains(request.S, resultado);
            Assert.Contains(request.D.ToString(), resultado);
            Assert.Contains(request.I.ToString(), resultado);
            Assert.Contains(request.Dt.ToString(), resultado);
        }

        public static TheoryData<MinimalApiRequest, int> TestingValues => new()
        {
            { new MinimalApiRequest{ S="1", D=1, Dt=new DateTime(1,1,1) }, 0 },
            { new MinimalApiRequest{ S="2", D=2, Dt=new DateTime(2,2,2) }, 1 },
            { new MinimalApiRequest{ S="3", D=3, Dt=new DateTime(3,3,3) }, 2 },
        };
    }
}