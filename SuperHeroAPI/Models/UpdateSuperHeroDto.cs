using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Models
{
    public class UpdateSuperHeroDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(150)]
        public string Place { get; set; }

        [Required]
        public int BaseStrength { get; set; }
    }
}
