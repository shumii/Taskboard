using TaskBoard.Domain.Model;

namespace TaskBoard.Domain.Repository;

public interface IUserRepository : IRepository<User>
{    
    Task<List<User>> GetUsersByIdsAsync(List<Guid> userIds);  
}