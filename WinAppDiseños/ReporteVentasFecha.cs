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
    public partial class ReporteVentasFecha : Form
    {
        public ReporteVentasFecha()
        {
            InitializeComponent();
        }

        private void ReporteVentasFecha_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Obtén la fecha seleccionada
            DateTime fechaSeleccionada = dateTimePicker1.Value;

            // Llama a un método que carga y muestra el informe con la fecha seleccionada
            CargarInforme(fechaSeleccionada);
        }

        private void CargarInforme(DateTime fecha)
        {
            // Configura el ReportViewer y establece el parámetro de fecha
            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = "Reportes\\Inf_Ventas_Fecha.rdlc"; // Reemplaza con la ruta de tu informe

            // Crea un parámetro y asigna la fecha
            ReportParameter parametroFecha = new ReportParameter("FechaP", fecha.ToString("yyyy-MM-dd"));
            reportViewer1.LocalReport.SetParameters(parametroFecha);

            // Actualiza y muestra el informe
            reportViewer1.RefreshReport();
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
