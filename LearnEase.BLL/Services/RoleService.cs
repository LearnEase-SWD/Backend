using LearnEase.Repository.UOW;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleReponse> CreateRole(RoleRequest request)
        {
            Role role = new Role();
            role.RoleName = request.name;

            await _unitOfWork.GetRepository<IRoleRepository>().CreateAsync(role);
            return new RoleReponse(null, role.RoleName);
        }

        public async Task<RoleReponse> DeleteRole(RoleRequest request)
        {
            var findRoleByName = await _unitOfWork.GetRepository<IRoleRepository>().FindByName(request.name);

            await _unitOfWork.GetRepository<IRoleRepository>().DeleteAsync(findRoleByName);
            return new RoleReponse(null, findRoleByName.RoleName);
        }

        public async Task<RoleReponse> GetRole(string request)
        {
            var result = await _unitOfWork.GetRepository<IRoleRepository>().FindByName(request);
            return new RoleReponse(result.RoleId, result.RoleName);
        }
    }
}
