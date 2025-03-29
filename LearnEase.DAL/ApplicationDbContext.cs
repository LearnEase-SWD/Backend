using LearnEase.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<TheoryLesson> TheoryLessons { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<CourseHistory> CourseHistories { get; set; }
        public DbSet<VideoLesson> VideoLessons { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserExercise> UserExercises { get; set; }
        public DbSet<UserLesson> UserLesson { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            // Cấu hình quan hệ một-nhiều giữa Lesson và TheoryLesson
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.TheoryLessons)
                .WithOne(tl => tl.Lesson)
                .HasForeignKey(tl => tl.LessonID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ một-nhiều giữa Lesson và VideoLesson
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.VideoLessons)
                .WithOne(vl => vl.Lesson)
                .HasForeignKey(vl => vl.LessonID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
