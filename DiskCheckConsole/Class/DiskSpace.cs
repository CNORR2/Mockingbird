using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskCheckConsole.Class
{
    public class DiskSpace
    {
        //Gigabytes (GB)
        public double TotalSpace { get; set; }
        public double RemainingSpace { get; set; }
        public double PercentageOfSpaceRemaining { get; set; }
    }

    public class EmailTemplate
    {
        public static string EmailBody(string ServerName, string DriveLetter, double DiskPercentage, double RemainingSpace, double DriveSize)
        {
            return "The Disk Drive '" + 
                    DriveLetter +
                    "' on the Server " +
                    ServerName +
                    " is below " +
                    "%" + DiskPercentage +
                    "r/n/r/n Remaing Space:" +
                    RemainingSpace +
                    "gb/"
                    + DriveSize
                    + "gb";
        }

        public static string EmailSubject(string ServerName, string DriveLetter)
        {
            return "Disk Drive Alert (" + ServerName + "/" + DriveLetter + ")";
        }
    }
}
