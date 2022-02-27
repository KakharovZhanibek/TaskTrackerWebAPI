using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackerWebAPI.DAL.Abstract
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
