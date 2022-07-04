using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Data
{
    public class SuperHeroDbContext : DbContext
    {
        public DbSet<SuperHero> SuperHeroes { get; set; }

        public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext> options) : base(options)
        {
        }
    }
}