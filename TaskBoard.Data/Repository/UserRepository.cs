using System;
using System.Collections.Generic;
using System.Text;
using TaskBoard.Data.Context;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Data.Repository
{    
    public class UserRepository : BaseRepository<User>, IUserRepository
    {       
        public UserRepository(TaskBoardDbContext context) : base(context)
        {            
        }        

        public Task<List<User>> GetUsersByIdsAsync(List<Guid> userIds)
        {
            throw new NotImplementedException();
        }

      
    }
}
