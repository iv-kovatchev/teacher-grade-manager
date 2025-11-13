using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherGradeManager.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Student ID must be greater than 0")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Course ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Course ID must be greater than 0")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Grade value is required")]
        [Range(2.00, 6.00, ErrorMessage = "Grade must be between 2.00 and 6.00")]
        public decimal GradeValue { get; set; }
    }
}
