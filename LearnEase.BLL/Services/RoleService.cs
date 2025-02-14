using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoles _roleRepository;
        public RoleService(IRoles roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleReponse> createRole(RoleRequest request)
        {
            Role role = new Role();
            role.RoleName = request.name;

            var result = await _roleRepository.createRole(role);
            return new RoleReponse(null, result.RoleName);
        }

        public async Task<RoleReponse> deleteRole(RoleRequest request)
        {
            var findRoleByName = await _roleRepository.findByName(request.name);

            var result = await _roleRepository.deleteRole(findRoleByName);
            return new RoleReponse(null, result.RoleName);
        }

        public async Task<List<RoleReponse>> getAllRoles()
        {
            var result = await _roleRepository.getAllRoles();
            return result.ConvertAll(role => new RoleReponse(role.RoleId, role.RoleName));
        }

        public async Task<RoleReponse> getRole(string request)
        {
            var result = await _roleRepository.findByName(request);
            return new RoleReponse(result.RoleId, result.RoleName);
        }
    }
}
