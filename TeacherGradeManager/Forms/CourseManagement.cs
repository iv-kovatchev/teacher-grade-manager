using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using TeacherGradeManager.Models;
using TeacherGradeManager.Services.CourseService;

namespace TeacherGradeManager.Forms
{
    public partial class CourseManagement : Form
    {
        private readonly ICourseService _courseService;

        private Course _selectedCourse = null;

        public CourseManagement(ICourseService courseService)
        {
            InitializeComponent();
            _courseService = courseService;
            this.Load += CourseManagement_Load;
        }

        private void CourseManagement_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            LoadCourses();
        }

        private void InitializeComboBoxes()
        {
            //Populate DayOfWeek ComboBox
            cmbDayOfWeek.Items.Clear();
            cmbDayOfWeek.Items.Add("Monday");
            cmbDayOfWeek.Items.Add("Tuesday");
            cmbDayOfWeek.Items.Add("Wednesday");
            cmbDayOfWeek.Items.Add("Thursday");
            cmbDayOfWeek.Items.Add("Friday");
            cmbDayOfWeek.Items.Add("Saturday");
            cmbDayOfWeek.Items.Add("Sunday");
            cmbDayOfWeek.DropDownStyle = ComboBoxStyle.DropDownList;

            //Populate Type ComboBox
            cmbType.Items.Clear();
            cmbType.Items.Add("Lecture");
            cmbType.Items.Add("Exercise");
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }

                string courseName = txtCourseName.Text.Trim();
                DayOfWeek dayOfWeek = GetSelectedDayOfWeek();
                TimeSpan time = dtpTime.Value.TimeOfDay;
                CourseType type = GetSelectedCourseType();

                var newCourse = new Course
                {
                    Name = courseName,
                    DayOfWeek = dayOfWeek,
                    Time = time,
                    Type = type
                };

                _courseService.AddCourse(newCourse);

                MessageBox.Show("Course added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCourses();
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
                if (_selectedCourse == null)
                {
                    MessageBox.Show("Please select a course to edit.", "No Selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidateInputs())
                {
                    return;
                }

                _selectedCourse.Name = txtCourseName.Text.Trim();
                _selectedCourse.DayOfWeek = GetSelectedDayOfWeek();
                _selectedCourse.Time = dtpTime.Value.TimeOfDay;
                _selectedCourse.Type = GetSelectedCourseType();

                _courseService.UpdateCourse(_selectedCourse);

                MessageBox.Show("Course updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCourses();

                _selectedCourse = null;
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
                if (_selectedCourse == null)
                {
                    MessageBox.Show("Please select a course to delete.", "No Selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string message = $"Are you sure you want to delete this course?\n\n" +
                        $"Name: {_selectedCourse.Name}\n" +
                        $"Day: {_selectedCourse.DayOfWeek}\n" +
                        $"Time: {_selectedCourse.Time:hh\\:mm}\n" +
                        $"Type: {_selectedCourse.Type}";

                DialogResult result = MessageBox.Show(message, "Confirm Deletion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _courseService.DeleteCourse(_selectedCourse.Id);

                    MessageBox.Show("Course deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadCourses();

                    _selectedCourse = null;
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
            dgvCourses.ClearSelection();
            ClearInputs();
            _selectedCourse = null;
        }

        private void LoadCourses()
        {
            try
            {
                var courses = _courseService.GetAllCourses();

                dgvCourses.DataSource = null;
                dgvCourses.DataSource = courses;

                // Remove empty column before ID
                dgvCourses.RowHeadersVisible = false;

                if (dgvCourses.Columns.Count > 0)
                {
                    dgvCourses.Columns["Id"].HeaderText = "ID";
                    dgvCourses.Columns["Name"].HeaderText = "Course Name";
                    dgvCourses.Columns["DayOfWeek"].HeaderText = "Day";
                    dgvCourses.Columns["Time"].HeaderText = "Time";
                    dgvCourses.Columns["Type"].HeaderText = "Type";

                    dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dgvCourses.Columns["Id"].Width = 50;
                    dgvCourses.Columns["Name"].Width = 200;
                    dgvCourses.Columns["DayOfWeek"].Width = 100;
                    dgvCourses.Columns["Time"].Width = 100;
                    dgvCourses.Columns["Type"].Width = 100;

                    dgvCourses.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading courses: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtCourseName.Clear();
            cmbDayOfWeek.SelectedIndex = -1;
            dtpTime.Value = DateTime.Today.AddHours(9);
            cmbType.SelectedIndex = -1;
            txtCourseName.Focus();
        }

        private bool ValidateInputs()
        {
            // Validate Course Name
            if (string.IsNullOrWhiteSpace(txtCourseName.Text.Trim()))
            {
                MessageBox.Show("Please enter Course Name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCourseName.Focus();
                return false;
            }

            // Validate Day of Week
            if (cmbDayOfWeek.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Day of Week", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDayOfWeek.Focus();
                return false;
            }

            // Validate Type
            if (cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Course Type", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.Focus();
                return false;
            }

            return true;
        }

        private DayOfWeek GetSelectedDayOfWeek()
        {
            switch (cmbDayOfWeek.SelectedItem.ToString())
            {
                case "Monday": return DayOfWeek.Monday;
                case "Tuesday": return DayOfWeek.Tuesday;
                case "Wednesday": return DayOfWeek.Wednesday;
                case "Thursday": return DayOfWeek.Thursday;
                case "Friday": return DayOfWeek.Friday;
                case "Saturday": return DayOfWeek.Saturday;
                case "Sunday": return DayOfWeek.Sunday;
                default: return DayOfWeek.Monday;
            }
        }

        private void SetDayOfWeekComboBox(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: cmbDayOfWeek.SelectedIndex = 0; break;
                case DayOfWeek.Tuesday: cmbDayOfWeek.SelectedIndex = 1; break;
                case DayOfWeek.Wednesday: cmbDayOfWeek.SelectedIndex = 2; break;
                case DayOfWeek.Thursday: cmbDayOfWeek.SelectedIndex = 3; break;
                case DayOfWeek.Friday: cmbDayOfWeek.SelectedIndex = 4; break;
                case DayOfWeek.Saturday: cmbDayOfWeek.SelectedIndex = 5; break;
                case DayOfWeek.Sunday: cmbDayOfWeek.SelectedIndex = 6; break;
            }
        }

        private CourseType GetSelectedCourseType()
        {
            switch (cmbType.SelectedIndex)
            {
                case 0: return CourseType.Lecture;
                case 1: return CourseType.Exercise;
                default: return CourseType.Lecture;
            }
        }

        private void SetTypeComboBox(CourseType type)
        {
            switch (type)
            {
                case CourseType.Lecture: cmbType.SelectedIndex = 0; break;
                case CourseType.Exercise: cmbType.SelectedIndex = 1; break;
            }
        }

        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                _selectedCourse = (Course)dgvCourses.SelectedRows[0].DataBoundItem;

                txtCourseName.Text = _selectedCourse.Name;
                SetDayOfWeekComboBox(_selectedCourse.DayOfWeek);
                dtpTime.Value = DateTime.Today.Add(_selectedCourse.Time);
                SetTypeComboBox(_selectedCourse.Type);

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;

                btnAdd.Enabled = false;
            }
            else
            {
                _selectedCourse = null;
                ClearInputs();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;

                btnAdd.Enabled = true;
            }
        }
    }
}
