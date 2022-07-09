using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Models
{
    public class SuperPowerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AdditionToSuperHeroStrength { get; set; }
        public string UsedBySuperHeroes { get; set; } = string.Empty;
    }
}
