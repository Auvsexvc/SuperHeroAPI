using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly SuperHeroDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(SuperHeroDbContext dbContext, IPasswordHasher<User> passwordHasher, IMapper mapper, ILogger<AccountService> logger)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logger = logger;
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
            //throw new NotImplementedException();
            return string.Empty;
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