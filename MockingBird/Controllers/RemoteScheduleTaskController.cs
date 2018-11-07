using Microsoft.Win32.TaskScheduler;
using MockingBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MockingBird.Controllers
{
    public class RemoteScheduleTaskController : Controller
    {
        private ServerDBContext sdb = new ServerDBContext();
        private SchedulledTaskStatusesDBContext tdb = new SchedulledTaskStatusesDBContext();

        // GET: RemoteScheduleTasks
        public ActionResult Index(string ServerName, string ScheduleTask, string TaskToDo)
        {
            List<string> ListOfErrors = new List<string>();
            ViewBag.ErrorList = null;
            ViewBag.SelectedServerName = ServerName;
            IDictionary<double, string> SchedulledTasksStatuses = new Dictionary<double, string>();
            List<string> SchedulledTasks = new List<string>();

            foreach (var TaskStatus in tdb.SchedulledTaskStatus)
            {
                SchedulledTasksStatuses.Add(new KeyValuePair<double, string>(Convert.ToDouble(TaskStatus.Status), TaskStatus.Description));
            }

            if (!string.IsNullOrEmpty(ServerName))
            {
                Task[] allTasks;

                using (TaskService tasksrvc = new TaskService(ServerName))
                {
                    allTasks = tasksrvc.FindAllTasks(new Regex(".*")); // this will list ALL tasks for ALL users

                    foreach (Task tsk in allTasks)
                    {
                        string str = tsk.Folder.ToString();
                        int pos = str.IndexOf(@"Microsoft", 0);
                        if (pos < 0)
                        {
                            SchedulledTasks.Add(tsk.Name);
                        }
                    }
                    SchedulledTasks.Sort();
                }
            }

            ViewBag.ScheduleDetails = null;

            if (!string.IsNullOrEmpty(ScheduleTask))
            {
                //GET ALL SCHEDULLED TASKS HERE AND MODEL IT
                using (TaskService tasksrvc = new TaskService(ServerName))
                {
                    Task[] allTasks;
                    allTasks = tasksrvc.FindAllTasks(new Regex(".*"));

                    foreach (Microsoft.Win32.TaskScheduler.Task tsk in allTasks)
                    {
                        if (tsk.Name == ScheduleTask)
                        {
                            RemoteScheduleTask TaskDetails = new RemoteScheduleTask();

                            TaskDetails.SchedulledTaskName = tsk.Name;
                            TaskDetails.Author = tsk.Definition.RegistrationInfo.Author;
                            TaskDetails.Credentials = tsk.Definition.Principal.UserId;
                            TaskDetails.Disable = Convert.ToBoolean(tsk.IsActive);
                            TaskDetails.LastTaskResult = tsk.LastTaskResult.ToString();
                            TaskDetails.LastRunTime = tsk.LastRunTime.ToString();
                            TaskDetails.NextRunTime = tsk.NextRunTime.ToString();
                            TaskDetails.Status = tsk.State.ToString();

                            ViewBag.ScheduleDetails = TaskDetails;

                            //START, STOP, ENABLE, DISABLED
                            Task task = tasksrvc.FindTask(tsk.Name);
                            switch (TaskToDo)
                            {
                                case "Start":
                                    try
                                    {
                                        task.Run();
                                        task.RegisterChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        //Feed Audit Logs
                                        Console.WriteLine(ex.Message.ToString());
                                    }
                                    break;
                                case "Stop":
                                    task.Stop();
                                    task.RegisterChanges();
                                    break;
                                case "Enable":
                                    task.Definition.Settings.Enabled = true;
                                    task.RegisterChanges();
                                    break;
                                case "Disable":
                                    task.Definition.Settings.Enabled = false;
                                    task.RegisterChanges();
                                    break;
                            }
                        }
                    }
                }
            }

            if (ListOfErrors.Count > 0)
            {
                ViewBag.ErrorList = ListOfErrors;
            }

            ViewBag.TaskStatuses = SchedulledTasksStatuses;
            ViewBag.ServerList = new SelectList(sdb.Servers.OrderBy(x => x.ServerName), "ServerName", "ServerName");
            ViewBag.SchedulledTasks = SchedulledTasks;

            return View();
        }
    }
}