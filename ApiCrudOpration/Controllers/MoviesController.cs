using ApiCrudOpration.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudOpration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        
        private new List<string> _allwExtention = new List<string> { "jpg", "png" };
        private long _maxAllowPosterSize = 1048576;
        public MoviesController(IMovieService movieService, IGenreService genreService, IMapper mapper)
        {
            _movieService = movieService;
            _genreService = genreService;
            _mapper = mapper;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("poster is rquierd");
            if (!_allwExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only jpg or png are allowed");
            if (dto.Poster.Length > _maxAllowPosterSize)
                return BadRequest("max allowed size for poste is 1MB");
            var isValidGenre = await _genreService.isvalidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("invalid Genre id");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();
            await _movieService.create(movie);
            
            return Ok(movie);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var movie = await _movieService.GetAllMovie();
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movie);
            return Ok(data);
        }
        [HttpGet("GetById{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
           // var movie = await _context.Movies.FindAsync(Id);// i cant use .include()
            var movie = await _movieService.GetById(Id);
            if (movie == null)
                return NotFound();
            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
        }
        [HttpGet("GetByGenreId{Id}")]
        public async Task<IActionResult> GetByGenreId(byte Id)
        {
            var movie = await _movieService.GetAllMovie(Id);
            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
            
        }

        [HttpPut("Update{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] MovieDto dto)
        {
            var movie = await _movieService.GetById(Id);
            if (movie == null)
                return NotFound($"No movie Found with Id:{Id}");
            var isValidGenre = await _genreService.isvalidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("invalid Genre id");
            if (dto.Poster != null)
            {

                if (!_allwExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("only jpg or png are allowed");
                if (dto.Poster.Length > _maxAllowPosterSize)
                    return BadRequest("max allowed size for poste is 1MB");
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }
            movie.Title= dto.Title;
            movie.Rate= dto.Rate;
            movie.StoreLine= dto.StoreLine;
            movie.Year= dto.Year;
            movie.GenreId= dto.GenreId;
            _movieService.Update(movie);
            return Ok(movie);
        }
        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var movie = await _movieService.GetById(Id) ;
            if (movie == null) 
                return NotFound($"no movie was found with id:{Id}");
            
            _movieService.Deleete(movie);
            return Ok(movie);
        }
    }
}
