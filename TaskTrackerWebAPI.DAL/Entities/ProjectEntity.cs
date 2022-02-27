using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTrackerWebAPI.DAL.Abstract;

namespace TaskTrackerWebAPI.DAL.Entities
{
    public enum ProjectStatus
    {
        NotStarted,
        Active,
        Completed
    }
    public class ProjectEntity : BaseEntity
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
        public virtual List<TaskEntity>? Tasks { get; set; }
    }
}
