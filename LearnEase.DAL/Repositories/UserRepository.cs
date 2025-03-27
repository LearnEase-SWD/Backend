using LearnEase.Repository;
using LearnEase.Repository.Repositories;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnEase_Api.LearnEase.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
	{
		private readonly IRedisCacheService _redisCacheService;

		public UserRepository(ApplicationDbContext context, 
						      IRedisCacheService redisCacheService) : base(context)
		{
			_redisCacheService = redisCacheService;
		}

		public async Task<User> FindByEmail(string email)
		{
			/*_redisCacheService.SetAsync();*/
			User user = await _dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email));
			if (user != null)
			{
				await _redisCacheService.SetAsync("email", user.Email, TimeSpan.FromMinutes(2));
			}

			return user;
		}

		public async Task<User> FindByName(string name)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.UserName == name);
		}
	}
}
