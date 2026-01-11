namespace TaskBoard.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task UpsertAsync(T entity);        
        Task AddRangeAsync(IEnumerable<T> entities);        
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
