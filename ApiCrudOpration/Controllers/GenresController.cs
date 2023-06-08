
using ApiCrudOpration.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudOpration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Genre = await _genreService.GetAllGenre();
            return Ok(Genre);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] GenreDto dto)
        {

            var Genre = new Genre
            {
                Name = dto.Name,
            };
            await _genreService.Create(Genre);
            return Ok(Genre);
        }

        [HttpPut("Update{Id}")]
        public async Task<IActionResult> Update(byte Id, [FromBody] GenreDto dto)
        {
            var Genre = await _genreService.GetById(Id);
            if (Genre == null) return NotFound($"No Genre Found with Id:{Id}");
            Genre.Name = dto.Name;
            _genreService.Update(Genre);
            return Ok(Genre);
        }
        [HttpDelete("Delete{Id}")]
        public async Task<IActionResult> Delete(byte Id)
        {
            var Genre = await _genreService.GetById(Id);
            if (Genre == null) return NotFound($"No Genre Found with Id:{Id}");
             _genreService.Delete(Genre);
            return Ok(Genre);
        }
    }
}
