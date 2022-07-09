using SuperHeroAPI.Models;

namespace SuperHeroAPI.Interfaces
{
    public interface ISuperPowerService
    {
        SuperPowerDto GetById(int id);
        IEnumerable<SuperPowerDto> GetAll();
        int Create(CreateSuperPowerDto dto);
        void Delete(int id);
        void DeleteAll();
        SuperPowerDto Update(int id, CreateSuperPowerDto dto);
        SuperPowerDto AddHeroAssignment(int id, int heroId);
        SuperPowerDto RemoveHeroAssignment(int id, int heroId);
    }
}
