using System.ComponentModel.DataAnnotations;

namespace OnlineRegistration.ViewModels
{
    public class UserCourseViewModel
    {
        [Key]
        public string UserID { get; set; }

        [Key]
        public int CourseID { get; set; }

        [Display(Name ="Клиент")]
        public string UserFullName { get; set; }

        [Display(Name ="Курс")]
        public string CourseName { get; set; }
    }
}