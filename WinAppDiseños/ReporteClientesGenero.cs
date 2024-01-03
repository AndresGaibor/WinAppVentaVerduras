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

namespace WinAppDiseños
{
    public partial class ReporteClientesGenero : Form
    {
        public ReporteClientesGenero()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ReporteClientesGenero_Load(object sender, EventArgs e)
        {
            this.dataSet1.leerXml();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }


        private void MostrarInformeConFiltro(string genero)
        {
            if (true)
            {
                // Get a filtered dataset based on the selected cedula
                DataSet filteredDataSet = GetFilteredDataSet(genero);

                // Configure the ReportViewer
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = Path.Combine(Archivos.rutaDebug, "../../Reportes/Inf_Venta_Genero.rdlc");


                // Set the data source for the report to the filtered dataset
                if (filteredDataSet.Tables.Count >= 2)
                {
                    // Set the data source for the report to the filtered datasets
                    ReportDataSource rds1 = new ReportDataSource("DataSet1", filteredDataSet.Tables["Factura"]);
                    ReportDataSource rds2 = new ReportDataSource("DataSet2", filteredDataSet.Tables["Cliente"]);
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

        private DataSet GetFilteredDataSet(string genero)
        {
            // Create a new DataSet to hold the filtered data
            DataSet filteredDataSet = new DataSet();

            try
            {
                // Filter the Cliente data
                DataTable originalTable1 = dataSet1.Tables["Cliente"];
                DataView view1 = new DataView(originalTable1);
                
                view1.RowFilter = $"genero = '({genero})'";

                DataTable filteredTable1 = view1.ToTable();
                filteredDataSet.Tables.Add(filteredTable1);



                // obtengo la lista de codigos de cliente que tiene el genero
                List<string> codigoscli = new List<string>();
                DataRow[] clientes = dataSet1.getClientesByGenero();

                foreach(DataRow cliente in clientes)
                {
                    codigoscli.Add(cliente["codigocli"].ToString());
                    
                }



                // Filter the Factura data
                DataTable originalTable2 = dataSet1.Tables["Factura"];
                DataView view2 = new DataView(originalTable2);

                string filterExpression = string.Join(" OR ", codigoscli.Select(c => $"codigocli = {c}"));
                view2.RowFilter = filterExpression;

                DataTable filteredTable2 = view2.ToTable();
                filteredDataSet.Tables.Add(filteredTable2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering dataset: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return filteredDataSet;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MostrarInformeConFiltro("M");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                MostrarInformeConFiltro("F");
            }
        }
    }
}
