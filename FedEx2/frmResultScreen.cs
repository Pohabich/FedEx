using System.Drawing;
using System.Windows.Forms;

namespace FedEx2
{
    public partial class frmResultScreen : Form
    {
        public frmResultScreen(string fName, string lName, string eMail, string phone)
        {
            InitializeComponent();

            dgResults.DataSource = DbManager.CalculateData(fName.Trim(), lName.Trim(), eMail.Trim(), phone.Trim());
        }

        private void dgResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int colIndex = e.ColumnIndex;

            if (e.ColumnIndex == (dgResults.Columns.Count - 1))
                e.CellStyle.BackColor = Color.CornflowerBlue;
            else
            {
                if (e.RowIndex % 2 == 1)
                    e.CellStyle.BackColor = Color.AliceBlue;
            }
        }
    }
}