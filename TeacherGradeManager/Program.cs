using System.ComponentModel.DataAnnotations;
using TeacherGradeManager.Forms;
using TeacherGradeManager.Models;
using TeacherGradeManager.Repositories;
using TeacherGradeManager.Services.CourseService;
using TeacherGradeManager.Services.GradeService;
using TeacherGradeManager.Services.StudentService;

namespace TeacherGradeManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Setup database and services
            var studentRepository = new StudentRepository("teachers_management.db");
            var studentService = new StudentService(studentRepository);

            var courseRepository = new CourseRepository("teachers_management.db");
            var courseService = new CourseService(courseRepository);


            var gradeRepository = new GradeRepository("teachers_management.db");
            var gradeService = new GradeService(gradeRepository, studentRepository, courseRepository);

            // Run the form
            //Application.Run(new StudentManagement(studentService));
            //Application.Run(new CourseManagement(courseService));
            Application.Run(new GradeManagement(gradeService, studentService, courseService));
        }
    }
}