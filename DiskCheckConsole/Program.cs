using MockingBird.Models;
using System;
using System.Linq;
using System.Management;
using DiskCheckConsole.Class;
using DiskCheckConsole.Emails;
using DiskCheckConsole.Logger;
using System.Threading;
using MockingBirdService.DiskSpaceCheckServices;

namespace DiskCheckConsole
{
    class Program
    {
        private static bool DiskSuccess;
        const string DATE_FORMAT = "yyyy-MM-dd h:mm:ss tt";
        public static string DateTimeNow = DateTime.Now.ToString(DATE_FORMAT);
        public static bool ServiceStarted;
        //private static Thread DiskSpaceCheckThread = new Thread(new ThreadStart(DiskSpaceCheckServices.DiskSpaceService));
        private static Thread Worker;
        private static AutoResetEvent StopRequest = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            ServiceStarted = true;

            Worker = new Thread(DiskSpaceCheckServices.DiskSpaceService);
            Worker.Start();

            



            //ServiceStarted = true;

            //ThreadStart DiskSpaceCheckThreadStart = new ThreadStart(DiskSpaceCheckServices.DiskSpaceService);
            //Thread DiskSpaceCheckThread = new Thread(DiskSpaceCheckThreadStart);


            ////Start all services on threads
            //while (ServiceStarted)
            //{
            //    if (DiskSpaceCheckThread == null && DiskSpaceCheckThread.IsAlive)
            //    {
            //        DiskSpaceCheckThread.Start();
            //    }
            //}

            //ScheduledTaskCheckThread.Start();
            //ServiceCheckThread.Start();



            //DiskSpaceCheckThread.Start();



            //LogWriter.LogWrite(InitialisationStatus.ProgramStarted.ToString(), LogTypes.DiskCheck.ToString());
            //bool DiskCheckSuccess = DiskSpaceCheck();

            //if (DiskCheckSuccess)
            //{
            //    LogWriter.LogWrite(InitialisationStatus.ProgramCompleted.ToString(), LogTypes.DiskCheck.ToString());
            //}
            //{
            //    LogWriter.LogWrite(InitialisationStatus.ProgramFailure.ToString(), LogTypes.DiskCheck.ToString());
            //}
        }

        public static bool DiskSpaceCheck()
        {
            DiskSuccess = false;

            try
            {
                using (var context = new DriveSpaceDBContext())
                {
                    var DiskRecords = context.DiskCheckers.ToList();

                    foreach (var i in DiskRecords)
                    {
                        if (!i.Disable)
                        {
                            DiskSpace availableSpace = GetDriveSpace(i.ServerName, i.DriveLetter);
                            
                            //Update DB
                            var Update = context.DiskCheckers.Find(i.ID);
                            Update.AvailableSpace = availableSpace.RemainingSpace.ToString();
                            Update.DriveSize = availableSpace.TotalSpace.ToString();
                            Update.LastRan = Convert.ToDateTime(DateTimeNow);
                            //Update.LowDiskPercentage = Convert.ToInt32(availableSpace.PercentageOfSpaceRemaining);

                            // Mark as Changed
                            context.Entry(Update).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();

                            if (i.AdminEmailAlert == true || i.AlertSubscribers == true)
                            {
                                //If emails are needed, check to see if any alerts need to be sent
                                if (Convert.ToDateTime(i.LastRan).AddMinutes(i.PollTime) <= Convert.ToDateTime(DateTimeNow))
                                {
                                    int LowPercentageAlert = Convert.ToInt32(i.LowDiskPercentage);
                                    int CurrentPercentage = Convert.ToInt32(availableSpace.PercentageOfSpaceRemaining);

                                    //Is the disk in a low disk state
                                    if (LowPercentageAlert > CurrentPercentage)
                                    {
                                        //Send Email
                                        string AllEmailAddresses = string.Empty;
                                        //Get Addresses
                                        if (i.AdminEmailAlert)
                                        {
                                            AllEmailAddresses = "InsertConfigAdminEmail@Something.com";
                                        }

                                        if (i.AlertSubscribers = true && AllEmailAddresses == string.Empty)
                                        {
                                            AllEmailAddresses = AllEmailAddresses + ", " + i.SubcriptionEmailAddresses;
                                        }
                                        else if (i.AlertSubscribers)
                                        {
                                            AllEmailAddresses = i.SubcriptionEmailAddresses;
                                        }
                                        else
                                        {
                                            //log
                                        }


                                        string EmailSubject = EmailTemplate.EmailSubject(i.ServerName, i.DriveLetter);
                                        string HtmlBody = EmailTemplate.EmailBody(i.ServerName, i.DriveLetter, availableSpace.PercentageOfSpaceRemaining, availableSpace.RemainingSpace, availableSpace.TotalSpace);
                                        bool EmailSuccess = EmailAlets.SendEmail(HtmlBody, EmailSubject, AllEmailAddresses);
                                        //Log Email Success
                                    }
                                    else
                                    {
                                        //
                                    }
                                }
                            }
                        }
                    }
                }
                DiskSuccess = true;
            }
            catch
            {
            }

            return DiskSuccess;
        }

        public static DiskSpace GetDriveSpace(string ServerName, string DriveLetter)
        {
            DiskSpace DriveDetails = new DiskSpace();

            try
            {
                ConnectionOptions oConn = new ConnectionOptions();
                string strNameSpace = @"\\";

                if (ServerName != "")
                    strNameSpace += ServerName;
                else
                    strNameSpace += ".";

                strNameSpace += @"\root\cimv2";
                ManagementScope oMs = new ManagementScope(strNameSpace, oConn);

                //get Fixed disk state
                ObjectQuery oQuery = new ObjectQuery("select FreeSpace,Size,Name from Win32_LogicalDisk where DriveType=3");
                ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
                ManagementObjectCollection oReturnCollection = oSearcher.Get();

                //loop through found drives and write out info
                double D_Freespace = 0;
                double D_Totalspace = 0;
                foreach (ManagementObject oReturn in oReturnCollection)
                {
                    // Free Space in bytes
                    string strFreespace = oReturn["FreeSpace"].ToString();
                    D_Freespace = Math.Round(D_Freespace + System.Convert.ToDouble(strFreespace) / 1024 / 1024 / 1024, 2);

                    string strTotalspace = oReturn["Size"].ToString();
                    D_Totalspace = Math.Round(D_Totalspace + System.Convert.ToDouble(strTotalspace) / 1024 / 1024 / 1024, 2);

                    string driveReturned = oReturn["Name"].ToString().Substring(0, 1);

                    if (driveReturned == DriveLetter)
                    {
                        DriveDetails.TotalSpace = D_Totalspace;
                        DriveDetails.RemainingSpace = D_Freespace;
                        DriveDetails.PercentageOfSpaceRemaining = 
                            Math.Round(Convert.ToDouble(DriveDetails.RemainingSpace) / Convert.ToDouble(DriveDetails.TotalSpace) * 100, 0);
                    }
                }
            }
            catch
            {
                //MessageBox.Show("Failed to obtain Server Information. The node you are trying to scan can be a Filer or a node which you don't have administrative priviledges. Please use the UNC convention to scan the shared folder in the server", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return DriveDetails;
        }
    }
}
