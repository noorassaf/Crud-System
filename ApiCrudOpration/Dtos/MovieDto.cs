namespace ApiCrudOpration.Dtos
{
    public class MovieDto
    {
        [StringLength(100)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [StringLength(100)]
        public string StoreLine { get; set; }
        public IFormFile? Poster { get; set; }
        public byte GenreId { get; set; }
    }
}
