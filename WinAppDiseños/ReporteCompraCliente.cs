using Microsoft.Reporting.WinForms;
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
            CargarComboBox();
            this.dataSet1.leerXml();
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
            // Obtén el valor seleccionado en el ComboBox
            string valorFiltro = CmBox_Ced.SelectedItem.ToString();

            // Llama a un método que genera y muestra el informe con el filtro
            MostrarInformeConFiltro(valorFiltro);
        }

        private void MostrarInformeConFiltro(string valorFiltro)
        {
            // Configura el ReportViewer y establece el filtro
            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = "Reportes\\Inf_Compra_Cliente.rdlc"; 

            // Agrega parámetros al informe (puedes tener más según tus necesidades)
            ReportParameter[] parametros = new ReportParameter[1];
            parametros[0] = new ReportParameter("ParametroFiltro", valorFiltro);
            reportViewer1.LocalReport.SetParameters(parametros);

            // Actualiza y muestra el informe
            reportViewer1.RefreshReport();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        void CargarComboBox()
        {
            try
            {
                if (dataSet1 != null && dataSet1.Tables.Count > 0)
                {
                    DataTable tabla = dataSet1.Cliente;

                    foreach (DataRow fila in tabla.Rows)
                    {
                        CmBox_Ced.Items.Add(fila["cedula"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cédulas en el ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
