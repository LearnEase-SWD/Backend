namespace LearnEase_Api.Entity
{
    public class Role
    {
        public string RoleId { get; set; } 
        public string RoleName { get; set; } 
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
