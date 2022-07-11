using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var userDtos = _accountService.GetAll();

            return Ok(userDtos);
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] CreateUserDto dto)
        {
            _accountService.CreateUser(dto);

            return Ok();
        }
    }
}
