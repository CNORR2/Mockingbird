using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockingBird.Models
{
    public class ViewModels
    {

        public IEnumerable<HostedEnvironment> Environments { get; set; }
        public IEnumerable<HostedServers> Servers { get; set; }
    }
}