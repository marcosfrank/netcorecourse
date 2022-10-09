using Microsoft.AspNetCore.Mvc;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{

    public class ComplexClass
    {
        public ComplexClass(int prop)
        {
            Prop = prop;
        }
        public int Prop { get; set; }
    }
    public record class RecordClass(string Name, int Age, ComplexClass C);
    
    public record struct RecordStruct(string Name, int Age, ComplexClass C);
    
    public readonly record struct RecordStructRO(string Name, int Age, ComplexClass C);


    [Route("api/[controller]")]
    public class RecordsController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            var ccForA = new ComplexClass(1);
            //Constructor con parametros posicionales. (Positional Parameters)
            var a = new RecordClass("a", 10, ccForA);
            var b = new RecordStruct("b", 15, new ComplexClass(1));
            var c = new RecordStructRO("c", 5, new ComplexClass(1));

            //ToString()
            Console.WriteLine(a);
            Console.WriteLine(b.ToString()); //Es indistinto si agregamos o no el ToString();
            Console.WriteLine(c);

            //Uso de propiedades como si utilizaramos clases normales
            Console.WriteLine(a.Age); //10
            Console.WriteLine(b.Name); //b

            //Constructor sin parametros (Parameterless Constructor)
            //var a2 = new RecordClass(); //ERROR
            var b2 = new RecordStruct();
            var c2 = new RecordStructRO();
            Console.WriteLine(b2.Age); //0 -> Asigna los valores por defecto
            Console.WriteLine(c2.Name); //null -> Asigna los valores por defecto

            // Inmutabilidad (Immutability)
            //a.Age = 15; //Compile error
            b.Age = 15; //Because Record Structs are not immutable by default
            //c.Age = 15; //Compile error

            // Inmutabilidad "Superficial" (Shallow Immutability)
            a.C.Prop = 1; // 
            c.C.Prop = 1; // 

            // Igualdad por valor (Value-Equality)
            var a3 = new RecordClass("a", 10, ccForA);
            var b3 = new RecordStruct("b", 15, new ComplexClass(1));
            var c3 = new RecordStructRO("c", 5, new ComplexClass(1));
            var c3b = new RecordStructRO("c", 5, c.C);

            Console.WriteLine(a == a3); //true
            Console.WriteLine(c == c3); //false --> Porque la propiedad de tipo "ComplexClass" tiene una instancia distinta.
            Console.WriteLine(c == c3b); //true
            Console.WriteLine(new ComplexClass(2) == new ComplexClass(2)); // false

            //Como se utiliza With 
            var a4 = a3 with { Age = 200 };
            var b4 = b3 with { C = new ComplexClass(7) };
            var c4 = c3 with { Name = "new instance" };

            //Structs VS Record Structs
            var b5 = new RecordStruct("b", 15, new ComplexClass(1));
            var structb = new StructB ("b", 15, 1);

            Console.WriteLine(b5); //Tipo + Propiedades con sus valores.
            Console.WriteLine(structb); //Full-Qualified Type -> No tenemos traduccion al español o Nombre totalmente calificado??

            Console.WriteLine(b5 == b3); //
            //Console.WriteLine(structb == structb); //ERROR == no esta implementado
        }

        public struct StructB
        {
            public StructB(string name, int age, int propC)
            {
                Name = name;
                Age = age;
                C = new ComplexClass(propC);
            }
            public string Name { get; set; }
            public int Age { get; set; }
            public ComplexClass C { get; set; }
        }

        //Record class permite herencia, Record Structs No
        public record RecordChild(string Name, int Age, ComplexClass C, String extra) : RecordClass(Name, Age, C);
        //public record RecordChildStruct(string Name, int Age, ComplexClass C, String extra) : RecordC(Name, Age, C); //ERROR
    }

}
