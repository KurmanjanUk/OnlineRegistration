using Microsoft.AspNet.Identity.EntityFramework;
using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineRegistration.Controllers
{
    public class UserController : Controller
    {
        private readonly CourseContext db = new CourseContext();

        // GET: User
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

      
    }
}
