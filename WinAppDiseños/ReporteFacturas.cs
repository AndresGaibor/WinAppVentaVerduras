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
    public partial class ReporteFacturas : Form
    {
        public ReporteFacturas()
        {
            InitializeComponent();
        }

        private void ReporteFacturas_Load(object sender, EventArgs e)
        {
            // guardar el xml por defecto del dataset
            this.dataSet1.leerXml();
            this.reportViewer1.RefreshReport();
        }
    }
}
