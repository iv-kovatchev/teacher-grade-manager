using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;

namespace TeacherGradeManager.Services.StudentService
{
    internal class StudentService: IStudentService
    {
        private List<Student> _students;
        
        private int _nextId;

        public StudentService()
        {
            _students = new List<Student>();
            _nextId = 0;
        }
    }
}
