using LearnEase.Repository.Repository;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearnEase_Api.LearnEase.Infrastructure.Repository
{
    public class RolesRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RolesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Role> createRole(Role role)
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
        }

        public async Task<Role> deleteRole(Role role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            _context.Roles.Remove(role);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return null;
            }

            return role;
        }

        public async Task<Role> FindByName(string name)
        {
            var result = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == name);
            if (result == null) throw new ArgumentNullException(nameof(name));
            return result;
        }

        public async Task<List<Role>> getAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
