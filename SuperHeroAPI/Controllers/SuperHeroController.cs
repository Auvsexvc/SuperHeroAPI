using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<SuperHeroDto>> GetAll()
        {
            IEnumerable<SuperHeroDto> superHeroesDtos = _superHeroService.GetAll();

            return Ok(superHeroesDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<SuperHeroDto> Get([FromRoute] int id)
        {
            object heroDto = _superHeroService.GetById(id);

            return Ok(heroDto);
        }

        [HttpPost]
        public ActionResult<IEnumerable<SuperHeroDto>> AddHero([FromBody] CreateSuperHeroDto dto)
        {
            var id = _superHeroService.Create(dto);

            return Created($"/api/SuperHero/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<SuperHeroDto>> DeleteHero([FromRoute] int id)
        {
            _superHeroService.Delete(id);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult<IEnumerable<SuperHeroDto>> DeleteAll()
        {
            _superHeroService.DeleteAll();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<SuperHeroDto> UpdateHero([FromRoute] int id, [FromBody] UpdateSuperHeroDto dto)
        {
            return Ok(_superHeroService.Update(id, dto));
        }
    }
}