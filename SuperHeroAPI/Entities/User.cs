namespace SuperHeroAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public virtual Role? Role { get; set; }
        public int RoleId { get; set; }
    }
}
