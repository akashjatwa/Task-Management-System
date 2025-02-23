using TaskManagement.API.Models;

namespace TaskManagement.API.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string email);
    }
}
