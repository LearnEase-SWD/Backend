namespace LearnEase_Api.Dtos.request
{
    public record UserDetailRequest(
        string firstName,
        string lastName,
        string phone,
        string imageUrl,
        DateTime? dbo,
        string? address,
        string? CreatedUser,
        string? UpdatedUser,
        string UserId
)
    {
    }
}
