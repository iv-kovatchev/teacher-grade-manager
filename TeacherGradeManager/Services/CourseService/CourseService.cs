using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;
using TeacherGradeManager.Repositories;

namespace TeacherGradeManager.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public Course GetCourseById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Course ID must be greater than 0", nameof(id));
            }

            var course = _courseRepository.GetById(id);

            if (course == null)
            {
                throw new InvalidOperationException($"Course with ID {id} not found");
            }

            return course;
        }

        public void AddCourse(Course course)
        {
            ValidateCourse(course);

            var existingCourses = _courseRepository.GetAll();

            if (existingCourses.Any(c => c.Name.Equals(course.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"A course with the name '{course.Name}' already exists");
            }

            _courseRepository.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            ValidateCourse(course);

            var existingCourse = _courseRepository.GetById(course.Id);

            if (existingCourse == null)
            {
                throw new ValidationException($"Course with ID {course.Id} not found");
            }

            var allCourses = _courseRepository.GetAll();

            if (allCourses.Any(c => c.Name.Equals(course.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"A course with the name '{course.Name}' already exists");
            }

            _courseRepository.Update(course);
        }
        public void DeleteCourse(int courseId)
        {
            if (courseId <= 0)
            {
                throw new ArgumentException("Course ID must be greater than 0", nameof(courseId));
            }

            var existingCourse = _courseRepository.GetById(courseId);

            if (existingCourse == null)
            {
                throw new ValidationException($"Course with ID {courseId} not found. Cannot delete.");
            }

            _courseRepository.Delete(courseId);
        }

        private void ValidateCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course), "Course cannot be null");
            }

            course.Name = course.Name.Trim();

            var validationContext = new ValidationContext(course);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(course, validationContext, validationResults, true);

            if (!isValid)
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Course validation failed: {errors}");
            }
        }
    }
}
