using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class SchedulledTasks
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Scheduled Task Name")]
        public string SchedulledTaskName { get; set; }
        [Required]
        [Display(Name = "Server Name")]
        public string ServerName { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Last Task Result")]
        public string LastTaskResult { get; set; }
        [Display(Name = "Last Run Time")]
        public string LastRunTime { get; set; }
        [Display(Name = "Next Run Time")]
        public string NextRunTime { get; set; }
        [Required]
        [Display(Name = "Minutes Between Email Alerts")]
        public int PollTime { get; set; }
        [Display(Name = "Admin Email Alert")]
        public bool AdminEmailAlert { get; set; }
        [Display(Name = "Alert Email Subscribers")]
        public bool AlertSubscribers { get; set; }
        [Display(Name = "Subscription Email Addresses")]
        public string SubcriptionEmailAddresses { get; set; }
        [Display(Name = "Disable Action")]
        public bool Disable { get; set; }
        [Display(Name = "Show on Dash")]
        public bool ShowOnDash { get; set; }
        public bool IgnorePollTimeOnNextRun { get; set; }
    }

    public class SchedulledTasksDBContext : DbContext
    {
        public DbSet<SchedulledTasks> SchedulledTasks { get; set; }
    }
}