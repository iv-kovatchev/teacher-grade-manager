using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;

namespace TeacherGradeManager.Services.StudentService
{
    public interface IStudentService
    {
        void AddStudent(Student student);

        void UpdateStudent(Student student);

        void DeleteStudent(Student student);

        Student GetStudentById(int id);

        List<Student> GetAllStudents();
    }
}
