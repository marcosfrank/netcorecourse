namespace NetCoreCourse.FirstExample.WebApp.Entities
{
    public class Person
    {
        // Verificar que esta propiedad tambien es Id de la entidad. 
        // En este caso utiliza la convencion {type}Id
        //Normalmente vamos a seguir una unica convencion para que el codigo sea mas facil de entender.
        public int PersonId { get; set; }
        // En este caso, una persona no podria existir si NO existe una direccion.
        // Muchas decisiones de diseño de software van a depender del negocio.
        public Address Address { get; set; } 
    }
}
