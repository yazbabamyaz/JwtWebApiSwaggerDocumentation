using System.ComponentModel.DataAnnotations;

namespace LessonApi.Models.Entities
{
    public abstract class BaseEntity
    {       
        public virtual int Id { get; set; }
        //OOP=>Encapsulation
        public DateTime CreateDate { get=>_createDate; set=>_createDate=value; }
        DateTime _createDate = DateTime.Now;
        
    }
    
}
