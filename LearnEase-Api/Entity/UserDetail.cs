﻿namespace LearnEase_Api.Entity
{
    public class UserDetail
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? Dbo { get; set; }
        public string? Address { get; set; }
        public string? CreatedUser { get; set; }
        public string? UpdatedUser { get; set; }

        public string? UserId { get; set; }
        public User User { get; set; }
    }
}
