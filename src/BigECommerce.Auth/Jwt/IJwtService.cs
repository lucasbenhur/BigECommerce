namespace BigECommerce.Auth.Jwt
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(string email, string password);
    }
}
