using LearnEase_Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase_Api.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<TheoryLesson> TheoryLessons { get; set; }
        public DbSet<VideoLesson> VideoLessons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserExercise> UserExercises { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
        public DbSet<UserFlashcard> UserFlashcards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

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

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
