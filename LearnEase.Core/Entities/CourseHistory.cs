using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Entities
{
	public class CourseHistory
	{
		[Key]
		public Guid PurchaseID { get; set; }

		[ForeignKey("User")]
		public string UserID { get; set; }

		[ForeignKey("Course")]
		public Guid CourseID { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		public DateTime PurchasedAt { get; set; }

		public virtual User User { get; set; }
		public virtual Course Course { get; set; }
	}
}
