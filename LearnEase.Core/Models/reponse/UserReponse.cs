﻿namespace LearnEase_Api.Dtos.reponse
{
    public record UserReponse(string id, string userName, string email, bool isActive,
         string? CreatedUser, string? UpdatedUser)
    {
    }
}
