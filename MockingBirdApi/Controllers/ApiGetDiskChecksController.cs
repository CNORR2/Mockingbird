using MockingBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MockingBirdApi.Controllers
{
    public class ApiGetDiskChecksController : ApiController
    {
        public DriveSpaceDBContext db = new DriveSpaceDBContext();

        public List<DiskChecker> DiskCheckList()
        {
            using (var db = new DriveSpaceDBContext())
            {
                // Display all Blogs from the database
                var diskList = from d in db.DiskCheckers
                               orderby d.ServerName
                               select d;

                List<DiskChecker> DiskList = diskList.ToList();

                return DiskList;
            }
        }


        // GET: api/ApiGetDiskChecks
        public IEnumerable<DiskChecker> Get()
        {
            List<DiskChecker> DiskList = DiskCheckList();

            return DiskList;

            //return new string[] { "value", "value1" };
        }

        // GET: api/ApiGetDiskChecks/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiGetDiskChecks
        public void Post([FromBody]string value)
        {
            //Create and save a new Blog
            //    Console.Write("Enter a name for a new Blog: ");
            //var name = Console.ReadLine();

            //var blog = new Blog { Name = name };
            //db.Blogs.Add(blog);
            //db.SaveChanges();
        }

        // PUT: api/ApiGetDiskChecks/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiGetDiskChecks/5
        public void Delete(int id)
        {
        }
    }
}
