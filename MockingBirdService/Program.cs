using System.ServiceProcess;

namespace MockingBirdService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MockingBirdServices()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
