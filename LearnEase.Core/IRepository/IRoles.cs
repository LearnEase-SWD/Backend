using LearnEase_Api.Entity;

namespace LearnEase_Api.LearnEase.Infrastructure.IRepository
{
    public interface IRoles
    {
        Task<List<Role>> getAllRoles();
        Task<Role> createRole(Role role);
        Task<Role> deleteRole(Role role);
        Task<Role> findByName(string name);
    }
}
