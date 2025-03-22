using LearnEase.Repository.UOW;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;

namespace LearnEase_Api.LearnEase.Core.Services
{

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GetRepository<Course>().Entities;
            var paginatedResult = await _unitOfWork.GetRepository<Course>().GetPagging(query, pageIndex, pageSize);
            return paginatedResult.Items;
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Course>().GetByIdAsync(id);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course), "Course cannot be null");

            if (course.CourseID == Guid.Empty)
                course.CourseID = Guid.NewGuid();

            await _unitOfWork.GetRepository<Course>().CreateAsync(course);
            await _unitOfWork.SaveAsync();
            return course;
        }

        public async Task<bool> UpdateCourseAsync(Guid id, Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course), "Course cannot be null");

            var existingCourse = await _unitOfWork.GetRepository<Course>().GetByIdAsync(id);
            if (existingCourse == null) return false;

            existingCourse.CourseName = course.CourseName;
            existingCourse.CourseDescription = course.CourseDescription;
            existingCourse.Price = course.Price;
            existingCourse.Language = course.Language;
            existingCourse.DifficultyLevel = course.DifficultyLevel;
            existingCourse.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.GetRepository<Course>().UpdateAsync(existingCourse);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var existingCourse = await _unitOfWork.GetRepository<Course>().GetByIdAsync(id);
            if (existingCourse == null) return false;

            try
            {
                await _unitOfWork.GetRepository<Course>().DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log error here
                Console.WriteLine($"Error deleting course: {ex.Message}");
                return false;
            }


        }
    }

}