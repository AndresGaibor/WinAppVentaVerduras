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
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
        }
        //JIJIJIA
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Rpt_Verduras rpt_Verduras = new Rpt_Verduras();
            rpt_Verduras.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ReporteCliente rc = new ReporteCliente();
            rc.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ReporteFacturas rf = new ReporteFacturas();
            rf.Show();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ReporteVentasFecha rf = new ReporteVentasFecha();
            rf.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ReporteCompraCliente rf = new ReporteCompraCliente();
            rf.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ReporteClientesGenero reporteClientesGenero = new ReporteClientesGenero();
            reporteClientesGenero.Show();
        }
    }
}
