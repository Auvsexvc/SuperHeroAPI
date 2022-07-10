using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Models;

namespace SuperPowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperPowerController : ControllerBase
    {
        private readonly ISuperPowerService _superPowerService;

        public SuperPowerController(ISuperPowerService superPowerService)
        {
            _superPowerService = superPowerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SuperPowerDto>> GetAll()
        {
            IEnumerable<SuperPowerDto> superPoweresDtos = _superPowerService.GetAll();

            return Ok(superPoweresDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<SuperPowerDto> Get([FromRoute] int id)
        {
            object PowerDto = _superPowerService.GetById(id);

            return Ok(PowerDto);
        }

        [HttpPost]
        public ActionResult<List<SuperPower>> AddPower([FromBody] CreateSuperPowerDto dto)
        {
            var id = _superPowerService.Create(dto);

            return Created($"/api/SuperPower/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<SuperPower>> DeletePower([FromRoute] int id)
        {
            _superPowerService.Delete(id);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult<List<SuperPower>> DeleteAll()
        {
            _superPowerService.DeleteAll();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<List<SuperPowerDto>> UpdatePower([FromRoute] int id, [FromBody] UpdateSuperPowerDto dto)
        {
            _superPowerService.Update(id, dto);

            return Ok();
        }

        [Route("{id}/[action]/{heroId}")]
        [HttpPost]
        public ActionResult<SuperPowerDto> AssignToHero([FromRoute] int id, int heroId)
        {
            return Ok(_superPowerService.AddHeroAssignment(id, heroId));
        }

        [Route("{id}/[action]/{heroId}")]
        [HttpDelete]
        public ActionResult<SuperPowerDto> RemoveFromHero([FromRoute] int id, int heroId)
        {
            return Ok(_superPowerService.RemoveHeroAssignment(id, heroId));
        }
    }
}