using System;

namespace LearnEase_Api.Entity
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string userName { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public string? CreatedUser { get; set; }
        public string? UpdatedUser { get; set; }

        public UserDetail UserDetail { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}