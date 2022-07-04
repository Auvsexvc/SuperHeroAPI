using AutoMapper;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly SuperHeroDbContext _context;
        private readonly IMapper _mapper;

        public SuperHeroService(SuperHeroDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(SuperHero entity)
        {
            var hero = _mapper.Map<SuperHero>(entity);
            _context.Add(hero);
            _context.SaveChanges();

            return hero.Id;
        }

        public void Delete(int? id)
        {
            var hero = _context.SuperHeroes.Find(id);
            if (hero != null)
            {
                _context.SuperHeroes.Remove(hero);
                _context.SaveChanges();
            }
        }

        public IEnumerable<SuperHero> GetAll()
        {
            var heroes = _context.SuperHeroes.ToList();
            var heroesDto = _mapper.Map<List<SuperHero>>(heroes);
            return heroesDto;
        }

        public SuperHero GetById(int? id)
        {
            var hero = _context.SuperHeroes.Find(id);
            var heroDto = _mapper.Map<SuperHero>(hero);
            return heroDto;
        }

        public void Update(SuperHero entity)
        {
            var hero = _context.SuperHeroes.Find(entity.Id);
            if (hero != null)
            {
                hero.Id = entity.Id;
                hero.Name = entity.Name;
                hero.FirstName = entity.FirstName;
                hero.LastName = entity.LastName;
                hero.Place = entity.Place;
                _context.SaveChanges();
            }
        }
    }
}