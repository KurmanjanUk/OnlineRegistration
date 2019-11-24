using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRegistration.Models
{
    [Table("UserCourses")]
    public class UserCourse
    {
        [Key, Column(Order = 0)]
        public string UserID { get; set; }
       
        [Key, Column(Order = 1)]
        public int CourseID { get; set; }
    }
}