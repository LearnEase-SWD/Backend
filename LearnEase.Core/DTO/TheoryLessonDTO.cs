using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Core.DTO
{
	public class TheoryLessonDTO
	{
		public Guid TheoryID { get; set; }
		public Guid LessonID { get; set; }
		public string Content { get; set; }
		public string Examples { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now; 
	}
}
