namespace NetCoreCourse.FirstExample.WebApp.Dto
{
    public class FilterCollectionDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;//En casos reales el tamaño de la pagina puede ser 10,25,100.
        public string? OrderBy { get; set; }
    }
}
