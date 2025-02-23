using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Data.Entities;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Models;

namespace TaskManagement.API.Services
{
    public class UserService : IUserService
    {
        private readonly TaskDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new Users
                {
                    UserId = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Password = u.PasswordHash

                })
                .ToListAsync();
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            return new Users
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
            };
        }

        public async Task<Users> CreateUserAsync(Users users)
        {
            var userEntity = new UserEntity()
            {
                Id = users.UserId,
                FullName = users.FullName,
                Email = users.Email,
                PasswordHash = users.Password
            };

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return new Users
            {
                UserId = users.UserId,
                FullName = users.FullName,
                Email = users.Email,
            };
        }

        public async Task<Users> UpdateUserAsync(int id, Users users)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == users.UserId);
            if (existingUser == null) return null;

            existingUser.FullName = users.FullName;
            existingUser.Email = users.Email;
            existingUser.PasswordHash = users.Password;
        
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return new Users
            {
                UserId = users.UserId,
                FullName = users.FullName,
                Email = users.Email,
                Password = users.Password
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
