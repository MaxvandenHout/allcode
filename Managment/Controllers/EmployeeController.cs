using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Managment.Models;

namespace Managment.Controllers
{
    public class EmployeeController : Controller
    {
        private managmentEntities3 db = new managmentEntities3();
        // GET: Employee
        public ActionResult Index()
        {
            int id = Int32.Parse((string)Session["UserID"]);

            var jobs = db.jobs
                .Join(
                    db.Jobtakers,
                    j => j.Jobid,
                    jt => jt.Jobid,
                    (j, jt) => new { Job = j, Jobtaker = jt }
                )
                .Where(jjt => jjt.Jobtaker.Jobtakerid == id)
                .Select(jjt => jjt.Job).ToList();
            return View(jobs);
        }

        public ActionResult Jobstartview(int? jobid)
        {
            
            int id = Int32.Parse((string)Session["UserID"]);
            var match = db.Jobtakers.First(f => f.Jobid == jobid && f.Jobtakerid == id);
            var job = db.jobs.First(q => q.Jobid == match.Jobid);
            if (job != null)
            {
                return View(job);
            }
            return RedirectToAction("Index", "Login");
           
        }

        public void Jobstart(int? jobid) 
        {
            int id = Int32.Parse((string)Session["UserID"]);
            var match = db.Jobtakers.First(f => f.Jobid == jobid && f.Jobtakerid == id);
            var job = db.jobs.First(q => q.Jobid == match.Jobid);
            if (job != null)
            {
                job.Jobtaker = id;
                job.acceptancetime = DateTime.Now;
                db.SaveChanges();
            }
            RedirectToAction("Index", "Login");
        }

        public void JobFinish(int? jobid)
        {
            int id = Int32.Parse((string)Session["UserID"]);
            var match = db.Jobtakers.First(f => f.Jobid == jobid && f.Jobtakerid == id);
            var job = db.jobs.First(q => q.Jobid == match.Jobid);
            if (job != null)
            {
                //comment
                job.finishtime = DateTime.Now;
                db.SaveChanges();
            }
            RedirectToAction("Index", "Login");
        }
    }
}