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
    public partial class MostarReporte : Form
    {
        public MostarReporte()
        {
            InitializeComponent();
        }

        private void MostarReporte_Load(object sender, EventArgs e)
        {
            // guardar el xml por defecto del dataset
            this.dataSet1.WriteXml("D:\\aplicaciones\\DataSet1.xml");
            object[] parametros = { "1", "Andres" };
            this.dataSet1.Estudiantes.Rows.Add(parametros);
            // imprime los datos del dataset en un messagebox
            foreach (DataRow row in this.dataSet1.Estudiantes.Rows)
            {
                string id = row["Codigo"].ToString();
                string nombre = row["Nombre"].ToString();
                MessageBox.Show(id + " " + nombre);
            }
            this.dataSet1.WriteXml("D:\\aplicaciones\\DataSet1.xml");
            this.reportViewer1.RefreshReport();
        }
    }
}
