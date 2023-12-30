using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinAppDiseños
{
    public partial class Vender : Form
    {
        public Vender()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        int codigoCliente = 0; // consumidor final

        object[] detalleFactura;

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            string ruta = Archivos.rutaDebug;

            // datos de la factura
            DateTime fecha = dTPFecha.Value.Date;
            string secuenciaStr = txtSecuencia.Text;

            // codigo de verdura, cantidad, precio
           

            try
            {
                /**
                dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));
                object[] vect = new object[5];
                vect[0] = null;
                vect[1] = textBox1.Text.Trim();
                vect[2] = textBox3.Text.Trim();
                vect[3] = Convert.ToDouble(textBox2.Text.Replace('.', ','));
                vect[4] = textBox4.Text.Trim();
                dataSet11.Verdura.Rows.Add(vect);
                dataSet11.WriteXml(Path.Combine(ruta, "Inventario.xml"));
                MessageBox.Show("Producto agregado correctamente");
                this.Close();
                */
            }
            catch (Exception ex)
            {

                Console.WriteLine("--Facturar--" + ex.Message);
                Console.WriteLine(ex);
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                string cedulaStr = txtCliente.Text.Trim();

                if(cedulaStr.Length == 0)
                {
                    this.lblCliente.Text = "CONSUMIDOR FINAL";
                    codigoCliente = 0;

                    return;
                }

                if(cedulaStr.Length != 10 && cedulaStr.Length != 0)
                {
                    MessageBox.Show("Cédula incorrecta");
                    codigoCliente = 0;
                    this.txtCliente.Clear();
                    this.lblCliente.Text = "CONSUMIDOR FINAL";
                    return;
                }

                Cedula validador = new Cedula(cedulaStr);
                if(!validador.ComprobarCedula())
                {
                    codigoCliente = 0;
                    MessageBox.Show("Cédula incorrecta");
                    this.txtCliente.Clear();
                    this.lblCliente.Text = "CONSUMIDOR FINAL";
                    return;
                }

                // buscar al cliente

                // si existe renombrar el label cliente

                // si no existe llamar a la clase nuevo cliente
            }
        }
        int filaDetalle = 0;

        private void dGVProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // valor de la primera columna de la fila seleccionada
            int filaSeleccionada = e.RowIndex;
            DataGridViewCellCollection producto = dGVProductos.Rows[filaSeleccionada].Cells;

            this.dGVDetalleFactura.Rows.Add(producto["CodigoProducto"].Value, producto["NombreProducto"].Value, 1, producto["PrecioProducto"].Value);

            // revisa si la primera columna ya tiene un dato con ese codigo
            int enc = -1;
            foreach (DataGridViewRow fila in dGVDetalleFactura.Rows)
            {
                if (fila.Cells[0].Value.ToString() == producto["CodigoProducto"].Value.ToString())
                {
                    enc = fila.Index;
                    break;
                }
            }
        }

        private void Vender_Load(object sender, EventArgs e)
        {
            string ruta = AppDomain.CurrentDomain.BaseDirectory;
            dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));
            dGVProductos.DataSource = dataSet11.Tables["Verdura"];

            dGVProductos.Refresh();

        }
    }
}
