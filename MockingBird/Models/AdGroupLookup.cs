using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class AdGroupLookup
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<string> AdGroupName { get; set; }
    }
}