namespace NetCoreCourse.FirstExample.WebApp.Models
{
    public class RandomServiceValues
    {
        public int Transient { get; set; }
        public int Scoped { get; set; }
        public int Singleton { get; set; }

        public RandomServiceValues AddToSingleton(int valueToAdd) //Metodo de instancia
        {
            Singleton += valueToAdd;
            return this;
        }
    }

    public static class RandomServicesValuesExtensions 
    {
        public static RandomServiceValues AddToScoped(this RandomServiceValues values, int valueToAdd) //Metodos de Extension
        {
            values.Scoped += valueToAdd;
            return values;
        }

        public static RandomServiceValues AddToTransient(this RandomServiceValues values, int valueToAdd)
        {
            values.Transient += valueToAdd;
            return values;
        }
    }
}
