namespace MockingBirdService
{
    using System.ServiceProcess;
    using System.Threading;
    public partial class MockingBirdServices : ServiceBase
    {
        //private Thread DiskSpaceCheckThread = new Thread(new ThreadStart(DiskSpaceCheckServices.DiskSpaceCheckServices.DiskSpaceService));
        //private Thread ScheduledTaskCheckThread; //= new Thread(new ThreadStart(""))
        //private Thread ServiceCheckThread; //= new Thread(new ThreadStart(""))

        public MockingBirdServices()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadStart DiskSpaceCheckThreadStart = new ThreadStart(DiskSpaceCheckServices.DiskSpaceCheckServices.DiskSpaceService);

            Thread DiskSpaceCheckThread = new Thread(DiskSpaceCheckThreadStart);


            //Start all services on threads
            DiskSpaceCheckThread.Start();
            //ScheduledTaskCheckThread.Start();
            //ServiceCheckThread.Start();
        }

        protected override void OnStop()
        {
            DiskSpaceCheckServices.DiskSpaceCheckServices.DiskCheckerServiceEnabled = false;
            //DiskSpaceCheckThread.Abort();
        }
    }
}
