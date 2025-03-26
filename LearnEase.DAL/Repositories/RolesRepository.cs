/*using LearnEase.Core.Entities;
using LearnEase.Repository.Repositories;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearnEase_Api.LearnEase.Infrastructure.Repositories
{
    public class RolesRepository : GenericRepository<Role>, IRoleRepository
    {
        public RolesRepository(ApplicationDbContext context) : base(context)
        {
        }

        *//*public async Task<Role> createRole(Role role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            var countRole = await _context.Roles.CountAsync();
            if (countRole == 0)
            {
                role.RoleId = "1";
            }
            else
            {
                role.RoleId = (countRole + 1).ToString();
            }

            await _context.Roles.AddAsync(role);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return null;
            }

            return role;
        }*/

        /*public async Task<Role> deleteRole(Role role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            _context.Roles.Remove(role);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return null;
            }

            return role;
        }*//*

        public async Task<Role> FindByName(string name)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.RoleName == name);
            return result;
        }

    }
}
*/