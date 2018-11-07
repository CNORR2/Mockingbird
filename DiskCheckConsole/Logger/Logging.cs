namespace DiskCheckConsole.Logger
{
    using System;
    using System.Configuration;
    using System.IO;

    public class LogWriter
    {
        public static void LogWrite(string LogMessage, string LogType)
        {
            string logDirectory = string.Empty;
            logDirectory = @Convert.ToString(ConfigurationManager.AppSettings["LogDirectory"]);
            string todaysDate = DateTime.Now.ToString("MMddyyyy");
            string logFilePath = logDirectory + "\\" + LogType + "_" + todaysDate + ".txt";

            using (StreamWriter w = File.AppendText(logFilePath))
            {
                try
                {

                    Log(LogMessage, w);
                }
                catch (Exception ex)
                {
                    //Create new text file
                    if (File.Exists(logFilePath))
                    {
                        using (var tw = new StreamWriter(logFilePath, true))
                        {
                            Log(LogMessage, w);
                        }
                    }
                }
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
