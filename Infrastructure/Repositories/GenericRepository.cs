using LessonApi.Infrastructure.Context;
using LessonApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LessonApi.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return SaveAsync();
        }

       

        public Task<bool> DeleteAsync(int id)
        {
           var value= _context.Set<T>().Find(id);
            if (value != null) _context.Remove(value);       
            return SaveAsync();
        }

        public ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var value =  _context.Set<T>().FirstOrDefault(x => x.Id == id);
            return   value;
        }   

        public   async Task<bool> SaveAsync()=>await _context.SaveChangesAsync() >= 0 ? true : false;
        


        public Task<bool> UpdateAsync(T entity)
        {
            entity.CreateDate = DateTime.Now;            
            _context.Set<T>().Update(entity);
            return SaveAsync();            
        }
    }
}
