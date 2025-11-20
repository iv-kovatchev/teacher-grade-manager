using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeacherGradeManager.Models;
using TeacherGradeManager.Services.CourseService;
using TeacherGradeManager.Services.GradeService;
using TeacherGradeManager.Services.StudentService;

namespace TeacherGradeManager.Forms
{
    public partial class GradeManagement : Form
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        private Grade _selectedGrade = null;

        private List<Student> _allStudents;
        private List<Course> _allCourses;

        private ToolTip toolTip = new ToolTip();

        public GradeManagement(
            IGradeService gradeService,
            IStudentService studentService,
            ICourseService courseService)
        {
            InitializeComponent();

            _gradeService = gradeService;
            _studentService = studentService;
            _courseService = courseService;

            cmbCourse.MouseHover += cmbCourse_MouseHover;

            dgvGrades.SelectionChanged += dgvGrades_SelectionChanged;

            this.Load += GradeManagement_Load;
        }

        private void GradeManagement_Load(object sender, EventArgs e)
        {
            LoadStudentsComboBox();
            LoadCoursesComboBox();
            LoadGradeValuesComboBox();
            LoadGrades();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }

                int studentIndex = cmbStudent.SelectedIndex;
                var selectedStudent = _allStudents[studentIndex];

                int courseIndex = cmbCourse.SelectedIndex;
                var selectedCourse = _allCourses[courseIndex];

                decimal gradeValue = (decimal)cmbGradeValue.SelectedItem;

                var newGrade = new Grade
                {
                    StudentId = selectedStudent.Id,
                    CourseId = selectedCourse.Id,
                    GradeValue = gradeValue
                };

                _gradeService.AddGrade(newGrade);

                MessageBox.Show("Grade added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadGrades();

                ClearInputs();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show($"Validation Error: {ex.Message}", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedGrade == null)
                {
                    MessageBox.Show("Please select a grade to edit.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInputs())
                {
                    return;
                }

                int studentIndex = cmbStudent.SelectedIndex;
                var selectedStudent = _allStudents[studentIndex];

                int courseIndex = cmbCourse.SelectedIndex;
                var selectedCourse = _allCourses[courseIndex];

                decimal gradeValue = (decimal)cmbGradeValue.SelectedItem;

                _selectedGrade.StudentId = selectedStudent.Id;
                _selectedGrade.CourseId = selectedCourse.Id;
                _selectedGrade.GradeValue = gradeValue;

                _gradeService.UpdateGrade(_selectedGrade);

                MessageBox.Show("Grade updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadGrades();

                _selectedGrade = null;
                ClearInputs();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show($"Validation Error: {ex.Message}", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedGrade == null)
                {
                    MessageBox.Show("Please select a grade to delete.", "No Selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Get student and course names for confirmation message
                var student = _allStudents.FirstOrDefault(s => s.Id == _selectedGrade.StudentId);
                var course = _allCourses.FirstOrDefault(c => c.Id == _selectedGrade.CourseId);

                string studentName = student != null
                    ? $"{student.FirstName} {student.LastName} ({student.FacultyNumber})"
                    : "Unknown Student";
                string courseName = course != null ? course.Name : "Unknown Course";

                string message = $"Are you sure you want to delete this grade?\n\n" +
                        $"Student: {studentName}\n" +
                        $"Course: {courseName}\n" +
                        $"Grade: {_selectedGrade.GradeValue:0.00}";

                DialogResult result = MessageBox.Show(message, "Confirm Deletion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _gradeService.DeleteGrade(_selectedGrade.Id);

                    MessageBox.Show("Grade deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadGrades();

                    _selectedGrade = null;
                    ClearInputs();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dgvGrades.ClearSelection();
            ClearInputs();
            _selectedGrade = null;
        }

        private void LoadStudentsComboBox()
        {
            try
            {
                _allStudents = _studentService.GetAllStudents();
                cmbStudent.Items.Clear();


                foreach (var student in _allStudents)
                {
                    cmbStudent.Items.Add($"{student.FirstName} {student.LastName} ({student.FacultyNumber})");
                }

                cmbStudent.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCoursesComboBox()
        {
            try
            {
                _allCourses = _courseService.GetAllCourses();

                cmbCourse.Items.Clear();

                foreach (var course in _allCourses)
                {
                    cmbCourse.Items.Add($"{course.Name} - {course.DayOfWeek} at {course.Time:hh\\:mm}");
                }

                cmbStudent.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGradeValuesComboBox()
        {
            cmbGradeValue.Items.Clear();

            for (decimal grade = 2.00M; grade <= 6.00M; grade += 0.50M)
            {
                cmbGradeValue.Items.Add(grade);
            }

            cmbGradeValue.SelectedIndex = -1;
        }

        private void LoadGrades()
        {
            try
            {
                var grades = _gradeService.GetAllGrades();

                var students = _studentService.GetAllStudents();
                var courses = _courseService.GetAllCourses();

                var gradeDisplayList = new List<GradeDisplay>();

                foreach (var grade in grades)
                {
                    var student = students.FirstOrDefault(s => s.Id == grade.StudentId);

                    var course = courses.FirstOrDefault(c => c.Id == grade.CourseId);

                    gradeDisplayList.Add(new GradeDisplay
                    {
                        Id = grade.Id,
                        StudentId = grade.StudentId,
                        CourseId = grade.CourseId,
                        StudentName = student != null
                            ? $"{student.FirstName} {student.LastName} ({student.FacultyNumber})"
                            : "Unknown Student",
                        CourseName = course != null
                            ? course.Name
                            : "Unknown Course",
                        GradeValue = grade.GradeValue
                    });
                }

                dgvGrades.DataSource = null;
                dgvGrades.DataSource = gradeDisplayList;

                if (dgvGrades.Columns.Count > 0)
                {
                    dgvGrades.RowHeadersVisible = false;

                    dgvGrades.Columns["Id"].HeaderText = "ID";
                    dgvGrades.Columns["StudentName"].HeaderText = "Student";
                    dgvGrades.Columns["CourseName"].HeaderText = "Course";
                    dgvGrades.Columns["GradeValue"].HeaderText = "Grade";

                    dgvGrades.Columns["StudentId"].Visible = false;
                    dgvGrades.Columns["CourseId"].Visible = false;

                    dgvGrades.Columns["GradeValue"].DefaultCellStyle.Format = "0.00";

                    dgvGrades.Columns["Id"].Width = 50;
                    dgvGrades.Columns["StudentName"].Width = 250;
                    dgvGrades.Columns["CourseName"].Width = 200;
                    dgvGrades.Columns["GradeValue"].Width = 100;

                    dgvGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dgvGrades.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading grades: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCourse_MouseHover(object sender, EventArgs e)
        {
            if (cmbCourse.SelectedIndex >= 0)
            {
                string fullText = cmbCourse.SelectedItem.ToString();

                // Show tooltip with full text
                toolTip.SetToolTip(cmbCourse, fullText);
            }
        }

        private void dgvGrades_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGrades.SelectedRows.Count > 0)
            {
                var selectedGradeDisplay = (GradeDisplay)dgvGrades.SelectedRows[0].DataBoundItem;

                _selectedGrade = _gradeService.GetGradeById(selectedGradeDisplay.Id);

                int studentIndex = _allStudents.FindIndex(s => s.Id == _selectedGrade.StudentId);
                if (studentIndex >= 0)
                {
                    cmbStudent.SelectedIndex = studentIndex;
                }

                int courseIndex = _allCourses.FindIndex(c => c.Id == _selectedGrade.CourseId);
                if (courseIndex >= 0)
                {
                    cmbCourse.SelectedIndex = courseIndex;
                }

                for (int i = 0; i < cmbGradeValue.Items.Count; i++)
                {
                    if ((decimal)cmbGradeValue.Items[i] == _selectedGrade.GradeValue)
                    {
                        cmbGradeValue.SelectedIndex = i;
                        break;
                    }
                }

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;

                btnAdd.Enabled = false;
            }
            else
            {
                _selectedGrade = null;
                ClearInputs();

                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;

                btnAdd.Enabled = true;
            }
        }

        private bool ValidateInputs()
        {
            if (cmbStudent.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a student", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStudent.Focus();
                return false;
            }

            if (cmbCourse.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a course", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCourse.Focus();
                return false;
            }

            if (cmbGradeValue.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a grade value", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGradeValue.Focus();
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            cmbStudent.SelectedIndex = -1;
            cmbCourse.SelectedIndex = -1;
            cmbGradeValue.SelectedIndex = -1;
            cmbStudent.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class GradeDisplay
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public decimal GradeValue { get; set; }

        //Hidden properties
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
