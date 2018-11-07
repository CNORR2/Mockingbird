using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class DiskChecker
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Server Name")]
        public string ServerName { get; set; }
        [Required]
        [Display(Name = "Drive Letter")]
        public string DriveLetter { get; set; }
        [Required]
        [Display(Name = "Low Disk Percentage")]
        public int LowDiskPercentage { get; set; }
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
        [Display(Name = "Last Run")]
        public DateTime LastRan { get; set; }
        [Display(Name = "Drive Size")]
        public string DriveSize { get; set; }
        [Display(Name = "Available Space")]
        public string AvailableSpace { get; set; }
        [Display(Name = "Ignore Poll Times on Next Run")]
        public bool IgnorePollTimeOnNextRun { get; set; }
    }

    public class DriveSpaceDBContext : DbContext
    {
        public DbSet<DiskChecker> DiskCheckers { get; set; }
    }
}