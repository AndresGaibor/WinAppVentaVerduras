using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDiseños.Properties;

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
            this.dataSet1.leerXml();

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Obtén la fecha seleccionada
            DateTime fechaSeleccionada = dateTimePicker1.Value;

            // Llama a un método que carga y muestra el informe con la fecha seleccionada
            //CargarInforme(fechaSeleccionada);

            MostrarInformeConFiltro(fechaSeleccionada);
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

        private void MostrarInformeConFiltro(DateTime fechacompra)
        {
            if (true)
            {
                // Get a filtered dataset based on the selected cedula
                DataSet filteredDataSet = GetFilteredDataSet(fechacompra);

                // Configure the ReportViewer
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = Path.Combine(Archivos.rutaDebug, "../../Reportes/Informe_VentasporFecha.rdlc");


                // Set the data source for the report to the filtered dataset
                if (filteredDataSet.Tables.Count >= 2)
                {
                    // Set the data source for the report to the filtered datasets
                    ReportDataSource rds1 = new ReportDataSource("DataSet1", filteredDataSet.Tables["Cliente"]);
                    ReportDataSource rds2 = new ReportDataSource("DataSet2", filteredDataSet.Tables["Factura"]);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds1);
                    reportViewer1.LocalReport.DataSources.Add(rds2);

                    // Refresh the report
                    reportViewer1.RefreshReport();
                }
                else
                {
                    MessageBox.Show("The filtered dataset does not contain the expected number of tables.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

        }

        private DataSet GetFilteredDataSet(DateTime fechacompra)
        {
            // Create a new DataSet to hold the filtered data
            DataSet filteredDataSet = new DataSet();

            try
            {
                DataRow[] facturas = dataSet1.getFacturaByFecha(fechacompra.ToString());
                List<string> busqueda = new List<string>();
                foreach (DataRow factura in facturas)
                {
                    busqueda.Add(factura["codigocli"].ToString());
                }
                string[] strings = busqueda.ToArray();
                // Filter the Cliente data
                DataTable originalTable1 = dataSet1.Tables["Cliente"];
                DataView view1 = new DataView(originalTable1);
                string joined = string.Join(",", busqueda);
                string filterExpression = string.Join(" OR ", busqueda.Select(c => $"codigocli = {c}"));
                view1.RowFilter = filterExpression;
                //view1.RowFilter = $"codigocli IN ({joined})";  // Adjust the filter to match your dataset's structure
                
                DataTable filteredTable1 = view1.ToTable();
                //DataTable ab = dataSet1.Cliente.Where(c =>
                //{ return busqueda.Contains(c.codigocli.ToString()); }).CopyToDataTable();
            
                //filteredDataSet.Tables.Add(ab);
                filteredDataSet.Tables.Add(filteredTable1);

                // Filter the Factura data
                DataTable originalTable2 = dataSet1.Tables["Factura"];
                DataView view2 = new DataView(originalTable2);
                
                view2.RowFilter = $"fechacompra = '{fechacompra.ToString()}'";  // Adjust the filter to match your dataset's structure
                DataTable filteredTable2 = view2.ToTable();
                filteredDataSet.Tables.Add(filteredTable2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering dataset: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return filteredDataSet;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
