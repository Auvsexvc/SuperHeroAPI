using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Interfaces;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpPost]
        public ActionResult<List<SuperHero>> AddHero(SuperHero hero)
        {
            var id = _superHeroService.Create(hero);
            return Created($"/api/SuperHero/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<SuperHero>> DeleteHero(int? id)
        {
            if (_superHeroService.GetById(id) == null)
            {
                return NotFound();
            }
            _superHeroService.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<SuperHero> Get(int? id)
        {
            object hero = _superHeroService.GetById(id);
            if(hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }

        [HttpGet]
        public ActionResult<List<SuperHero>> GetAll()
        {
            object superHeroes = _superHeroService.GetAll();

            return Ok(superHeroes);
        }

        [HttpPut]
        public ActionResult<List<SuperHero>> UpdateHero(SuperHero request)
        {
            if(_superHeroService.GetById(request.Id) == null)
            {
                return NotFound();
            }
            _superHeroService.Update(request);

            return Ok();
        }
    }
}