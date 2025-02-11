using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Forum
    {
        [Key]
        public Guid ForumID { get; set; }

        [ForeignKey("Course")]
        public Guid CourseID { get; set; }
        public Course Course { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Topic> Topics { get; set; }
    }
}
