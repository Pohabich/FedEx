using System;
using System.Windows.Forms;

namespace FedEx2
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtFirstName.Text) && string.IsNullOrEmpty(txtLastName.Text) && string.IsNullOrEmpty(txtPhone.Text) && string.IsNullOrEmpty(txtEmail.Text))
            //    return;

            frmResultScreen resScreen = new frmResultScreen(txtFirstName.Text, txtLastName.Text, txtPhone.Text, txtEmail.Text);
            resScreen.ShowDialog();//Show();
        }
    }
}