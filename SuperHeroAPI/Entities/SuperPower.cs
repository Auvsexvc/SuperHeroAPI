using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Entities
{
    public class SuperPower
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int AdditionToSuperHeroStrength { get; set; }

        public virtual List<SuperHero>? SuperHeroes { get; set; }
    }
}