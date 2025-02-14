using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;

namespace LearnEase_Api.Mapper
{
    public class UserDetailsMapper
    {
        public UserDetailResponse ToUserDetailResponse(UserDetail userDetail)
        {
            if (userDetail == null)
                throw new ArgumentNullException(nameof(userDetail));

            return new UserDetailResponse(
                userDetail.Id,
                userDetail.FirstName,
                userDetail.LastName,
                userDetail.Phone,
                userDetail.ImageUrl,
                userDetail.DateOfBirth,
                userDetail.Address,
                userDetail.CreatedAt,
                userDetail.UpdatedAt,
                userDetail.UserId
            );
        }

        public UserDetail ToUserDetailEntity(UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null)
                throw new ArgumentNullException(nameof(userDetailRequest));

            return new UserDetail
            {
                FirstName = userDetailRequest.firstName,
                LastName = userDetailRequest.lastName,
                Phone = userDetailRequest.phone,
                ImageUrl = userDetailRequest.imageUrl,
                DateOfBirth = userDetailRequest.dbo,
                Address = userDetailRequest.address,
                CreatedAt = userDetailRequest.CreatedUser,
                UpdatedAt = userDetailRequest.UpdatedUser,
                UserId = userDetailRequest.UserId,
            };
        }

        public UserDetailRequest ToUserDetailRequest(UserDetail userDetail)
        {
            if (userDetail == null)
                throw new ArgumentNullException(nameof(userDetail));

            return new UserDetailRequest(userDetail.FirstName, userDetail.LastName,
                userDetail.Phone, userDetail.ImageUrl, userDetail.DateOfBirth, userDetail.Address, userDetail.CreatedAt,
                userDetail.UpdatedAt, userDetail.UserId)
            {

            };
        }
    }
}
