using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Core.Models.reponse
{
    public class CourseWithCompletionStatusResponse
    {
        public Guid CourseID { get; set; }
        public string CourseTitle { get; set; }
        public bool IsCompleted { get; set; }
        public int TotalLessons { get; set; }
        public int CompletedLessonsCount { get; set; }
        public int ProgressPercentage { get; set; }
    }
}
