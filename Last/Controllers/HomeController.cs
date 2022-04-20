using Last.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Last.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            
            return View(db.Categories.ToList());
        }
        public ActionResult Details(int JobId)
        {
            var job = db.jobs.Find(JobId);
            if(job == null)
            {
                return HttpNotFound();
            }
            else
            {
                Session["JobId"] = JobId;
                return View(job);
            }
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (check.Count < 1) {
                var job = new ApplyForJob();

                job.Message = Message;
                job.JobId = JobId;
                job.UserId = UserId;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result="تمت الاضافة بنجاح";

            }
            else
            {
                ViewBag.Result="معذرة سبق وتقدمت لهذه الوظيفة";
            }
         
            return View();
        }
        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(jobs.ToList());
        }
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if(job == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(job);
            }
        }
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(job);
            }
        }
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);

        }
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(job);
            }
        }
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            var job1 = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(job1);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");
        }
        //لعرض المتقدمين لوظيفة معينه
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var userid = User.Identity.GetUserId();
            var jobs = from app in db.ApplyForJobs
                       join job in db.jobs
                       on
                       app.JobId equals job.Id
                       where job.User.Id == userid
                       select app;
            return View(jobs.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}