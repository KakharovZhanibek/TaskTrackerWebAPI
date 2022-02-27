using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackerWebAPI.DAL.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
