using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Models
{
    public class CreateSuperPowerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AdditionToSuperHeroStrength { get; set; }
    }
}