using LearnEase_Api.dto.reponse;
using LearnEase_Api.dto.request;
using LearnEase_Api.Entity;
using LearnEase_Api.mapper;
using LearnEase_Api.Repository.UserDetailRepository;
using System.Threading.Tasks;

namespace LearnEase_Api.Models.UserDetailService
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly UserDetailsMapper _userDetailsMapper = new UserDetailsMapper();

        public UserDetailService(IUserDetailRepository userDetailRepository)
        {
            _userDetailRepository = userDetailRepository;
        }

        public async Task<UserDetailResponse> CreateUserDetail(UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null) throw new ArgumentNullException(nameof(userDetailRequest));

            var userDetailEntity = _userDetailsMapper.ToUserDetailEntity(userDetailRequest);

            var createdUserDetail = await _userDetailRepository.CreateUserDetail(userDetailEntity);

            if (createdUserDetail == null)
                return null; 

            return _userDetailsMapper.ToUserDetailResponse(createdUserDetail);
        }

        
        public async Task<UserDetailResponse> getUserDetailById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var userDetail = await _userDetailRepository.getUserDetailById(id);

            if (userDetail == null)
                return null; 
            return _userDetailsMapper.ToUserDetailResponse(userDetail);
        }

       
        public async Task<UserDetailResponse> getUserDetailByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var userDetail = await _userDetailRepository.getUserDetailByUserId(userId);

            if (userDetail == null)
                return null; 

            
            return _userDetailsMapper.ToUserDetailResponse(userDetail);
        }

        
        public async Task<UserDetailResponse> UpdateUserDetail(UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null) throw new ArgumentNullException(nameof(userDetailRequest));

            
            var userDetailEntity = _userDetailsMapper.ToUserDetailEntity(userDetailRequest);

           
            var updatedUserDetail = await _userDetailRepository.UpdateUserDetail(userDetailEntity);

            if (updatedUserDetail == null)
                return null; 
            return _userDetailsMapper.ToUserDetailResponse(updatedUserDetail);
        }
    }
}
