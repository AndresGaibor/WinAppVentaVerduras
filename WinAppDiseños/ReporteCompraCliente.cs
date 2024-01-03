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
    public partial class ReporteCompraCliente : Form
    {
        string cedula = null;
        public ReporteCompraCliente(string nrCedula = null)
        {
            InitializeComponent();
            cedula = nrCedula;
        }

        private void ReporteCompraCliente_Load(object sender, EventArgs e)
        {
            CargarComboBox();
            this.dataSet1.leerXml();

            //ReportParameter PrmInvoiceNo = new ReportParameter("cedulacliente");
            //PrmInvoiceNo.Values.Add("");
            //this.reportViewer1.LocalReport.SetParameters(PrmInvoiceNo);


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
            try {
                cedula = (string)CmBox_Ced.Text;


                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cédulas en el ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            // Llama a un método que genera y muestra el informe con el filtro



            MostrarInformeConFiltro();
        }

        private void MostrarInformeConFiltro()
        {
            if (cedula != null && cedula.Length == 10)
            {
                // Get a filtered dataset based on the selected cedula
                DataSet filteredDataSet = GetFilteredDataSet(cedula);

                // Configure the ReportViewer
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = Path.Combine(Archivos.rutaDebug, "../../Reportes/Inf_Compra_Cliente.rdlc");
                

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

        private DataSet GetFilteredDataSet(string cedula)
        {
            // Create a new DataSet to hold the filtered data
            DataSet filteredDataSet = new DataSet();

            try
            {
                // Filter the Cliente data
                DataTable originalTable1 = dataSet1.Tables["Cliente"];
                DataView view1 = new DataView(originalTable1);
                view1.RowFilter = $"cedula = '{cedula}'";  // Adjust the filter to match your dataset's structure
                DataTable filteredTable1 = view1.ToTable();
                filteredDataSet.Tables.Add(filteredTable1);

                // Filter the Factura data
                DataTable originalTable2 = dataSet1.Tables["Factura"];
                DataView view2 = new DataView(originalTable2);
                object[] cliente = dataSet1.getClienteByCedula(cedula);
                if (cliente == null)
                {
                    MessageBox.Show("Cliente not found.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return filteredDataSet;
                }
                view2.RowFilter = $"codigocli = {cliente[9]}";  // Adjust the filter to match your dataset's structure
                DataTable filteredTable2 = view2.ToTable();
                filteredDataSet.Tables.Add(filteredTable2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering dataset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return filteredDataSet;
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

                    //CmBox_Ced.Items.Add("Seleccione una cedula");

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
