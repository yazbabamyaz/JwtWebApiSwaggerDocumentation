using AutoMapper;
using LessonApi.Infrastructure.Repositories;
using LessonApi.Models.Dtos;
using LessonApi.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LessonApi.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository _repo;
        private readonly IMapper _mapper;

       
        public LessonsController(ILessonRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        ///<summary>
        ///Bütün lesson nesnelerini getirir.
        /// </summary>
        /// <returns> </returns>
        [HttpGet]        
        //[Authorize(Policy = "RequireAdminRole")]//Daha karmaşık yetkilendirme için Policy kullanılabilir.
        [Authorize(Roles ="Admin")]//Yetki için Token da Role bilgisi Admin olmalı
        [ProducesResponseType(200, Type = typeof(ICollection<LessonDto>))]
        public IActionResult GetAll() 
        { 
            var value=_repo.GetAll();
            var lessonDto=_mapper.Map<List<LessonDto>>(value);
            return Ok(lessonDto);
        }


        /// <summary>
        /// Yeni lesson kaydeder.
        /// </summary>       
        /// <returns></returns>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLesson(LessonDto lessonDto)
        {
            if (lessonDto == null) return BadRequest(ModelState);

            //bu ders var mı yok mu
            if (await _repo.AnyAsync(lessonDto.LessonName)) 
            {
                ModelState.AddModelError("", "This lesson already exists");
                return StatusCode(404, ModelState);
            }
            

            var lesson=_mapper.Map<Lesson>(lessonDto);
            if(!await _repo.AddAsync(lesson))//Ekleme işlemi başarısız ise
            {
                ModelState.AddModelError("", $"An error occurred while creating a lesson {lesson.LessonName} or {lesson.Description}");
                return StatusCode(500, ModelState);
            }
            return  Ok(_mapper.Map<LessonDto>(lesson));
        }


        /// <summary>
        /// id bilgisine göre lesson getir
        /// </summary>
        /// <param name="id">lesson id bilgisi</param>
        /// <returns></returns>
        /// <response code="404">id'ye ait kaynak bulunamazsa</response> 
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]//Yetki için User role bilgisi gerekli
        [ProducesResponseType(200), ProducesResponseType(404)]
        public async Task<IActionResult> GetLesson(int id) 
        { 
           var value= await _repo.GetByIdAsync(id);
            if (value is null) return NotFound();
            var lessonDto=_mapper.Map<LessonDto>(value);
            return Ok(value);
        }

        /// <summary>
        /// id'ye göre Lesson sil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(policy: "RequireClaim")]//policy'e göre yetkilendirme.
        //Tokenda userName claim'i olacak ve değeri mustafa olacak.Tanımlama Program.cs de mevcut.
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var value=await _repo.GetByIdAsync(id);
            if (value is null) return NotFound();
            if(! await _repo.DeleteAsync(value.Id))
            {
                ModelState.AddModelError("", $"An error occurred while deleting the lesson {value.LessonName}");
            }
            return NoContent();//204
        }

        /// <summary>
        /// Lesson güncelle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lessonDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, [FromQuery] LessonDto lessonDto)
        {
            if (lessonDto == null || lessonDto.Id != id)
                return BadRequest(ModelState);

            Lesson value = _mapper.Map<Lesson>(lessonDto);

            if (!await _repo.UpdateAsync(value))
            {
                ModelState.AddModelError("", $"An error occurred while updating the lesson {value.LessonName}");
                return StatusCode(500, ModelState);
            }

            return Ok(value);
        }
    }
}
