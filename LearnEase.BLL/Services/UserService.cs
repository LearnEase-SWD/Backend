using LearnEase.Repository.UOW;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using LearnEase_Api.LearnEase.Infrastructure.Repository;
using LearnEase_Api.Mapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;
        private readonly ILogger<UserRepository> _logger;
        private readonly MapperUser _mapper = new MapperUser();

        public UserService(IUnitOfWork unitOfWork, ILogger<UserRepository> logger,IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UserReponse> CreateNewUser(userCreationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var findUser = await _unitOfWork.GetRepository<IUserRepository>().FindByEmail(request.email);
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
            string lastname = "";

            if (userNames.Length > 1)
            {
                for (var i = 1; i < userNames.Length; i++)
                {
                    lastname += userNames[i]+" ";
                }
            }

            var defaultRole = await _roleService.GetRole("User");
            if (defaultRole != null)
            {
                user.UserRoles = new List<UserRole>
            {
                new() { UserId = user.UserId, RoleId = defaultRole.id }
            };
            }

            await _unitOfWork.GetRepository<IUserRepository>().CreateAsync(user);
            await _unitOfWork.SaveAsync();

            var getUserEmail = await _unitOfWork.GetRepository<IUserRepository>().FindByEmail(user.Email);

            Console.WriteLine("hello");
            string urlImage = null;
            if (request.urlImage != null)
            {
                urlImage = request.urlImage;
            }


            //save detail
            var userDetail = new UserDetail
            {
                FirstName = userNames[0],
                LastName =lastname,
                Address = null,
                Phone = null,
                ImageUrl = urlImage,
                DateOfBirth = null,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                UserId = getUserEmail.UserId
            };

            await _unitOfWork.GetRepository<IUserDetailRepository>().CreateAsync(userDetail);
            await _unitOfWork.SaveAsync();

            return _mapper.mapperUserReponse(user);
        }

        public async Task<UserReponse> DeleteUserReponseById(string id)
        {
            var userRepo = _unitOfWork.GetRepository<IUserRepository>();
            var userDetailRepo = _unitOfWork.GetRepository<IUserDetailRepository>();

            // Lấy User trước khi xóa
            var findUserById = await userRepo.GetByIdAsync(id);
            if (findUserById == null) return null;

            var userResponse = _mapper.mapperUserReponse(findUserById); 

            var userDetail = await userDetailRepo.GetByIdAsync(id);
            if (userDetail != null)
            {
                await userDetailRepo.DeleteAsync(userDetail);
            }

            // Xóa User
            await userRepo.DeleteAsync(findUserById);

            return userResponse;
        }

        public async Task<UserReponse> FindUserByEmail(string email)
        {
            var result = await _unitOfWork.GetRepository<IUserRepository>().FindByEmail(email);
            if (result != null)
            {
                return _mapper.mapperUserReponse(result);
            }
            return null;

        }
        
        public async Task<UserReponse> GetUserReponseById(string id)
        {
            var result = await _unitOfWork.GetRepository<IUserRepository>().GetByIdAsync(id);
            return result != null ? _mapper.mapperUserReponse(result) : null;
        }

        public async Task<UserReponse> UpdateUserReponse(UserUpdateRequest request, string id)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var findUser = await _unitOfWork.GetRepository<IUserRepository>().GetByIdAsync(id);
            if (findUser == null) return null;

            findUser.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            findUser.IsActive = true;
            findUser.Email = request.email;
            findUser.UserName = request.userName;

            await _unitOfWork.GetRepository<IUserRepository>().UpdateAsync(findUser);
            return _mapper.mapperUserReponse(findUser);
        }
    }
}
