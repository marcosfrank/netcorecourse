namespace NetCoreCourse.FirstExample.WebApp.Dto
{
    public class CategoriesFilterDto : FilterCollectionDto
    {
        public string? DescriptionContains { get; set; }

        public string GetDescriptionContainsUpper()
        {
            return DescriptionContains.ToUpperInvariant();
        }
    }

    public static class FilterCollectionDtoExt
    {
        public static string DoExtension(this FilterCollectionDto dto)
        {
            return dto.ToString();
        }
    }
}
