namespace NetCoreCourse.FirstExample.WebApp.Entities
{
    public class Person
    {
        // Check that the following property is not ID, but PersonId. Another EF convention.
        public int PersonId { get; set; }
        public Address Address { get; set; }
    }
}
