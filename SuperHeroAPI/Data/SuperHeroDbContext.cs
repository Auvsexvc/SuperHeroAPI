using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Data
{
    public class SuperHeroDbContext : DbContext
    {
        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
        public DbSet<SuperPower> SuperPowers => Set<SuperPower>();

        public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext> options) : base(options)
        {
        }
    }
}