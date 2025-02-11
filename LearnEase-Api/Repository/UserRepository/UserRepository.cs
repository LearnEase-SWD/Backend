using LearnEase_Api.dto.request;
using LearnEase_Api.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnEase_Api.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> createNewUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _logger.LogInformation($"Creating new user: {user.ToString()}");

            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to save user to database.");
                return null;
            }

            return user;
        }

        public async Task<User> deleteUser(string id)
        {
            _logger.LogInformation($"Deleting user with ID: {id}");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId.Equals(id));
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return null;
            }

            user.IsActive = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User information updated: {user.ToString()}");
            return user;
        }

        public async Task<User> FindByEmail(string email)
        {
            _logger.LogInformation($"Finding user by email: {email}");
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals( email));
        }

        public async Task<User> FindById(string id)
        {
            _logger.LogInformation($"Finding user by ID: {id}");
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId.Equals(id));
        }

        public async Task<User> FindByName(string name)
        {
            _logger.LogInformation($"Finding user by name: {name}");
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<List<User>> GetAll()
        {
            _logger.LogInformation("Fetching all users.");
            return await _context.Users.ToListAsync();
        }

        public async Task<User> updateUser(User user, string id)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _logger.LogInformation($"Updating user: {user.ToString()}");
            

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
