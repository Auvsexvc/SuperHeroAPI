using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Entities
{
    public class SuperHero
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Place { get; set; } = string.Empty;

        [Required]
        public int BaseStrength { get; set; }

        public virtual List<SuperPower>? SuperPowers { get; set; }
    }
}