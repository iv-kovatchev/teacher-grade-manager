using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;
using TeacherGradeManager.Repositories;

namespace TeacherGradeManager.Services.GradeService
{
    public class GradeService : IGradeService
    {
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;

        public GradeService(
            IRepository<Grade> gradeRepository,
            IRepository<Student> studentRepository,
            IRepository<Course> courseRepository)
        {
            _gradeRepository = gradeRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public List<Grade> GetAllGrades()
        {
            return _gradeRepository.GetAll();
        }

        public Grade GetGradeById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Grade ID must be greater than 0", nameof(id));
            }

            var grade = _gradeRepository.GetById(id);

            if (grade == null)
            {
                throw new InvalidOperationException($"Grade with ID {id} not found");
            }

            return grade;
        }

        public void AddGrade(Grade grade)
        {
            ValidateGrade(grade);

            var existingGrades = _gradeRepository.GetAll();

            if (existingGrades.Any(g => g.StudentId == grade.StudentId && g.CourseId == grade.CourseId))
            {
                throw new InvalidOperationException(
                    $"A grade already exists for this student in this course");
            }

            _gradeRepository.Add(grade);
        }

        public void UpdateGrade(Grade grade)
        {
            ValidateGrade(grade);

            var existingGrade = _gradeRepository.GetById(grade.Id);

            if (existingGrade == null)
            {
                throw new ValidationException($"Grade with ID {grade.Id} not found");
            }

            var allGrades = _gradeRepository.GetAll();

            if (allGrades.Any(g => g.StudentId == grade.StudentId &&
                           g.CourseId == grade.CourseId &&
                           g.Id != grade.Id))
            {
                throw new InvalidOperationException(
                    $"A grade already exists for this student in this course");
            }

            _gradeRepository.Update(grade);
        }

        public void DeleteGrade(int gradeId)
        {
            if (gradeId <= 0)
            {
                throw new ArgumentException("Grade ID must be greater than 0", nameof(gradeId));
            }

            var existingGrade = _gradeRepository.GetById(gradeId);

            if (existingGrade == null)
            {
                throw new ValidationException($"Grade with ID {gradeId} not found. Cannot delete.");
            }

            _gradeRepository.Delete(gradeId);
        }

        public List<Grade> GetGradesByCourse(int courseId)
        {
            var gradeRepo = _gradeRepository as GradeRepository;

            if (gradeRepo == null)
            {
                throw new InvalidOperationException("Grade repository is not of type GradeRepository");
            }

            return gradeRepo.GetGradesByStudent(courseId);
        }

        public List<Grade> GetGradesByStudent(int studentId)
        {
            var gradeRepo = _gradeRepository as GradeRepository;

            if (gradeRepo == null)
            {
                throw new InvalidOperationException("Grade repository is not of type GradeRepository");
            }

            return gradeRepo.GetGradesByStudent(studentId);
        }

        private void ValidateGrade(Grade grade)
        {
            if (grade == null)
            {
                throw new ArgumentNullException(nameof(grade), "Grade cannot be null");
            }

            var validationContext = new ValidationContext(grade);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(grade, validationContext, validationResults, true);

            if (!isValid)
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Grade validation failed: {errors}");
            }

            decimal remainder = grade.GradeValue % 0.5m;

            if (remainder != 0)
            {
                throw new ValidationException(
            "Grade must be in 0.50 increments (2.00, 2.50, 3.00, 3.50, 4.00, 4.50, 5.00, 5.50, 6.00)");
            }

            //Validate if student and course exist
            var student = _studentRepository.GetById(grade.StudentId);
            if (student == null)
            {
                throw new ValidationException($"Student with ID {grade.StudentId} not found");
            }

            var course = _courseRepository.GetById(grade.CourseId);
            if (course == null)
            {
                throw new ValidationException($"Course with ID {grade.CourseId} not found");
            }
        }
    }
}
