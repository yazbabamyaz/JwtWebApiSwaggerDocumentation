namespace LessonApi.Models.Entities
{
    public class Lesson:BaseEntity
    {
        public string LessonName { get; set; }
        public decimal LessonPrice { get; set;}//derssaatücreti
        public string Description { get; set; }
    }
}
