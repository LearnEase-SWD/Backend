using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Core.DTO
{
    public class CourseDTO
    {
        public Guid CourseID { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int TotalLessons { get; set; }

        public string DifficultyLevel { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
