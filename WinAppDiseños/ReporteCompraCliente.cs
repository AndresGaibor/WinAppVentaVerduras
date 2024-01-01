using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppDiseños
{
    public partial class ReporteCompraCliente : Form
    {
        public ReporteCompraCliente()
        {
            InitializeComponent();
        }

        private void ReporteCompraCliente_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CmBox_Ced_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
