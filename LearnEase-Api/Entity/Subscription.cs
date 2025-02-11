using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SubscriptionID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(50)]
        public string PlanType { get; set; } // Monthly, Yearly

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } // Active, Expired
    }
}
