using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
