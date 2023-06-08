namespace ApiCrudOpration.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;
        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Genre> Create(Genre Genre)
        {
            await _context.Genres.AddAsync(Genre);
            _context.SaveChanges();
            return Genre;
        }

        public Genre Delete(Genre Genre)
        {
            _context.Remove(Genre);
            _context.SaveChanges();
            return Genre;
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await _context.Genres.OrderBy(m => m.Name).ToListAsync(); 
        }

        public async Task<Genre> GetById(byte Id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == Id);
        }

        public async Task<bool> isvalidGenre(byte Id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == Id);
        }

        public Genre Update(Genre Genre)
        {
            _context.Update(Genre);
            _context.SaveChanges();
            return Genre;
        }
    }
}
