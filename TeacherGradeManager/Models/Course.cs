using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherGradeManager.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Day of week is required")]
        public DayOfWeek DayOfWeek { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Course type is required")]
        public CourseType Type { get; set; }
    }

    public enum CourseType
    {
        Lecture,
        Exercise
    }
}
