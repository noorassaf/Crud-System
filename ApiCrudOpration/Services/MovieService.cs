using ApiCrudOpration.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrudOpration.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> create(Movie Movie)
        {
            await _context.Movies.AddAsync(Movie);
            _context.SaveChanges();
            return Movie;
        }

        public Movie Deleete(Movie Movie)
        {
            _context.Remove(Movie);
            _context.SaveChanges();
            return Movie;
        }

        public async Task<IEnumerable<Movie>> GetAllMovie(byte genreId = 0)
        {
           return await _context.Movies
                .Where(m=>m.GenreId == genreId||genreId==0)
                .OrderByDescending(x => x.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
            
        }

        public async Task<Movie> GetById(int Id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == Id);
        }

        public Movie Update(Movie Movie)
        {
            _context.Update(Movie);
            _context.SaveChanges();
            return Movie;
        }
    }
}
