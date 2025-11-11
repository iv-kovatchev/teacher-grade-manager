using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherGradeManager.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan Time { get; set; }

        public CourseType Type { get; set; }
    }

    public enum CourseType
    {
        Lecture,
        Exercise
    }
}
