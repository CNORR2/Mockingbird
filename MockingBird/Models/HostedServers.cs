using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockingBird.Models
{
    public class HostedServers
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string ServerName { get; set; }
        //public string AvailableDrives { get; set; }
        [Required]
        public string Environment { get; set; }
        public string Comments { get; set; }
    }


    public class ServerDBContext : DbContext
    {
        public DbSet<HostedServers> Servers { get; set; }
    }
}