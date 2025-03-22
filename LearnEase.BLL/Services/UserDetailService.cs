using LearnEase.Repository.UOW;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using LearnEase_Api.LearnEase.Infrastructure.Repositories;
using LearnEase_Api.Mapper;
using System.Threading.Tasks;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserDetailsMapper _userDetailsMapper = new UserDetailsMapper();

        public UserDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDetailResponse> CreateUserDetail(UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null) throw new ArgumentNullException(nameof(userDetailRequest));

            var userDetailEntity = _userDetailsMapper.ToUserDetailEntity(userDetailRequest);

            if (userDetailEntity == null)
                return null;

            await _unitOfWork.GetRepository<UserDetail>().CreateAsync(userDetailEntity);

            return _userDetailsMapper.ToUserDetailResponse(userDetailEntity);
        }


        public async Task<UserDetailResponse> getUserDetailById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var userDetail = await _unitOfWork.GetRepository<UserDetail>().GetByIdAsync(id);

            if (userDetail == null)
                return null;
            return _userDetailsMapper.ToUserDetailResponse(userDetail);
        }


        public async Task<UserDetailResponse> getUserDetailByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var userDetail = await _unitOfWork.GetCustomRepository<IUserDetailRepository>().GetUserDetailByUserId(userId);

            if (userDetail == null)
                return null;


            return _userDetailsMapper.ToUserDetailResponse(userDetail);
        }


        public async Task<UserDetailResponse> UpdateUserDetail(UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null) throw new ArgumentNullException(nameof(userDetailRequest));


            var userDetailEntity = _userDetailsMapper.ToUserDetailEntity(userDetailRequest);

            if (userDetailEntity == null)
                return null;

            await _unitOfWork.GetRepository<UserDetail>().UpdateAsync(userDetailEntity);
            
            return _userDetailsMapper.ToUserDetailResponse(userDetailEntity);
        }
    }
}
