using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class RemoteServices
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Display(Name = "Startup Type")]
        public string StartupType { get; set; }

        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }
        
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}