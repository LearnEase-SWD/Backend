using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using LearnEase_Api.Mapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly MapperUser _mapper = new MapperUser();
        private readonly IRoleService _roleService;
        private readonly IUserDetailService _userDetailService;

        public UserService(IUserRepository userRepository, IRoleService roleService, IUserDetailService userDetailService)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _userDetailService = userDetailService;
        }

        public async Task<UserReponse> createNewUser(userCreationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var findUser = await _userRepository.FindByEmail(request.email);
            if (findUser != null) return null;

            var user = new User
            {
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                IsActive = true,
                Email = request.email,
                UserName = request.userName
            };
            string[] userNames = request.userName.Split(' ');


            var defaultRole = await _roleService.getRole("User");
            if (defaultRole != null)
            {
                user.UserRoles = new List<UserRole>
            {
                new UserRole { UserId = user.UserId, RoleId = defaultRole.id }
            };
            }

            var result = await _userRepository.createNewUser(user);
            var getUserEmail = await _userRepository.FindByEmail(user.Email);


            //save detail
            var saveUserDetail = await _userDetailService.CreateUserDetail(new UserDetailRequest(userNames[0],
               userNames[1], null, null, null, null, user.CreatedAt, user.UpdatedAt, getUserEmail.UserId));
            return _mapper.mapperUserReponse(result);
        }

        public async Task<UserReponse> deleteUserReponseById(string id)
        {
            var findUserById = await _userRepository.FindById(id);
            if (findUserById == null) return null;
            var result = await _userRepository.deleteUser(id);
            return _mapper.mapperUserReponse(result);
        }

        public async Task<UserReponse> findUserByEmail(string email)
        {
            var result = await _userRepository.FindByEmail(email);
            if (result != null)
            {
                return _mapper.mapperUserReponse(result);
            }
            return null;

        }

        public async Task<List<UserReponse>> getAllUser()
        {
            var result = await _userRepository.GetAll();
            return result.ConvertAll(user => _mapper.mapperUserReponse(user));
        }

        public async Task<UserReponse> getUserReponseById(string id)
        {
            var result = await _userRepository.FindById(id);
            return result != null ? _mapper.mapperUserReponse(result) : null;
        }

        public async Task<UserReponse> updateUserReponse(UserUpdateRequest request, string id)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var findUser = await _userRepository.FindById(id);
            if (findUser == null) return null;

            findUser.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            findUser.IsActive = true;
            findUser.Email = request.email;
            findUser.UserName = request.userName;

            var result = await _userRepository.updateUser(findUser, id);
            return _mapper.mapperUserReponse(result);
        }
    }
}
