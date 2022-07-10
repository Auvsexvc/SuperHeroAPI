using AutoMapper;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Exceptions;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services
{
    public class SuperPowerService : ISuperPowerService
    {
        private readonly SuperHeroDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SuperHeroService> _logger;

        public SuperPowerService(SuperHeroDbContext context, IMapper mapper, ILogger<SuperHeroService> logger)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        public SuperPowerDto AddHeroAssignment(int id, int heroId)
        {
            var power = _dbContext.SuperPowers.Include(p => p.SuperHeroes).FirstOrDefault(p => p.Id == id);
            var hero = _dbContext.SuperHeroes.Include(p => p.SuperPowers).FirstOrDefault(p => p.Id == heroId);

            var heroesUsingPower = _dbContext.SuperPowers.Include(p => p.SuperHeroes).FirstOrDefault(p => p.Id == id);

            if (power is null)
            {
                throw new NotFoundException("SuperPower not found.");
            }
            else if (hero is null)
            {
                throw new NotFoundException("SuperHero not found.");
            }

            if (heroesUsingPower?.SuperHeroes.Any(h => h.Id == heroId) != false)
            {
                throw new BadRequestException("Pointed SuperHero is already using this Super power.");
            }

            power.SuperHeroes.Add(hero);
            hero.SuperPowers.Add(power);
            _dbContext.SaveChanges();

            return _mapper.Map<SuperPowerDto>(power);
        }

        public SuperPowerDto RemoveHeroAssignment(int id, int heroId)
        {
            var power = _dbContext.SuperPowers.Include(p => p.SuperHeroes).FirstOrDefault(p => p.Id == id);
            var hero = _dbContext.SuperHeroes.Include(p => p.SuperPowers).FirstOrDefault(p => p.Id == heroId);

            var heroesUsingPower = _dbContext.SuperPowers.Include(p => p.SuperHeroes).FirstOrDefault(p => p.Id == id);

            if (power is null)
            {
                throw new NotFoundException("SuperPower not found.");
            }
            else if(hero is null)
            {
                throw new NotFoundException("SuperHero not found.");
            }

            if (heroesUsingPower?.SuperHeroes.Any(h => h.Id == heroId) != true)
            {
                throw new BadRequestException("Pointed SuperHero is not using this Super power.");
            }

            power.SuperHeroes.Remove(hero);
            hero.SuperPowers.Remove(power);
            _dbContext.SaveChanges();

            return _mapper.Map<SuperPowerDto>(power);
        }

        public int Create(CreateSuperPowerDto dto)
        {
            var power = _mapper.Map<SuperPower>(dto);

            _dbContext
                .Add(power);

            _dbContext.SaveChanges();

            _logger.LogInformation("SuperPower created.");

            return power.Id;
        }

        public void Delete(int id)
        {
            var power = _dbContext.SuperPowers.Find(id);

            if (power is null)
            {
                throw new NotFoundException("SuperPower not found.");
            }

            _dbContext
                .SuperPowers
                .Remove(power);

            _dbContext.SaveChanges();

            _logger.LogInformation("SuperPower deleted.");
        }

        public void DeleteAll()
        {
            _dbContext
                .SuperPowers
                .RemoveRange(_dbContext.SuperPowers);

            _dbContext.SaveChanges();

            _logger.LogInformation("All SuperPowers have been deleted.");
        }

        public IEnumerable<SuperPowerDto> GetAll()
        {
            var powers = _dbContext
                .SuperPowers
                .Include(h => h.SuperHeroes)
                .ToList();

            return _mapper.Map<List<SuperPowerDto>>(powers);
        }

        public SuperPowerDto GetById(int id)
        {
            var power = _dbContext
                .SuperPowers
                .Include(h => h.SuperHeroes)
                .FirstOrDefault(h => h.Id == id);

            if (power is null)
            {
                throw new NotFoundException("SuperPower not found.");
            }

            return _mapper.Map<SuperPowerDto>(power);
        }

        public SuperPowerDto Update(int id, UpdateSuperPowerDto dto)
        {
            var power = _dbContext.SuperPowers.Find(id);

            if (power is null)
            {
                throw new NotFoundException("SuperPower not found.");
            }

            power.Name = dto.Name;
            power.Description = dto.Description;
            power.AdditionToSuperHeroStrength = dto.AdditionToSuperHeroStrength;

            _dbContext.SaveChanges();

            _logger.LogInformation("SuperPower updated.");

            return _mapper.Map<SuperPowerDto>(power);
        }
    }
}