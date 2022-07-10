using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Models
{
    public class SuperHeroDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Strength { get; set; }
        public string Place { get; set; } = string.Empty;
        public List<SuperPowerDto> SuperPowers { get; set; } = new List<SuperPowerDto>();
    }
}