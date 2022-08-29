namespace NetCoreCourse.FirstExample.WebApp.Statics
{
    public static class IntegerExtensionMethods
    {
        public static bool IsEven(this int number)
        { 
            return number % 2 == 0;
        }

        public static bool IsOdd(this int number) => !IsEven(number);
        //What an odd way of defining a method, right? Where are the {} ^^^^
    }
}
