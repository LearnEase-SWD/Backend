using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IRoleService
    {
        Task<RoleReponse> CreateRole(RoleRequest request);
        Task<RoleReponse> DeleteRole(RoleRequest request);
        Task<RoleReponse> GetByName(string name);
    }
}
