using SuperHeroAPI.Models;

namespace SuperHeroAPI.Interfaces
{
    public interface ISuperHeroService
    {
        SuperHeroDto GetById(int id);

        IEnumerable<SuperHeroDto> GetAll();

        int Create(CreateSuperHeroDto dto);

        void Delete(int id);

        void DeleteAll();

        SuperHeroDto Update(int id, UpdateSuperHeroDto dto);
    }
}