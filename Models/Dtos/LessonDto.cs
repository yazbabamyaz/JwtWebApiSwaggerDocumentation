using System.ComponentModel.DataAnnotations;

namespace LessonApi.Models.Dtos
{
    public class LessonDto
    {
        public int Id { get; set; }

        //Fluent Validation kütüphanesi de kullanılabilirdi.
        [Required(ErrorMessage = "Must to type LessonName")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Only letters are allowed")]
        public string LessonName { get; set; }
       
       
        public decimal LessonPrice { get; set; }//derssaatücreti
        public string Description { get; set; }
    }
}
