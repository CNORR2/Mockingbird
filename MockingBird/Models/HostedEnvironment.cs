using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MockingBird.Models
{
    public class HostedEnvironment
    {
        [Key]
        public int ID { get; set; }
        public string EnvironmentName { get; set; }
    }

    public class EnvironmentDBContext : DbContext
    {
        public DbSet<HostedEnvironment> Environments { get; set; }
    }
}