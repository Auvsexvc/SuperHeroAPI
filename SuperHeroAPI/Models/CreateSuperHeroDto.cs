using SuperHeroAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Models
{
    public class CreateSuperHeroDto
    {
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public int BaseStrength { get; set; }
    }
}
