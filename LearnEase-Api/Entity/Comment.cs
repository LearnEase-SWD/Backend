using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Comment
    {
        [Key]
        public Guid CommentID { get; set; }

        [ForeignKey("Post")]
        public Guid PostID { get; set; }
        public Post Post { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
