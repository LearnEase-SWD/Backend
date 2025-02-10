using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        public string? CreatedUser { get; set; }
        public string? UpdatedUser { get; set; }

        public UserDetail UserDetail { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public Leaderboard Leaderboard { get; set; }
    }
}