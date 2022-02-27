using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackerWebAPI.View_Models
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
    public class TaskVM : BaseVM
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
        //public ProjectVM Project { get; set; }
    }
}
