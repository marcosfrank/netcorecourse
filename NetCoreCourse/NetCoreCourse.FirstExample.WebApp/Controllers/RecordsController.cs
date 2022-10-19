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
    public record class RecordClass(string Name, int Age, ComplexClass Complex);
    
    public record struct RecordStruct(string Name, int Age, ComplexClass Complex);
    
    public readonly record struct RecordStructRO(string Name, int Age, ComplexClass Complex);


    [Route("api/[controller]")]
    public class RecordsController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            var ccForA = new ComplexClass(1);
            //Constructor con parametros posicionales. (Positional Parameters)
            var rc = new RecordClass("a", 10, ccForA);
            var rs = new RecordStruct("b", 15, new ComplexClass(1));
            var rsro = new RecordStructRO("c", 5, new ComplexClass(1));

            //ToString()
            Console.WriteLine(rc);
            Console.WriteLine(rs.ToString()); //Es indistinto si agregamos o no el ToString();
            Console.WriteLine(rsro);

            //Uso de propiedades como si utilizaramos clases normales
            Console.WriteLine(rc.Age); //10
            Console.WriteLine(rs.Name); //b

            //Constructor sin parametros (Parameterless Constructor)
            //var a2 = new RecordClass(); //ERROR
            var b2 = new RecordStruct();
            var c2 = new RecordStructRO();
            Console.WriteLine(b2.Age); //0 -> Asigna los valores por defecto
            Console.WriteLine(c2.Name); //null -> Asigna los valores por defecto

            // Inmutabilidad (Immutability)
            //rc.Age = 15; //Compile error
            rs.Age = 15; //Because Record Structs are not immutable by default
            //rsro.Age = 15; //Compile error

            // Inmutabilidad "Superficial" (Shallow Immutability) 
            rc.Complex.Prop = 2; //
            rsro.Complex.Prop = 1; // 

            // Igualdad por valor (Value-Equality)
            var a3 = new RecordClass("a", 10, ccForA);
            var b3 = new RecordStruct("b", 15, new ComplexClass(1));
            var c3 = new RecordStructRO("c", 5, new ComplexClass(1));
            var c3b = new RecordStructRO("c", 5, rsro.Complex);

            Console.WriteLine(rc == a3); //true
            Console.WriteLine(rsro == c3); //false --> Porque la propiedad de tipo "ComplexClass" tiene una instancia distinta.
            Console.WriteLine(rsro == c3b); //true
            Console.WriteLine(new ComplexClass(2) == new ComplexClass(2)); // false

            //Como se utiliza With 
            var a4 = a3 with { Age = 200 };
            var b4 = b3 with { Complex = new ComplexClass(7) };
            var c4 = c3 with { Name = "new instance" };
            var cc4 = c3 with { }; //copia exacta.

            //Structs VS Record Structs
            var b5 = new RecordStruct("b", 15, new ComplexClass(1));
            var structb = new StructB ("b", 15, 1);

            Console.WriteLine(b5); //Tipo + Propiedades con sus valores.
            Console.WriteLine(structb); //Full-Qualified Type -> No tenemos traduccion al español o "Nombre totalmente calificado"??

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
