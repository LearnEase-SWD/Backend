using LearnEase_Api.Entity;

namespace LearnEase_Api.Repository.UserDetailRepository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public UserDetailRepository(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<UserDetail> CreateUserDetail(UserDetail userDetail)
        {
            if(userDetail == null) throw new ArgumentNullException(nameof(userDetail));
            
            await _context.UserDetails.AddAsync(userDetail);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return null ;
            }

            return userDetail;
        }

        public async Task<UserDetail> getUserDetailById(string id)
        {
            return _context.UserDetails.FirstOrDefault(x=> x.Id.Equals(id));
        }

        public async Task<UserDetail> getUserDetailByUserId(string userId)
        {
            return  _context.UserDetails.FirstOrDefault(x => x.User.UserId.Equals(userId));
        }

        public async Task<UserDetail> UpdateUserDetail(UserDetail userDetail)
        {
            if (userDetail == null) throw new ArgumentNullException(nameof(userDetail));
            _context.UserDetails.Update(userDetail);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return null;
            }

            return userDetail;
        }
    }
}
