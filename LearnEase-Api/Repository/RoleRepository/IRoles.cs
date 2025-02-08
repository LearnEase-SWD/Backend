using LearnEase_Api.Entity;

namespace LearnEase_Api.Repository.RoleRepository
{
    public interface IRoles
    {
        Task<List<Role>> getAllRoles();
        Task<Role> createRole(Role role);
        Task<Role> deleteRole(Role role);
        Task<Role> findByName(string name);
    }
}
