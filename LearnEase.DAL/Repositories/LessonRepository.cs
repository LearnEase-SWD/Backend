﻿using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lesson> GetLessonByCourseId(Guid courseId)
        {
            return await _context.Lessons.FirstOrDefaultAsync(lesson => lesson.CourseID == courseId);
        }
    }
}
