namespace NetCoreCourse.XUnitTests
{
    internal class FibonacciTarget
    {
        public FibonacciTarget()
        {
        }
        //TODO: Ahora los quiero ver refactorizar este metodo. Tenemos tests que nos cubren.
        public List<long> GetNumbers(int numeros)
        {
            var list = new List<long>();
            if (numeros == 0)
                return list;

            list.Add(1);

            for (int i = 1; i < numeros; i++)
            {
                var ultimoIndice = list.Count - 1;
                var indiceNumeroAnterior = ultimoIndice - 1;
                long siguienteNumero = 1;
                if(indiceNumeroAnterior >= 0)
                    siguienteNumero = list[indiceNumeroAnterior] + list[ultimoIndice];
                list.Add(siguienteNumero);
            }

            return list;
        }
    }
}