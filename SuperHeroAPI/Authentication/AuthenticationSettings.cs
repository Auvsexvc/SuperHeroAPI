namespace SuperHeroAPI.Authentication
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = string.Empty;
        public int JwtExpire { get; set; }
        public string JwtIssuer { get; set; } = string.Empty;
    }
}
