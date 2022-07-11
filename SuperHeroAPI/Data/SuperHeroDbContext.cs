using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Data
{
    public class SuperHeroDbContext : DbContext
    {
        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
        public DbSet<SuperPower> SuperPowers => Set<SuperPower>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }
    }
}