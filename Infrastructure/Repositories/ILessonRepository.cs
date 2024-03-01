using LessonApi.Models.Entities;

namespace LessonApi.Infrastructure.Repositories
{
    public interface ILessonRepository:IGenericRepository<Lesson>
    {
        Task<bool> AnyAsync(string lessonName);
    }
    
}
