namespace NetCoreCourse.FirstExample.WebApp.Entities
{
    public class Thing : EntityBase
    {
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; } //Convencion que normalmente se utiliza para dar de alta un Thing asociado a la categoria que tiene este ID.
    }
}
