using LearnEase_Api.dto.reponse;
using LearnEase_Api.dto.request;
using LearnEase_Api.Entity;

namespace LearnEase_Api.mapper
{
    public class UserDetailsMapper
    {
        public UserDetailResponse ToUserDetailResponse(UserDetail userDetail)
        {
            if (userDetail == null)
                throw new ArgumentNullException(nameof(userDetail));

            return new UserDetailResponse(
                userDetail.Id,
                userDetail.firstName,
                userDetail.lastName,
                userDetail.phone,
                userDetail.imageUrl,
                userDetail.dbo,
                userDetail.address,
                userDetail.CreatedUser,
                userDetail.UpdatedUser,
                userDetail.UserId
            );
        }

        public  UserDetail ToUserDetailEntity(UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null)
                throw new ArgumentNullException(nameof(userDetailRequest));

            return new UserDetail
            {
                firstName = userDetailRequest.firstName,
                lastName = userDetailRequest.lastName,
                phone = userDetailRequest.phone,
                imageUrl = userDetailRequest.imageUrl,
                dbo = userDetailRequest.dbo,
                address = userDetailRequest.address,
                CreatedUser = userDetailRequest.CreatedUser,
                UpdatedUser = userDetailRequest.UpdatedUser,
                UserId = userDetailRequest.UserId,
            };
        }

        public  UserDetailRequest ToUserDetailRequest(UserDetail userDetail)
        {
            if (userDetail == null)
                throw new ArgumentNullException(nameof(userDetail));

            return new UserDetailRequest(userDetail.firstName, userDetail.lastName,
                userDetail.phone, userDetail.imageUrl, userDetail.dbo, userDetail.address, userDetail.CreatedUser,
                userDetail.UpdatedUser, userDetail.UserId)
            {
                
            };
        }
    }
}
