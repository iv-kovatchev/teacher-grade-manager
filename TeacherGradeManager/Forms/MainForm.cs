using TeacherGradeManager.Forms;
using TeacherGradeManager.Services.CourseService;
using TeacherGradeManager.Services.GradeService;
using TeacherGradeManager.Services.StudentService;

namespace TeacherGradeManager
{
    public partial class MainForm : Form
    {
        private readonly IStudentService _studentService;

        private readonly ICourseService _courseService;

        private readonly IGradeService _gradeService;

        public MainForm(IStudentService studentService, ICourseService courseService, IGradeService gradeService)
        {
            InitializeComponent();
            _studentService = studentService;
            _courseService = courseService;
            _gradeService = gradeService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            this.Hide();
            var studentForm = new StudentManagement(_studentService);
            studentForm.ShowDialog();
            this.Show();
        }

        private void btnManageCourses_Click(object sender, EventArgs e)
        {
            this.Hide();
            var courseForm = new CourseManagement(_courseService);
            courseForm.ShowDialog();
            this.Show();
        }

        private void btnManageGrades_Click(object sender, EventArgs e)
        {
            this.Hide();
            var gradeForm = new GradeManagement(_gradeService, _studentService, _courseService);
            gradeForm.ShowDialog();
            this.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to exit?", 
                "Confirm Exit", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
