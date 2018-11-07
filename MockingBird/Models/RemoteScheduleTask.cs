using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class RemoteScheduleTask
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Scheduled Task Name")]
        public string SchedulledTaskName { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Last Task Result")]
        public string LastTaskResult { get; set; }
        [Display(Name = "Last Run Time")]
        public string LastRunTime { get; set; }
        [Display(Name = "Next Run Time")]
        public string NextRunTime { get; set; }
        [Display(Name = "Credentials")]
        public string Credentials { get; set; }
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Display(Name = "Disable Action")]
        public bool Disable { get; set; }
    }
}