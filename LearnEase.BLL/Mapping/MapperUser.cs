using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;

namespace LearnEase_Api.Mapper
{
	public class MapperUser
	{
		public UserReponse MapperUserReponse(User user)
		{
			UserReponse userReponse =
				new(user.UserId, user.UserName, user.Email, user.IsActive, user.ImageUrl,
				user.CreatedAt);
			return userReponse;
		}
	}
}
