﻿using LearnEase.Core.Entities;
using LearnEase.Repository.Repository;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;

namespace LearnEase_Api.LearnEase.Infrastructure.Repository
{
    public class UserDetailRepository : GenericRepository<UserDetail>, IUserDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public UserDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<UserDetail> CreateUserDetail(UserDetail userDetail)
        {
            if (userDetail == null) throw new ArgumentNullException(nameof(userDetail));

            await _context.UserDetails.AddAsync(userDetail);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return null;
            }

            return userDetail;
        }

        public async Task<UserDetail> GetUserDetailByUserId(string userId)
        {
            return _context.UserDetails.FirstOrDefault(x => x.User.UserId.Equals(userId));
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
