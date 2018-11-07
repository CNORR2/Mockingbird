using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class AdUserGroupLookup
    {
        public string AdGroupName { get; set; }
        public List<string> UserNames { get; set; }
    }
}