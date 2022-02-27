using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackerWebAPI.View_Models
{
    public enum ProjectStatus
    {
        NotStarted,
        Active,
        Completed
    }
    public class ProjectVM : BaseVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        [Required]
        public ProjectStatus Status { get; set; }
        public int? Priority { get; set; }

#nullable enable
        public List<TaskVM>? Tasks { get; set; }
    }
}
