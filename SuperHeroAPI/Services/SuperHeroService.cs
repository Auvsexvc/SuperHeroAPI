using AutoMapper;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Exceptions;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly SuperHeroDbContext _dbContext;
        private readonly ILogger<SuperHeroService> _logger;
        private readonly IMapper _mapper;

        public SuperHeroService(SuperHeroDbContext context, IMapper mapper, ILogger<SuperHeroService> logger)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(CreateSuperHeroDto dto)
        {
            var hero = _mapper.Map<SuperHero>(dto);
            _dbContext.Add(hero);
            _dbContext.SaveChanges();

            _logger.LogInformation($"SuperHero {hero.Name} with ID {hero.Id} created.");

            return hero.Id;
        }

        public void Delete(int id)
        {
            var hero = _dbContext.SuperHeroes.Find(id);

            if (hero is null)
            {
                throw new NotFoundException("SuperHero not found.");
            }

            _dbContext.SuperHeroes.Remove(hero);
            _dbContext.SaveChanges();

            _logger.LogInformation($"SuperHero {hero.Name} with ID {hero.Id} deleted.");
        }

        public void DeleteAll()
        {
            _dbContext.SuperHeroes.RemoveRange(_dbContext.SuperHeroes);
            _dbContext.SaveChanges();

            _logger.LogInformation("All SuperHeroes have been deleted.");
        }

        public IEnumerable<SuperHeroDto> GetAll()
        {
            var heroes = _dbContext
                .SuperHeroes
                .Include(h => h.SuperPowers)
                .ToList();

            return _mapper.Map<List<SuperHeroDto>>(heroes);
        }

        public SuperHeroDto GetById(int id)
        {
            var hero = _dbContext
                .SuperHeroes
                .Include(h => h.SuperPowers)
                .FirstOrDefault(h => h.Id == id);

            if (hero is null)
            {
                throw new NotFoundException("SuperHero not found.");
            }

            return _mapper.Map<SuperHeroDto>(hero);
        }

        public SuperHeroDto Update(int id, UpdateSuperHeroDto dto)
        {
            var hero = _dbContext.SuperHeroes.Find(id);

            if (hero is null)
            {
                throw new NotFoundException("SuperHero not found.");
            }

            hero.Name = dto.Name;
            hero.FirstName = dto.FirstName;
            hero.LastName = dto.LastName;
            hero.Email = dto.Email;
            hero.Place = dto.Place;
            hero.BaseStrength = dto.BaseStrength;

            _dbContext.SaveChanges();

            _logger.LogInformation($"SuperHero {hero.Name} with ID {hero.Id} updated.");

            return _mapper.Map<SuperHeroDto>(hero);
        }
    }
}