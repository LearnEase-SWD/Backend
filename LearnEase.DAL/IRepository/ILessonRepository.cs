
using LearnEase.Core;
using LearnEase.Core.Entities;

namespace LearnEase.Repository.IRepository
{
    public interface ILessonRepository
    {
		Task<BasePaginatedList<Lesson>> GetLessonsByCourseId(Guid courseId, int pageIndex, int pageSize);
	}
}
