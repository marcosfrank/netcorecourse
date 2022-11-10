using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreCourse.XUnitTests
{
    public class TDDTests
    {
        /*
         1- Hacer una maquina que me determine si un número es 0, par o impar (sabemos que el 0 se considera par :lol)
         2- Hacer un Fibonacci hasta X numeros
         3- Controller que haga una transferencia bancaria
            - Si alguno de los usuarios no existe NotFound
            - Si no tiene dinero el emisor BadRequest
            - Ok
         */

        //TODO: Realizar el Ejercicio 3 con TDD.
        //      Utilizar las siguientes interfaces si los ayuda:
        //public interface IBankAccountService
        //{
        //    decimal GetBalance(int userId);
        //}

        //public interface IUserService
        //{
        //    decimal GetById(int userId);
        //}


    }

    


    public class FibonacciTests
    {

        [Fact]
        public void ElMetodo_con_parametro_0_devuelve_una_lista_vacia()
        {
            var numbers = new FibonacciTarget().GetNumbers(0);

            numbers.Should().BeEmpty();
        }

        [Fact]
        public void ElMetodo_con_parametro_1_devuelve_la_lista_esperada()
        {
            var numbers = new FibonacciTarget().GetNumbers(1);

            numbers.Should().ContainSingle();
            numbers[0].Should().Be(1);
        }

        [Fact]
        public void ElMetodo_con_parametro_2_devuelve_la_lista_esperada()
        {
            var numbers = new FibonacciTarget().GetNumbers(2);

            numbers.Should().HaveCount(2);
            numbers[0].Should().Be(1);
            numbers[1].Should().Be(1);
        }


        [Fact]
        public void ElMetodo_con_parametro_5_devuelve_la_lista_esperada()
        {
            var numbers = new FibonacciTarget().GetNumbers(5);

            numbers.Should().HaveCount(5);
            numbers[0].Should().Be(1);
            numbers[1].Should().Be(1);
            numbers[2].Should().Be(2);
            numbers[3].Should().Be(3);
            numbers[4].Should().Be(5);
        }


        [Fact]
        public void ElMetodo_con_parametro_6_devuelve_la_lista_esperada()
        {
            var numbers = new FibonacciTarget().GetNumbers(6);

            numbers.Should().HaveCount(6);
            numbers[0].Should().Be(1);
            numbers[1].Should().Be(1);
            numbers[2].Should().Be(2);
            numbers[3].Should().Be(3);
            numbers[4].Should().Be(5);
            numbers[5].Should().Be(8);
        }

        [Fact]
        public void ElMetodo_con_parametro_1000_devuelve_la_lista_esperada()
        {
            var numbers = new FibonacciTarget().GetNumbers(45);

            numbers.Should().HaveCount(45);

            numbers[39].Should().Be(102334155);
        }

        //TODO: Implementar este test
        [Fact(Skip = "Este test lo tienen que hacer funcionar los alumnos.")]
        public void ElMetodo_con_parametro_negativo_lanza_InvalidOperationException()
        {
            var target = new FibonacciTarget();

            target.Invoking(t => t.GetNumbers(-1))
                .Should().Throw<InvalidOperationException>();
        }
    }
}
