using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Models.Request
{
    public class TheoryLessonCreationRequest
    {
        [Required]
        public Guid LessonID { get; set; }

        [Required(ErrorMessage = "Nội dung bài học không được để trống.")]
        [Url(ErrorMessage = "Đường dẫn phải đúng định dạng")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Ví dụ không được để trống.")]
        [MaxLength(500, ErrorMessage = "Ví dụ không được dài quá 500 ký tự.")]
        public string Examples { get; set; }
    }
}
