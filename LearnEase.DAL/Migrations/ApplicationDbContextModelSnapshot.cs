﻿// <auto-generated />
using System;
using LearnEase.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LearnEase.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LearnEase_Api.Entity.Course", b =>
                {
                    b.Property<Guid>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("TotalLessons")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Exercise", b =>
                {
                    b.Property<Guid>("ExerciseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnswerOptions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LessonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ExerciseID");

                    b.HasIndex("LessonID");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Flashcard", b =>
                {
                    b.Property<Guid>("FlashcardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LessonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PronunciationAudioURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlashcardID");

                    b.HasIndex("LessonID");

                    b.ToTable("Flashcards");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Lesson", b =>
                {
                    b.Property<Guid>("LessonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int>("LessonType")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("LessonID");

                    b.HasIndex("CourseID");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.TheoryLesson", b =>
                {
                    b.Property<Guid>("TheoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Examples")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LessonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TheoryID");

                    b.HasIndex("LessonID")
                        .IsUnique();

                    b.ToTable("TheoryLessons");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.UserCourse", b =>
                {
                    b.Property<Guid>("UserCourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EnrolledAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProgressPercentage")
                        .HasColumnType("int");

                    b.Property<string>("ProgressStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserCourseID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserId");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.UserFlashcard", b =>
                {
                    b.Property<Guid>("UserFlashcardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FlashcardID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Progress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserFlashcardID");

                    b.HasIndex("FlashcardID");

                    b.HasIndex("UserID");

                    b.ToTable("UserFlashcards");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.UserLesson", b =>
                {
                    b.Property<Guid>("ProgressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastAccessedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LessonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProgressID");

                    b.HasIndex("LessonID")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("UserLesson");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.VideoLesson", b =>
                {
                    b.Property<Guid>("VideoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<Guid>("LessonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VideoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoID");

                    b.HasIndex("LessonID")
                        .IsUnique();

                    b.ToTable("VideoLessons");
                });

            modelBuilder.Entity("UserExercise", b =>
                {
                    b.Property<Guid>("AttemptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AttemptAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExerciseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Progress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAnswer")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AttemptID");

                    b.HasIndex("ExerciseID");

                    b.HasIndex("UserID");

                    b.ToTable("UserExercises");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Exercise", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Lesson", "Lesson")
                        .WithMany("Exercises")
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Flashcard", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Lesson", "Lesson")
                        .WithMany("Flashcards")
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Lesson", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.TheoryLesson", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Lesson", "Lesson")
                        .WithOne("TheoryLesson")
                        .HasForeignKey("LearnEase_Api.Entity.TheoryLesson", "LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.UserCourse", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnEase_Api.Entity.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.UserFlashcard", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Flashcard", "Flashcard")
                        .WithMany("UserFlashcards")
                        .HasForeignKey("FlashcardID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LearnEase_Api.Entity.User", "User")
                        .WithMany("UserFlashcards")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Flashcard");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.UserLesson", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Lesson", "Lesson")
                        .WithOne("UserProgress")
                        .HasForeignKey("LearnEase_Api.Entity.UserLesson", "LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnEase_Api.Entity.User", "User")
                        .WithMany("UserProgresses")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.VideoLesson", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Lesson", "Lesson")
                        .WithOne("VideoLesson")
                        .HasForeignKey("LearnEase_Api.Entity.VideoLesson", "LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("UserExercise", b =>
                {
                    b.HasOne("LearnEase_Api.Entity.Exercise", "Exercise")
                        .WithMany("UserExercises")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnEase_Api.Entity.User", "User")
                        .WithMany("UserExercises")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Course", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("UserCourses");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Exercise", b =>
                {
                    b.Navigation("UserExercises");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Flashcard", b =>
                {
                    b.Navigation("UserFlashcards");
                });

            modelBuilder.Entity("LearnEase_Api.Entity.Lesson", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("Flashcards");

                    b.Navigation("TheoryLesson")
                        .IsRequired();

                    b.Navigation("UserProgress")
                        .IsRequired();

                    b.Navigation("VideoLesson")
                        .IsRequired();
                });

            modelBuilder.Entity("LearnEase_Api.Entity.User", b =>
                {
                    b.Navigation("UserCourses");

                    b.Navigation("UserExercises");

                    b.Navigation("UserFlashcards");

                    b.Navigation("UserProgresses");
                });
#pragma warning restore 612, 618
        }
    }
}
