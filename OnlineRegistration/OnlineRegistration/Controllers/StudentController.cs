using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OnlineRegistration.Models;
using OnlineRegistration.ViewModels;

namespace OnlineRegistration.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private CourseContext db = new CourseContext();

        // GET: Student
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var user = db.Users.First(u => u.UserName == username);
            var userCourses = (from uc in db.UserCourses
                               join course in db.Courses on uc.CourseID equals course.CourseID
                               where uc.UserID == user.Id
                               select course).ToList();
            return View(userCourses);
        }

        public ActionResult GetCourses()
        {
            var userName = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.UserName == userName);
            var courses = db.Courses.Select(c => new CoursesViewModel { Course = c }).ToList();
            var userCourses = db.UserCourses.Where(uc => uc.UserID == user.Id).ToList();
            foreach (var uc in userCourses)
            {
                var userCourse = courses.FirstOrDefault(c => c.Course.CourseID == uc.CourseID);
                if (userCourse != null)
                {
                    userCourse.IsSelected = true;
                }
            }

            return View(courses);
        }


        [HttpPost]
        public ActionResult RegisterStudent(int courseID)
        {
            var userName = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                throw new Exception("Студент не найден");
            }

            var userCourse = db.UserCourses.FirstOrDefault(c => c.CourseID == courseID && c.UserID == user.Id);

            if (userCourse != null)
            {
                throw new Exception("Данный курс уже выбран");
            }

            db.UserCourses.Add(new UserCourse
            {
                CourseID = courseID,
                UserID = user.Id
            });

            db.SaveChanges();

            return RedirectToAction("GetCourses");
        }

        [HttpPost]
        public ActionResult CancelCourse(int courseID)
        {
            var userName = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                throw new Exception("Студент не найден");
            }

            var userCourse = db.UserCourses.FirstOrDefault(uc => uc.UserID == user.Id && uc.CourseID == courseID);
            if (userCourse == null)
            {
                ModelState.AddModelError("", $"Student with id {user.Id}, does not have course with id: {courseID}");
            }

            db.UserCourses.Remove(userCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
