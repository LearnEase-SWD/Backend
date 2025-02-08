using LearnEase_Api.dto.reponse;
using LearnEase_Api.dto.request;

namespace LearnEase_Api.Models.RolesService
{
    public interface IRoleService
    {
        Task<List<RoleReponse>> getAllRoles();
        Task<RoleReponse> createRole(RoleRequest request);
        Task<RoleReponse> deleteRole(RoleRequest request);
        Task<RoleReponse> getRole(string  name);
    }
}
