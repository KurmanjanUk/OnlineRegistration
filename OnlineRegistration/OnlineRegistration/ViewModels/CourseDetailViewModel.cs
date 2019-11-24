using OnlineRegistration.Models;
using System.Collections.Generic;

namespace OnlineRegistration.ViewModels
{
    public class CourseDetailViewModel
    {
        public Course Course{ get; set; }
        public List<ApplicationUser> Users { get; set; }

    }
}