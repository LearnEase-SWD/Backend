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
            CreateMap<TheoryLessonResponse, TheoryLesson>();
            CreateMap<TheoryLesson, TheoryLessonResponse>();
            CreateMap<LessonCreateRequest, Lesson>();
			
			CreateMap<FlashcardRequest, Flashcard>();
			CreateMap<FlashcardResponse, Flashcard>();

			CreateMap<CourseRequest, Course>();
			CreateMap<Course, CourseRequest>();
			CreateMap<CourseResponse, Course>();
			CreateMap<Course, CourseResponse>();

			CreateMap<Lesson, LessonResponse>();
			CreateMap<LessonResponse, Lesson>();
			CreateMap<LessonCreateRequest, Lesson>();
			CreateMap<Lesson, LessonCreateRequest>();

			CreateMap<Topic, TopicResponse>();
			CreateMap<TopicResponse, Topic>();
			
			CreateMap<Exercise, ExerciseRequest>();
			CreateMap<ExerciseRequest, Exercise>();
			CreateMap<ExerciseResponse, Exercise>();
			CreateMap<Exercise, ExerciseResponse>();

			CreateMap<UserCourseResponse, UserCourse>();
			CreateMap<UserCourse, UserCourseResponse>();

			CreateMap<VideoLesson, VideoLessonCreateRequest>();
			CreateMap<VideoLessonCreateRequest, VideoLesson>();
			CreateMap<VideoLessonResponse, VideoLesson>();
			CreateMap<VideoLesson, VideoLessonResponse>();
			
			CreateMap<Flashcard, FlashcardResponse>();
			CreateMap<FlashcardResponse, Flashcard>();
		}
    }
}
