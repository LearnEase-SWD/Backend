using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Models.Request
{
    public class LessonCreateRequest
    {
        [Required(ErrorMessage = "CourseID không được để trống.")]
        public Guid CourseID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Index phải là số nguyên dương.")]
        public int Index { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        [MaxLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Loại bài học không được để trống.")]
        [MaxLength(50, ErrorMessage = "Loại bài học không được vượt quá 50 ký tự.")]
        public string LessonType { get; set; }
    }
}
