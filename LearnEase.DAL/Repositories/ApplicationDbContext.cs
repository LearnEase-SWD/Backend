using LearnEase_Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
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
        public DbSet<VideoLesson> VideoLessons { get; set; }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserExercise> UserExercises { get; set; }
        public DbSet<UserLesson> UserProgress { get; set; }
        public DbSet<UserFlashcard> UserFlashcards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserFlashcard>()
               .HasOne(uf => uf.User)
               .WithMany(u => u.UserFlashcards)
               .HasForeignKey(uf => uf.UserID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserFlashcard>()
                .HasOne(uf => uf.Flashcard)
                .WithMany(f => f.UserFlashcards)
                .HasForeignKey(uf => uf.FlashcardID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);
        }
    }
}
