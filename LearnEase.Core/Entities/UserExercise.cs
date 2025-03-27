using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Entities;

public class UserExercise
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AttemptID { get; set; }

    [ForeignKey("User")]
    public string UserID { get; set; }
    public User User { get; set; }

    [ForeignKey("Exercise")]
    public Guid ExerciseID { get; set; }
    public Exercise Exercise { get; set; }

    [Required]
    [MaxLength(20)]
    public string UserAnswer { get; set; }

	[Required]
	public string Progress { get; set; }

	public DateTime AttemptAt { get; set; } = DateTime.UtcNow;
}
