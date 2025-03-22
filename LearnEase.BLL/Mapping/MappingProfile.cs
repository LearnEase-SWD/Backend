using AutoMapper;
using LearnEase.Core.Models.Request;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;

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
