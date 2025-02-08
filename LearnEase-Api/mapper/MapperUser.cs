using LearnEase_Api.dto.reponse;
using LearnEase_Api.Entity;

namespace LearnEase_Api.mapper
{
    public class MapperUser
    {
        public UserReponse mapperUserReponse(User user)
        {
            UserReponse userReponse = 
                new UserReponse(user.Id,user.userName,user.email,user.isActive,
                user.CreatedUser,user.UpdatedUser);
            return userReponse;
        }
    }
}
