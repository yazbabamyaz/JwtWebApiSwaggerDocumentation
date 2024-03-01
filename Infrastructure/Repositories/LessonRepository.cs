using LessonApi.Infrastructure.Context;
using LessonApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LessonApi.Infrastructure.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// LessonName  göre sorgulama yapar
        /// </summary>
        /// <param name="lessonName"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(string lessonName)
        {
           return await _context.Lessons.AnyAsync(x=>x.LessonName.ToLower().Trim().Equals(lessonName.ToLower().Trim()));
        }
    }
}
