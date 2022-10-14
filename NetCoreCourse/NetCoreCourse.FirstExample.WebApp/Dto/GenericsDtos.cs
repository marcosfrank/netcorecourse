namespace NetCoreCourse.FirstExample.WebApp.Dto
{
    // En un mismo archivo, se pueden tener varias clases. No es muy buena practica, pero es algo que se puede hacer.
    // En este caso estan en el mismo archivo para poder mostrar la herencia entre las clases.
    public class Transport
    {
        public virtual string Name { get; init; }
        public virtual int Wheels { get; }
        public string Model { get; set; }

        public override string ToString()
        {
            return $"The name is: '{Name}'";
        }
    }

    public class Plane : Transport
    {
        public override string Name => nameof(Plane); //Que es este nameof()?

        public override int Wheels => 12;//Este numero varia segun el avion, pero por simplicidad lo dejamos en 12.
        public int TurbineAmount { get; set; }
    }

    internal class Car : Transport
    {
        public override string Name => "Car";

        public override int Wheels => 4;

        public bool HasSolarRoof { get; set; }
    }
}
