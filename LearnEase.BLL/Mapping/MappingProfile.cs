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
			CreateMap<CourseRequest, Course>();
			CreateMap<FlashcardRequest, Flashcard>();
			CreateMap<CourseResponse, Course>();
			CreateMap<Course, CourseResponse>();
		}

    }
}
