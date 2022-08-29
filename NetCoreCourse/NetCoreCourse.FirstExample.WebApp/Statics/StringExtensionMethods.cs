namespace NetCoreCourse.FirstExample.WebApp.Statics
{
    public static class StringExtensionMethods
    {
        public static string ToCUILFormat(this string cuit)
        {
            int l = cuit.Length;
            if (l < 4) return string.Empty; //or null maybe.

            cuit = cuit.Insert(l-1, "/");
            cuit = cuit.Insert(2, "-");
            
            return cuit;
        }
    }
}
