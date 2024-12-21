using System;
using System.Windows.Forms;

namespace QuanLiThuVienUeh
{
    public partial class Form_Login_Main : Form
    {
        public Form_Login_Main()
        {
            InitializeComponent();

        }

        private void label_UehStudent_Click(object sender, EventArgs e)
        {
            Form_Login_Student formLoginStudent = new Form_Login_Student();
            formLoginStudent.Show();

            this.Hide();
        }

        private void label_UehStaff_Click(object sender, EventArgs e)
        {
            Form_Login_Staff formLoginStaff = new Form_Login_Staff();
            formLoginStaff.Show();

            this.Hide();
        }

        private void Form_Login_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
