using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class SchedulledTaskStatuses
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Schedulled Task Status")]
        public string Status { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class SchedulledTaskStatusesDBContext : DbContext
    {
        public DbSet<SchedulledTaskStatuses> SchedulledTaskStatus { get; set; }

        public System.Data.Entity.DbSet<MockingBird.Models.SchedulledTasks> SchedulledTasks { get; set; }
    }
}