namespace BigECommerce.Auth.Jwt
{
    public class JwtSettings
    {
        public static string Key = "N5v9w2X8Q1r4T7u0Y3z6P8s2K5m8J1a3";
        public int ExpireMinutes { get; set; } = 60;
        public string Issuer { get; set; } = "BigECommerce";
        public string Audience { get; set; } = "BigECommerceUsers";
    }
}
