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
using TeacherGradeManager.Services.StudentService;

namespace TeacherGradeManager.Forms
{
    public partial class StudentManagement : Form
    {
        private readonly IStudentService _studentService;

        private Student _selectedStudent = null;

        public StudentManagement(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService;
            this.Load += StudentManagement_Load;
        }

        private void StudentManagement_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                var students = _studentService.GetAllStudents();

                dgvStudents.DataSource = null;
                dgvStudents.DataSource = students;

                // Remove empty column before ID
                dgvStudents.RowHeadersVisible = false;

                if (dgvStudents.Columns.Count > 0)
                {
                    dgvStudents.Columns["Id"].HeaderText = "ID";
                    dgvStudents.Columns["FirstName"].HeaderText = "First Name";
                    dgvStudents.Columns["LastName"].HeaderText = "Last Name";
                    dgvStudents.Columns["FacultyNumber"].HeaderText = "Faculty Number";

                    dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dgvStudents.Columns["Id"].Width = 50;
                    dgvStudents.Columns["FirstName"].Width = 200;
                    dgvStudents.Columns["LastName"].Width = 200;
                    dgvStudents.Columns["FacultyNumber"].Width = 150;

                    dgvStudents.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }

                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string facultyNumber = txtFacultyNumber.Text.Trim();

                var newStudent = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    FacultyNumber = facultyNumber
                };

                _studentService.AddStudent(newStudent);

                MessageBox.Show("Student added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadStudents();

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
                if (_selectedStudent == null)
                {
                    MessageBox.Show("Please select a student to edit.", "No Selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidateInputs())
                {
                    return;
                }

                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string facultyNumber = txtFacultyNumber.Text.Trim();

                _selectedStudent.FirstName = firstName;
                _selectedStudent.LastName = lastName;
                _selectedStudent.FacultyNumber = facultyNumber;

                _studentService.UpdateStudent(_selectedStudent);

                MessageBox.Show("Student updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadStudents();

                _selectedStudent = null;
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
                if (_selectedStudent == null)
                {
                    MessageBox.Show("Please select a student to delete.", "No Selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string message = $"Are you sure you want to delete this student?\n\n" +
                        $"Name: {_selectedStudent.FirstName} {_selectedStudent.LastName}\n" +
                        $"Faculty Number: {_selectedStudent.FacultyNumber}";

                DialogResult result = MessageBox.Show(message, "Confirm Deletion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _studentService.DeleteStudent(_selectedStudent.Id);

                    MessageBox.Show("Student deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadStudents();

                    _selectedStudent = null;
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
            dgvStudents.ClearSelection();

            ClearInputs();

            _selectedStudent = null;
        }

        private void ClearInputs()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtFacultyNumber.Clear();
            txtFirstName.Focus();
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                _selectedStudent = (Student)dgvStudents.SelectedRows[0].DataBoundItem;

                txtFirstName.Text = _selectedStudent.FirstName;
                txtLastName.Text = _selectedStudent.LastName;
                txtFacultyNumber.Text = _selectedStudent.FacultyNumber;

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;

                btnAdd.Enabled = false;
            }
            else
            {
                _selectedStudent = null;
                ClearInputs();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;

                btnAdd.Enabled = true;
            }
        }

        private bool ValidateInputs()
        {
            // Validate First Name
            if (string.IsNullOrWhiteSpace(txtFirstName.Text.Trim()))
            {
                MessageBox.Show("Please enter First Name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            // Validate Last Name
            if (string.IsNullOrWhiteSpace(txtLastName.Text.Trim()))
            {
                MessageBox.Show("Please enter Last Name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            // Validate Faculty Number
            if (string.IsNullOrWhiteSpace(txtFacultyNumber.Text.Trim()))
            {
                MessageBox.Show("Please enter Faculty Number", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFacultyNumber.Focus();
                return false;
            }

            return true;
        }
    }
}
