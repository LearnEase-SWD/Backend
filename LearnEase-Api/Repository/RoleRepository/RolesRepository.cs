using LearnEase_Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase_Api.Repository.RoleRepository
{
    public class RolesRepository : IRoles
    {
        private readonly ApplicationDbContext _context;
        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Role> createRole(Role role)
        {
           if(role == null) throw new ArgumentNullException(nameof(role));
           var countRole = await _context.Roles.CountAsync();
            if (countRole == 0)
            {
                role.Id = "1";
            }
            else
            {
                role.Id=(countRole+1).ToString();
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

        public async Task<Role> findByName(string name)
        {
            var result = await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
            if (result == null) throw new ArgumentNullException(nameof(name));
            return result;
        }

        public async Task<List<Role>> getAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
