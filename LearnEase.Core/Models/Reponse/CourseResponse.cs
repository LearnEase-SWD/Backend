using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Core.Models.Reponse
{
    public class CourseResponse
    {
        public Guid CourseID { get; set; }
		public Guid TopicID { get; set; }
		public string Title { get; set; }
        public decimal Price { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public int TotalLessons { get; set; }
        public string DifficultyLevel { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
