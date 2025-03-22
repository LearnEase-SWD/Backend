using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Entity;

namespace LearnEase_Api.Mapper
{
    public class MapperUser
    {
        public UserReponse MapperUserReponse(User user)
        {
            UserReponse userReponse =
                new (user.UserId, user.UserName, user.Email, user.IsActive,
                user.CreatedAt, user.UpdatedAt);
            return userReponse;
        }
    }
}
