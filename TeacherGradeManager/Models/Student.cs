using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherGradeManager.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Faculty number is required")]
        [RegularExpression(@"^F\d{1,6}$", ErrorMessage = "Faculty number must start with 'F' followed by 1-6 digits")]
        public string FacultyNumber { get; set; }
    }
}
