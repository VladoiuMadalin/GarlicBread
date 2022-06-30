using GameStore.DataLayer.Entities;

namespace GameStore.Services
{
    public interface ICustomerAuthService
    {
        string GetToken(User use);
        bool ValidateToken(string tokenString);
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}