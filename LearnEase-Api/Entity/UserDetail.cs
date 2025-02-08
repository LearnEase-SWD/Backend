namespace LearnEase_Api.Entity
{
    public class UserDetail
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? phone { get; set; }
        public string? imageUrl { get; set; }
        public DateTime? dbo { get; set; }
        public string? address { get; set; }
        public string? CreatedUser { get; set; }
        public string? UpdatedUser { get; set; }

        public string? UserId { get; set; }
        public User User { get; set; }
    }
}
