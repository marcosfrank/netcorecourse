﻿using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.Controllers.ToRemove;
using NetCoreCourse.FirstExample.WebApp.Dto;
using NetCoreCourse.FirstExample.WebApp.Handlers;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class GenericController : ControllerBase
    {
        [HttpGet("lists")]
        public IActionResult ListUsage() //Ver la clase transporte.
        {
            var list2 = new List<Transport>();
            var plane = new Plane();
            plane.Model = "Airbus A320";
            plane.TurbineAmount = 2;
            var plane2 = new Plane
            {
                Model = "Boeing 747",
                TurbineAmount = 4
            };
            var car1 = new Car
            {
                Model = "Chevrolet Corsa",
                HasSolarRoof = false,
            };

            list2.Add(plane);
            list2.Add(plane2);
            list2.Add(car1);



            return Ok(list2);
        }

        [HttpGet("swap")]
        public IActionResult Swap()
        {
            int a, b;
            char c, d;
            a = 10;
            b = 20;
            c = 'I';
            d = 'V';
            var s = "";

            Swap<int>(ref a, ref b);
            Swap<char>(ref c, ref d);

            return Ok(new { a, b, c, d });
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        [HttpGet("class")]
        public IActionResult GetSpecificationsFromClass()
        {
            var intSpec = new SpecificationHandler<int>().GetSpecification(10);
            var charSpec = new SpecificationHandler<char>().GetSpecification('d');
            //var stringSpec = new SpecificationHandler<string>().GetSpecification(15); //Porque no compila esta linea?

            var transportSpec1 = new SpecificationHandler<Transport>().GetSpecification(new Plane());
            var transportSpec2 = new SpecificationHandler<Transport>().GetSpecification(new Car());

            // Probar agregando la restriccion al generic.
            return Ok(new { intSpec, charSpec, transportSpec1, transportSpec2 });
        }
    }
}