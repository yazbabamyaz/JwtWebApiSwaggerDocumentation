using LessonApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonApi.Infrastructure.Config
{
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasData(
                new Lesson { Id=1, LessonName = "Matematik", Description = "Matematik dersi açıklama bilgisi", LessonPrice = 400 },
                new Lesson { Id=2, LessonName = "Türkçe", Description = "Türkçe dersi açıklama bilgisi", LessonPrice = 200 },
                new Lesson {Id=3, LessonName = "Fizik", Description = "Fizik dersi açıklama bilgisi", LessonPrice = 400 },
                new Lesson {Id=4, LessonName = "Kimya", Description = "Kimya dersi açıklama bilgisi", LessonPrice = 300 }
            );
        }
    }
}
