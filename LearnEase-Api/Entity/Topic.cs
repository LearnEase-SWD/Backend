using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Topic
    {
        [Key]
        public Guid TopicID { get; set; }

        [ForeignKey("Forum")]
        public Guid ForumID { get; set; }
        public Forum Forum { get; set; }

        [Required]
        public string Title { get; set; } // VD: Hỏi đáp bài tập, Thảo luận, Góp ý khóa học

        public string Description { get; set; } // Mô tả về chủ đề

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Một chủ đề có nhiều bài viết
        public List<Post> Posts { get; set; }
    }

}
