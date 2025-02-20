namespace LearnEase_Api.Dtos.reponse
{
    public record UserDetailResponse(string Id,
        string firstName,
        string lastName,
        string phone,
        string imageUrl,
        DateTime? dbo,
        string address,
        string? CreatedUser,
        string? UpdatedUser,
        string UserId)
    {
    }
}
