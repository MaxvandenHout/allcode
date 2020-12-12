using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Managment.Models;

namespace Managment.Controllers
{
    public class jobsController : Controller
    {
        private managmentEntities3 db = new managmentEntities3();

        // GET: jobs
        public ActionResult Index(string filter)
        {

            var model = db.jobs.ToList();
            if (filter != null)
            {
                switch (filter)
                {
                    case "All":
                        model = db.jobs.ToList();
                        break;
                    case "Open":
                        model = db.jobs.Where(j => j.acceptancetime == null).ToList();
                        break;
                    case "Busy":
                        model = db.jobs.Where(j => j.acceptancetime != null && j.finishtime == null).ToList();
                        break;
                    case "Finished":
                        model = db.jobs.Where(j => j.finishtime != null).ToList();
                        break;
               
                }
            }

            ViewBag.filter = filter;
            if (filter == null)
            {
                filter = "All";
            }
            return View(model);
        }

        // GET: jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // GET: jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Jobid,Jobname,Jobdiscription,Status,Jobtaker,Begintime,Endtime,acceptancetime,finishtime,comment")] jobs jobs)
        {
            if (ModelState.IsValid)
            {
                db.jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobs);
        }

        // GET: jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Jobid,Jobname,Jobdiscription,Status,Jobtaker,Begintime,Endtime,acceptancetime,finishtime,comment")] jobs jobs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobs);
        }

        // GET: jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jobs jobs = db.jobs.Find(id);
            db.jobs.Remove(jobs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Todo() {

            var model = db.jobs.Where(j => j.finishtime == null);
            return View("Todo", model);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
