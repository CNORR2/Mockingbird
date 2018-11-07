using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public enum Permissions
    {
        FullAccessAdministrator,
        Administrator,
        MonitoringUser
    };

    public class Users
    {
        public int ID { get; set; }
        public string WindowsIdentity { get; set; }
        public Permissions Permissions {get; set;}
        public string EmailAddress { get; set; }
        public bool CriticalEmailSubscriber { get; set; }
        public bool LockUser { get; set; }
    }
}