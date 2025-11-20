using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;
using TeacherGradeManager.Repositories;

namespace TeacherGradeManager.Services.StudentService
{
    internal class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repository;
        private readonly GradeRepository _gradeRepository;

        public StudentService(IRepository<Student> repository, GradeRepository gradeRepository)
        {
            _repository = repository;
            _gradeRepository = gradeRepository;
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }

        public Student GetStudentById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Student ID must be greater than 0", nameof(id));
            }

            var student = _repository.GetById(id);
            if (student == null)
            {
                throw new InvalidOperationException($"Student with ID {id} not found");
            }

            return student;
        }

        public void AddStudent(Student student)
        {
            //Validate the student data
            ValidateStudent(student);

            var existingStudents = _repository.GetAll();
            if (existingStudents.Any(s => s.FacultyNumber.Equals(student.FacultyNumber))) {
                throw new ValidationException("A student with this faculty number already exists.");
            }

            _repository.Add(student);
        }

        public void UpdateStudent(Student student)
        {

            ValidateStudent(student);

            var existingStudent = _repository.GetById(student.Id);
            if(existingStudent == null)
            {
                throw new ValidationException("Student not found.");
            }

            //Check if another student has the same faculty number
            var allStudents = _repository.GetAll();
            if (allStudents.Any(s => s.FacultyNumber == student.FacultyNumber && s.Id != student.Id))
            {
                throw new ValidationException("A student with this faculty number already exists");
            }

            _repository.Update(student);
        }

        public void DeleteStudent(int studentId)
        {
            if (studentId <= 0)
            {
                throw new ArgumentException("Student ID must be greater than 0", nameof(studentId));
            }

            var existingStudent = _repository.GetById(studentId);
            if (existingStudent == null)
            {
                throw new ValidationException($"Student with ID {studentId} not found. Cannot delete.");
            }

            var grades = _gradeRepository.GetGradesByStudent(studentId);
            if (grades.Any())
            {
                throw new InvalidOperationException(
                    $"Cannot delete student. Student has {grades.Count} grade(s) assigned. " +
                    "Please remove all grades before deleting the student.");
            }

            _repository.Delete(studentId);
        }

        private void ValidateStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

            student.FirstName = student.FirstName.Trim();
            student.LastName = student.LastName.Trim();
            student.FacultyNumber = student.FacultyNumber.Trim().ToUpper();

            //Validate using Data Annotations
            var validationContext = new ValidationContext(student);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(student, validationContext, validationResults, true);

            if(!isValid)
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Student validation failed: {errors}");
            }
        }
    }
}
