using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Post
    {
        [Key]
        public Guid PostID { get; set; }

        [ForeignKey("Topic")]
        public Guid TopicID { get; set; }
        public Topic Topic { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ContentHtml { get; set; }

        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Comment> Comments { get; set; }
    }

}
