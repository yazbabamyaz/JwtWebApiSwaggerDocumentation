using LessonApi.Infrastructure.Config;
using LessonApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LessonApi.Infrastructure.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LessonConfig());
            //yapılandırma sınıfların çoksa ve tek tek eklemek istemiyorsan:aşağıdaki kodu kullan.
            //mevcut assembly'deki IEntityTypeConfiguration dan implement edilen bütün classları ilgilendirir.
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
