using TaskManagement.API.Models;

namespace TaskManagement.API.Interfaces
{
    public interface IUserService 
    {
        Task<List<Users>> GetAllUsersAsync();
        Task<Users> GetUserByIdAsync(int id);

        Task<Users> CreateUserAsync(Users users);

        Task<Users> UpdateUserAsync(int id, Users users);

        Task<bool> DeleteUserAsync(int id);

    }
}
