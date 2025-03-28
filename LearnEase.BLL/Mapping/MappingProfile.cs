using AutoMapper;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<TheoryLessonCreateRequest, TheoryLesson>();
            CreateMap<LessonCreateRequest, Lesson>();
			
			CreateMap<FlashcardRequest, Flashcard>();

			CreateMap<CourseRequest, Course>();
			CreateMap<CourseResponse, Course>();
			CreateMap<Course, CourseResponse>();

			CreateMap<Lesson, LessonResponse>();
			CreateMap<LessonResponse, Lesson>();

			CreateMap<Topic, TopicResponse>();
			CreateMap<TopicResponse, Topic>();
			
			CreateMap<Exercise, ExerciseRequest>();
			CreateMap<ExerciseRequest, Exercise>();

			CreateMap<UserCourseResponse, UserCourse>();
			CreateMap<UserCourse, UserCourseResponse>();
		}

    }
}
