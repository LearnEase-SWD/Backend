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

        public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            return await _lessonRepository.Entities
                .Include(l => l.Course)
                .Include(l => l.VideoLesson)
                .Include(l => l.TheoryLesson)
                .Include(l => l.Exercises)
                .Include(l => l.Flashcards)
                .ToListAsync();
        }

        public async Task<Lesson?> GetLessonByIdAsync(Guid id)
        {
            return await _lessonRepository.Entities
                .Include(l => l.Course)
                .Include(l => l.VideoLesson)
                .Include(l => l.TheoryLesson)
                .Include(l => l.Exercises)
                .Include(l => l.Flashcards)
                .FirstOrDefaultAsync(l => l.LessonID == id);
        }

        public async Task CreateLessonAsync(Lesson lesson)
        {
            await _lessonRepository.CreateAsync(lesson);
            await _lessonRepository.SaveAsync();
        }

        public async Task<bool> UpdateLessonAsync(Guid id, Lesson lesson)
        {
            var existingLesson = await _lessonRepository.GetByIdAsync(id);
            if (existingLesson == null) return false;

            existingLesson.Title = lesson.Title;
            existingLesson.LessonType = lesson.LessonType;
            existingLesson.Index = lesson.Index;
            existingLesson.CourseID = lesson.CourseID;
            existingLesson.CreatedAt = DateTime.UtcNow;

            await _lessonRepository.UpdateAsync(existingLesson);
            await _lessonRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteLessonAsync(Guid id)
        {
            var existingLesson = await _lessonRepository.GetByIdAsync(id);
            if (existingLesson == null) return false;

            await _lessonRepository.DeleteAsync(id);
            await _lessonRepository.SaveAsync();
            return true;
        }



        }
}