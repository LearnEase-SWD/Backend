using LearnEase.Repository.IRepository;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LearnEase_Api.LearnEase.Core.Services
{

        public class CourseService : ICourseService
        {
            private readonly ICourseRepository _courseRepo;

            public CourseService(ICourseRepository courseRepo)
            {
                _courseRepo = courseRepo;
            }

            public async Task<IEnumerable<Course>> GetAllCoursesAsync()
            {
                return await _courseRepo.GetAllCoursesAsync();
            }

            public async Task<Course?> GetCourseByIdAsync(Guid id)
            {
                return await _courseRepo.GetCourseByIdAsync(id);
            }

            public async Task<Course> CreateCourseAsync(Course course)
            {
                if (course == null)
                    throw new ArgumentNullException(nameof(course), "Course cannot be null");

                if (course.CourseID == Guid.Empty)
                    course.CourseID = Guid.NewGuid();

                await _courseRepo.CreateAsync(course);
                await _courseRepo.SaveAsync();
                return course;
            }

            public async Task<bool> UpdateCourseAsync(Guid id, Course course)
            {
                if (course == null)
                    throw new ArgumentNullException(nameof(course), "Course cannot be null");

                var existingCourse = await _courseRepo.GetCourseByIdAsync(id);
                if (existingCourse == null) return false;

                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Price = course.Price;
                existingCourse.Language = course.Language;
                existingCourse.DifficultyLevel = course.DifficultyLevel;
                existingCourse.UpdatedAt = DateTime.UtcNow;

                await _courseRepo.UpdateAsync(existingCourse);
                await _courseRepo.SaveAsync();
                return true;
            }

            public async Task<bool> DeleteCourseAsync(Guid id)
            {
                var existingCourse = await _courseRepo.GetCourseByIdAsync(id);
                if (existingCourse == null) return false;

                try
                {
                    await _courseRepo.DeleteAsync(id);
                    await _courseRepo.SaveAsync();
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