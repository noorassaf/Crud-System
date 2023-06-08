namespace ApiCrudOpration.Dtos
{
    public class GenreDto
    {
        [StringLength(100)]
        public string Name { get; set; }
    }
}
