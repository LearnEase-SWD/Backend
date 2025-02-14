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
        public string Title { get; set; } 

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Post> Posts { get; set; }
    }

}
