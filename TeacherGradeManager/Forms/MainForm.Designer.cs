namespace TeacherGradeManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnManageStudents = new Button();
            btnManageCourses = new Button();
            btnManageGrades = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(55, 30);
            label1.Name = "label1";
            label1.Size = new Size(487, 38);
            label1.TabIndex = 0;
            label1.Text = "Teacher Grade Management System";
            // 
            // btnManageStudents
            // 
            btnManageStudents.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnManageStudents.Location = new Point(160, 120);
            btnManageStudents.Name = "btnManageStudents";
            btnManageStudents.Size = new Size(250, 40);
            btnManageStudents.TabIndex = 1;
            btnManageStudents.Text = "📚 Manage Students";
            btnManageStudents.UseVisualStyleBackColor = true;
            btnManageStudents.Click += btnManageStudents_Click;
            // 
            // btnManageCourses
            // 
            btnManageCourses.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnManageCourses.Location = new Point(160, 190);
            btnManageCourses.Name = "btnManageCourses";
            btnManageCourses.Size = new Size(250, 40);
            btnManageCourses.TabIndex = 2;
            btnManageCourses.Text = "📘 Manage Courses";
            btnManageCourses.UseVisualStyleBackColor = true;
            btnManageCourses.Click += btnManageCourses_Click;
            // 
            // btnManageGrades
            // 
            btnManageGrades.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnManageGrades.Location = new Point(160, 260);
            btnManageGrades.Name = "btnManageGrades";
            btnManageGrades.Size = new Size(250, 40);
            btnManageGrades.TabIndex = 3;
            btnManageGrades.Text = "📝 Manage Grades";
            btnManageGrades.UseVisualStyleBackColor = true;
            btnManageGrades.Click += btnManageGrades_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 353);
            Controls.Add(btnManageGrades);
            Controls.Add(btnManageCourses);
            Controls.Add(btnManageStudents);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Teacher Grade Management System";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnManageStudents;
        private Button btnManageCourses;
        private Button btnManageGrades;
    }
}
