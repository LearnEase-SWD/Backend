using LearnEase_Api.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class UserExercise
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserExerciseID { get; set; }

    [ForeignKey("User")]
    public string UserID { get; set; }
    public User User { get; set; }

    [ForeignKey("Exercise")]
    public Guid ExerciseID { get; set; }
    public Exercise Exercise { get; set; }

    [Required]
    [MaxLength(20)]
    public string CompletionStatus { get; set; } // Completed, In Progress, Failed

    public int Score { get; set; }

    public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
