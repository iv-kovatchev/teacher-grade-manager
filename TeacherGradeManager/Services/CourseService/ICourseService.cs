using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;

namespace TeacherGradeManager.Services.CourseService
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();

        Course GetCourseById(int id);

        void AddCourse(Course course);

        void UpdateCourse(Course course);

        void DeleteCourse(int courseId);
    }
}
