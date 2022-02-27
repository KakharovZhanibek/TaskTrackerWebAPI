using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTrackerWebAPI.DAL.Abstract;

namespace TaskTrackerWebAPI.DAL.Entities
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
    public class TaskEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public TaskStatus Status { get; set; }
        [Required]
        public int Priority { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
