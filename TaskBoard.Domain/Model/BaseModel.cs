using System;
using System.Collections.Generic;
using System.Text;

namespace TaskBoard.Domain.Model
{
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
