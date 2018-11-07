using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class ApplicationConfiguration
    {
        public string MailHost { get; set; }
        public int MailPort { get; set; }
        public string AdminSubscriptionEmailAddress { get; set; }
        public string MockingbirdEmailAddress { get; set; }
    }
}