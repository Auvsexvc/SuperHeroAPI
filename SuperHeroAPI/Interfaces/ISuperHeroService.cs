using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Interfaces
{
    public interface ISuperHeroService
    {
        SuperHero GetById(int? id);
        IEnumerable<SuperHero> GetAll();
        int Create(SuperHero entity);
        void Delete(int? id);
        void Update(SuperHero entity);
    }
}