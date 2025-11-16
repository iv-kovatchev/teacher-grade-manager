using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;

namespace TeacherGradeManager.Services.GradeService
{
    public interface IGradeService
    {
        List<Grade> GetAllGrades();

        Grade GetGradeById(int id);

        void AddGrade(Grade grade);

        void UpdateGrade(Grade grade);

        void DeleteGrade(int gradeId);

        List<Grade> GetGradesByStudent(int studentId);

        List<Grade> GetGradesByCourse(int courseId);
    }
}
