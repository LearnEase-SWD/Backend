namespace LearnEase.Core.Models.Reponse
{
    public record UserReponse(string id, string userName, string email, bool isActive,
        string? ImageUrl, string? CreatedUser)
    {
    }
}
