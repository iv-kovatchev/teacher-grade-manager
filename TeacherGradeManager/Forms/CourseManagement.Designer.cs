namespace TeacherGradeManager.Forms
{
    partial class CourseManagement
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
            dgvCourses = new DataGridView();
            panel1 = new Panel();
            btnCancel = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            cmbType = new ComboBox();
            dtpTime = new DateTimePicker();
            cmbDayOfWeek = new ComboBox();
            txtCourseName = new TextBox();
            lblType = new Label();
            lblTime = new Label();
            lblDayOfWeek = new Label();
            lblCourseName = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCourses
            // 
            dgvCourses.AllowUserToAddRows = false;
            dgvCourses.AllowUserToDeleteRows = false;
            dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCourses.Location = new Point(0, 0);
            dgvCourses.Name = "dgvCourses";
            dgvCourses.RowHeadersWidth = 51;
            dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCourses.Size = new Size(882, 317);
            dgvCourses.TabIndex = 0;
            dgvCourses.SelectionChanged += dgvCourses_SelectionChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(cmbType);
            panel1.Controls.Add(dtpTime);
            panel1.Controls.Add(cmbDayOfWeek);
            panel1.Controls.Add(txtCourseName);
            panel1.Controls.Add(lblType);
            panel1.Controls.Add(lblTime);
            panel1.Controls.Add(lblDayOfWeek);
            panel1.Controls.Add(lblCourseName);
            panel1.Location = new Point(0, 318);
            panel1.Name = "panel1";
            panel1.Size = new Size(882, 235);
            panel1.TabIndex = 5;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(732, 195);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 30);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(732, 15);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 30);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Delete Course";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(582, 15);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 30);
            btnEdit.TabIndex = 9;
            btnEdit.Text = "Edit Course";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(266, 195);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add Course";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // cmbType
            // 
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(160, 150);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(200, 28);
            cmbType.TabIndex = 7;
            // 
            // dtpTime
            // 
            dtpTime.CustomFormat = "HH:mm";
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.Location = new Point(160, 105);
            dtpTime.Name = "dtpTime";
            dtpTime.ShowUpDown = true;
            dtpTime.Size = new Size(200, 27);
            dtpTime.TabIndex = 6;
            // 
            // cmbDayOfWeek
            // 
            cmbDayOfWeek.FormattingEnabled = true;
            cmbDayOfWeek.Location = new Point(160, 60);
            cmbDayOfWeek.Name = "cmbDayOfWeek";
            cmbDayOfWeek.Size = new Size(200, 28);
            cmbDayOfWeek.TabIndex = 5;
            // 
            // txtCourseName
            // 
            txtCourseName.Location = new Point(160, 15);
            txtCourseName.Name = "txtCourseName";
            txtCourseName.Size = new Size(200, 27);
            txtCourseName.TabIndex = 4;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(20, 150);
            lblType.Name = "lblType";
            lblType.Size = new Size(43, 20);
            lblType.TabIndex = 3;
            lblType.Text = "Type:";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(20, 105);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(45, 20);
            lblTime.TabIndex = 2;
            lblTime.Text = "Time:";
            // 
            // lblDayOfWeek
            // 
            lblDayOfWeek.AutoSize = true;
            lblDayOfWeek.Location = new Point(20, 60);
            lblDayOfWeek.Name = "lblDayOfWeek";
            lblDayOfWeek.Size = new Size(96, 20);
            lblDayOfWeek.TabIndex = 1;
            lblDayOfWeek.Text = "Day of Week:";
            // 
            // lblCourseName
            // 
            lblCourseName.AutoSize = true;
            lblCourseName.Location = new Point(20, 15);
            lblCourseName.Name = "lblCourseName";
            lblCourseName.Size = new Size(101, 20);
            lblCourseName.TabIndex = 0;
            lblCourseName.Text = "Course Name:";
            // 
            // CourseManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 553);
            Controls.Add(panel1);
            Controls.Add(dgvCourses);
            Name = "CourseManagement";
            Text = "Course Management";
            ((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCourses;
        private Panel panel1;
        private Label lblType;
        private Label lblTime;
        private Label lblDayOfWeek;
        private Label lblCourseName;
        private TextBox txtCourseName;
        private ComboBox cmbDayOfWeek;
        private DateTimePicker dtpTime;
        private ComboBox cmbType;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnCancel;
    }
}