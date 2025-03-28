﻿using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<User>>> GetUserAsync(int pageIndex, int pageSize);
		Task<UserReponse> GetUserReponseById(string id);
        Task<UserReponse> CreateNewUser(UserCreateRequest request);
        Task<UserReponse> UpdateUserReponse(UserUpdateRequest request, string id);
        Task<UserReponse> DeleteUserReponseById(string id);
        Task<UserReponse> FindUserByEmail(string email);
    }
}
