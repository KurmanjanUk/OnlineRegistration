using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRegistration.Models
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Display(Name ="Название курса")]
        public string CourseName { get; set; }

        [Display(Name = "Ментор")]
        public string Mentor { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }

        [Display(Name = "Дата начала")]
        [DataType(DataType.Date)]
        public DateTime StartDate {get; set; }

        public byte[] FileContent { get; set; }
        
        public string FileName { get; set; }
    }


}