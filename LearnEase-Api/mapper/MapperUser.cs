using LearnEase_Api.dto.reponse;
using LearnEase_Api.Entity;

namespace LearnEase_Api.mapper
{
    public class MapperUser
    {
        public UserReponse mapperUserReponse(User user)
        {
            UserReponse userReponse = 
                new UserReponse(user.UserId,user.UserName,user.Email,user.IsActive,
                user.CreatedAt,user.UpdatedAt);
            return userReponse;
        }
    }
}
