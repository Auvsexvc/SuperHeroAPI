using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Models
{
    public class SuperHeroDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Strength { get; set; }
        public string Place { get; set; }
        public List<SuperPowerDto> SuperPowers { get; set; }
    }
}