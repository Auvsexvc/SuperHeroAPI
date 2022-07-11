using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = _accountService.GenerateJWT(dto);

            return Ok(token);
        }
    }
}