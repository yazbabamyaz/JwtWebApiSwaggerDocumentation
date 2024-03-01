namespace LessonApi.Infrastructure.Repositories
{
    public interface IGenericRepository<T>
    {
        ICollection<T> GetAll();
        Task<T> GetByIdAsync(int id);      
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> SaveAsync();
        


    }
}
