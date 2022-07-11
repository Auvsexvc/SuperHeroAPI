using SuperHeroAPI.Models;

namespace SuperHeroAPI.Interfaces
{
    public interface IAccountService
    {
        void CreateUser(CreateUserDto dto);
        IEnumerable<UserDto> GetAll();
        string GenerateJWT(LoginUserDto dto);
    }
}
