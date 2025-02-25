using LearnEase.Repository.Repository;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnEase_Api.LearnEase.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> FindByEmail(string email)
        {
            _logger.LogInformation($"Finding user by email: {email}");
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<User> FindByName(string name)
        {
            _logger.LogInformation($"Finding user by name: {name}");
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == name);
        }


    }
}
