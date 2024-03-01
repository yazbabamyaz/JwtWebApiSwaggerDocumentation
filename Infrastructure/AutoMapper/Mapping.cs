using AutoMapper;
using LessonApi.Models.Dtos;
using LessonApi.Models.Entities;

namespace LessonApi.Infrastructure.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping() 
        {
            CreateMap<Lesson, LessonDto>().ReverseMap();
        }
    }
}
