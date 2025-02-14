using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IRoleService
    {
        Task<List<RoleReponse>> getAllRoles();
        Task<RoleReponse> createRole(RoleRequest request);
        Task<RoleReponse> deleteRole(RoleRequest request);
        Task<RoleReponse> getRole(string name);
    }
}
