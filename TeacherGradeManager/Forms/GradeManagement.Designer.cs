namespace TeacherGradeManager.Forms
{
    partial class GradeManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvGrades = new DataGridView();
            panel1 = new Panel();
            btnCancel = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            cmbGradeValue = new ComboBox();
            cmbCourse = new ComboBox();
            cmbStudent = new ComboBox();
            lblGrade = new Label();
            lblCourse = new Label();
            lblStudent = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGrades).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvGrades
            // 
            dgvGrades.AllowUserToAddRows = false;
            dgvGrades.AllowUserToDeleteRows = false;
            dgvGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrades.Location = new Point(0, 0);
            dgvGrades.MultiSelect = false;
            dgvGrades.Name = "dgvGrades";
            dgvGrades.ReadOnly = true;
            dgvGrades.RowHeadersWidth = 51;
            dgvGrades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrades.Size = new Size(882, 353);
            dgvGrades.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnBack);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(cmbGradeValue);
            panel1.Controls.Add(cmbCourse);
            panel1.Controls.Add(cmbStudent);
            panel1.Controls.Add(lblGrade);
            panel1.Controls.Add(lblCourse);
            panel1.Controls.Add(lblStudent);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 353);
            panel1.Name = "panel1";
            panel1.Size = new Size(882, 200);
            panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(732, 65);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(732, 16);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete Grade";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(582, 16);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 30);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "Edit Grade";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(240, 150);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add Grade";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // cmbGradeValue
            // 
            cmbGradeValue.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGradeValue.FormattingEnabled = true;
            cmbGradeValue.Location = new Point(160, 105);
            cmbGradeValue.Name = "cmbGradeValue";
            cmbGradeValue.Size = new Size(200, 28);
            cmbGradeValue.TabIndex = 5;
            // 
            // cmbCourse
            // 
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.FormattingEnabled = true;
            cmbCourse.Location = new Point(160, 60);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(200, 28);
            cmbCourse.TabIndex = 4;
            // 
            // cmbStudent
            // 
            cmbStudent.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStudent.FormattingEnabled = true;
            cmbStudent.Location = new Point(160, 15);
            cmbStudent.Name = "cmbStudent";
            cmbStudent.Size = new Size(200, 28);
            cmbStudent.TabIndex = 3;
            // 
            // lblGrade
            // 
            lblGrade.AutoSize = true;
            lblGrade.Location = new Point(20, 105);
            lblGrade.Name = "lblGrade";
            lblGrade.Size = new Size(52, 20);
            lblGrade.TabIndex = 2;
            lblGrade.Text = "Grade:";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(20, 60);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(57, 20);
            lblCourse.TabIndex = 1;
            lblCourse.Text = "Course:";
            // 
            // lblStudent
            // 
            lblStudent.AutoSize = true;
            lblStudent.Location = new Point(20, 15);
            lblStudent.Name = "lblStudent";
            lblStudent.Size = new Size(63, 20);
            lblStudent.TabIndex = 0;
            lblStudent.Text = "Student:";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(732, 150);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(120, 30);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // GradeManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 553);
            Controls.Add(panel1);
            Controls.Add(dgvGrades);
            Name = "GradeManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Grade Management";
            ((System.ComponentModel.ISupportInitialize)dgvGrades).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvGrades;
        private Panel panel1;
        private Label lblGrade;
        private Label lblCourse;
        private Label lblStudent;
        private ComboBox cmbGradeValue;
        private ComboBox cmbCourse;
        private ComboBox cmbStudent;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAdd;
        private Button btnBack;
    }
}