namespace ApiCrudOpration.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMovie(byte genreId= 0);
        Task<Movie> GetById(int Id);
        Task<Movie> create(Movie Movie);
        Movie Update(Movie Movie);
        Movie Deleete(Movie Movie);
    }
}
