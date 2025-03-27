using AutoMapper;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<TheoryLessonCreationRequest, TheoryLesson>();
            CreateMap<LessonCreationRequest, Lesson>();

        }

    }
}
