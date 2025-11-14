using System.ComponentModel.DataAnnotations;
using TeacherGradeManager.Forms;
using TeacherGradeManager.Models;
using TeacherGradeManager.Repositories;
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
            var repository = new StudentRepository("teachers_management.db");
            var service = new StudentService(repository);

            // Run the form
            Application.Run(new StudentManagement(service));
        }
    }
}