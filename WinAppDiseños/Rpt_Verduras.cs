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
    public partial class Rpt_Verduras : Form
    {
        public Rpt_Verduras()
        {
            InitializeComponent();
        }

        private void Rpt_Verduras_Load(object sender, EventArgs e)
        {
            // guardar el xml por defecto del dataset
            this.dataSet11.leerXml();
            this.reportViewer1.RefreshReport();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
