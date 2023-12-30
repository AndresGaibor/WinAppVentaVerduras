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

        private void calcularTotal()
        {
            int total = 0;
            foreach(DataGridViewRow fila in dGVDetalleFactura.Rows)
            {
                if (fila.Cells["Subtotal"].Value == null)
                {
                    continue;
                }

                total += Convert.ToInt32(fila.Cells["Subtotal"].Value);
            }

            this.lblTotal.Text = total.ToString();
        }

        private void calcularSubtotal(int fila)
        {
            if(fila < dGVDetalleFactura.Rows.Count - 1)
            {
                
                return;
            }

            DataGridViewCellCollection item = dGVDetalleFactura.Rows[fila].Cells;
            double cantidad = Convert.ToDouble(item["cantidad"].Value);
            double precio = Convert.ToDouble(item["precio"].Value);


            dGVDetalleFactura.Rows[fila].Cells["Subtotal"].Value = (double)cantidad * precio;
            calcularTotal();
        }

        private void dGVProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // valor de la primera columna de la fila seleccionada
            int filaSeleccionada = e.RowIndex;
            DataGridViewCellCollection producto = dGVProductos.Rows[filaSeleccionada].Cells;

            
            // revisa si la primera columna ya tiene un dato con ese codigo
            int enc = -1;
            foreach (DataGridViewRow fila in dGVDetalleFactura.Rows)
            {
                if (fila.Cells["Codigo"].Value == null)
                {
                    continue;
                }

                if (fila.Cells["Codigo"].Value.ToString() == producto["CodigoProducto"].Value.ToString())
                {
                    enc = fila.Index;
                    break;
                }
            }

            if(enc == -1)
            {
                this.dGVDetalleFactura.Rows.Add(producto["CodigoProducto"].Value, producto["NombreProducto"].Value, 1, producto["PrecioProducto"].Value);

                this.calcularSubtotal(dGVDetalleFactura.Rows.Count - 1);
                this.filaDetalle++;
            } else
            {
                int cantidadDetalle = Convert.ToInt32(dGVDetalleFactura.Rows[enc].Cells["cantidad"].Value);
                dGVDetalleFactura.Rows[enc].Cells["cantidad"].Value = cantidadDetalle + 1;
                this.calcularSubtotal(enc);
            }
        }

        private void Vender_Load(object sender, EventArgs e)
        {
            string ruta = AppDomain.CurrentDomain.BaseDirectory;
            dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));
            dGVProductos.DataSource = dataSet11.Tables["Verdura"];

            dGVProductos.Refresh();
        }

        private void dGVDetalleFactura_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex;
            if(fila == -1)
            {
                return;
            }
            // si es columna cantidad o precio
            DataGridViewCellCollection item = dGVDetalleFactura.Rows[fila].Cells;

            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                

                calcularSubtotal(fila);
            }
        }

        private void dGVDetalleFactura_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 2) // 1 should be your column index
            {
                int i;

                if (!int.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    MessageBox.Show("Ingrese solo numeros enteros");
                }
                else
                {
                    // the input is numeric 
                }
            }

            if (e.ColumnIndex == 3)
            {
                decimal i;
                string v = e.FormattedValue.ToString().Replace('.', ',');
                dGVDetalleFactura.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = v;

                if (!decimal.TryParse(v, out i))
                {
                    e.Cancel = true;
                    MessageBox.Show("Ingrese un numero");
                }
            }
        }
    }
}
