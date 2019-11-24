using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineRegistration.Models;
using OnlineRegistration.ViewModels;

namespace OnlineRegistration.Controllers
{
    public class UserCoursesController : Controller
    {
        private CourseContext db = new CourseContext();

        // GET: UserCourse
        public ActionResult Index()
        {
            var userCourses = (from uc in db.UserCourses
                               join u in db.Users on uc.UserID equals u.Id
                               join c in db.Courses on uc.CourseID equals c.CourseID
                               select new UserCourseViewModel
                               {
                                   CourseID = c.CourseID,
                                   UserID = u.Id,
                                   UserFullName = u.FullName,
                                   CourseName = c.CourseName
                               });
            return View(userCourses);
        }

       
        // GET: UserCourses/Create
        public ActionResult Create()
        {
            var users = db.Users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.FullName
            }).ToList();

            var courses = db.Courses.Select(c => new SelectListItem
            {
                Value = c.CourseID.ToString(),
                Text = c.CourseName
            }).ToList();

            ViewBag.Users = users;
            ViewBag.Courses = courses;

            return View();
        }

        // POST: UserCourses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,CourseID")] UserCourse userCourse)
        {
            if (ModelState.IsValid)
            {
                db.UserCourses.Add(userCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userCourse);
        }

        
        // GET: UserCourses/Edit/5
        public ActionResult Edit(string UserID, int CourseID)
        {
            var users = db.Users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.FullName
            }).ToList();

            var courses = db.Courses.Select(c => new SelectListItem
            {
                Value = c.CourseID.ToString(),
                Text = c.CourseName
            }).ToList();

            ViewBag.Users = users;
            ViewBag.Courses = courses;

            var userCourse = db.UserCourses.FirstOrDefault(uc =>
                uc.UserID == UserID && uc.CourseID == CourseID);
            return View(userCourse);
        }

        // POST: UserCourses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int previousCourseID, UserCourse userCourse)
        {
            if (ModelState.IsValid)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    var prevCourse = db.UserCourses.FirstOrDefault(uc =>
                        uc.UserID == userCourse.UserID && uc.CourseID == previousCourseID);
                    db.UserCourses.Remove(prevCourse);
                    db.SaveChanges();
                    db.UserCourses.Add(userCourse);
                    db.SaveChanges();
                    trans.Commit();
                    return RedirectToAction("Index");
                }
            }
            return View(userCourse);
        }

        // GET: UserCourses/Delete/5
        public ActionResult Delete(string UserID, int CourseID)
        {
            var userCourse = (from uc in db.UserCourses
                              join u in db.Users on uc.UserID equals u.Id
                              join c in db.Courses on uc.CourseID equals c.CourseID
                              where uc.UserID == UserID && uc.CourseID == CourseID
                              select new UserCourseViewModel
                              {
                                  CourseID = c.CourseID,
                                  UserID = u.Id,
                                  UserFullName = u.FullName,
                                  CourseName = c.CourseName
                              }).FirstOrDefault();

            if (userCourse == null)
            {
                return HttpNotFound();
            }

            return View(userCourse);
        }

        // POST: UserCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string UserID, int CourseID)
        {
            UserCourse userCourse = db.UserCourses.FirstOrDefault(uc =>
                uc.UserID == UserID && uc.CourseID == CourseID);

            db.UserCourses.Remove(userCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
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
