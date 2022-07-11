using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI.Authentication;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Exceptions;
using SuperHeroAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeroAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly SuperHeroDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(SuperHeroDbContext dbContext, IPasswordHasher<User> passwordHasher, IMapper mapper, ILogger<AccountService> logger, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logger = logger;
            _authenticationSettings = authenticationSettings;
        }

        public void CreateUser(CreateUserDto dto)
        {
            User newUser = new()
            {
                Email = dto.Email,
                Name = dto.Name,
                RoleId = dto.RoleId > 0 ? dto.RoleId : 2,
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            _logger.LogInformation($"User with ID: {newUser.Id} created");
        }

        public string GenerateJWT(LoginUserDto dto)
        {
            var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Name == dto.Name);

            if (user == null)
            {
                throw new BadRequestException("Invalid user or password");
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid user or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expireDate = DateTime.Now.AddDays(_authenticationSettings.JwtExpire);

            var token = new JwtSecurityToken(
                issuer: _authenticationSettings.JwtIssuer,
                audience: _authenticationSettings.JwtIssuer,
                claims: claims,
                expires: expireDate,
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<UserDto> GetAll()
        {
            List<User> users = _dbContext
                .Users
                .Include(r => r.Role)
                .ToList();

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}