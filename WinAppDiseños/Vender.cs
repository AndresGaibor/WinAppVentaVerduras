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
        int secuencia = 1;
        object[] detalleFactura;

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            string ruta = Archivos.rutaDebug;

            // datos de la factura
            DateTime fecha = dTPFecha.Value.Date;

            if(this.dGVDetalleFactura.RowCount == 0)
            {
                MessageBox.Show("No hay productos en la factura");
                return;
            }

            if(double.Parse(lblTotal.Text) > 50 && codigoCliente == 0)
            {
                MessageBox.Show("Facturas mayores a 50 dolares es obligatorio con datos");
                this.txtCliente.Focus();
                return;
            }

            // codigo de verdura, cantidad, precio
           

            try
            {
                //this.dataSet11.leerXml();
                object[][] vect = new object[dGVDetalleFactura.Rows.Count][];
                
                foreach(DataGridViewRow fila in dGVDetalleFactura.Rows)
                {
                    if (fila.Cells["Codigo"].Value == null)
                    {
                        continue;
                    }

                    vect[fila.Index] = new object[5];
                    vect[fila.Index][0] = this.secuencia;
                    vect[fila.Index][1] = fila.Cells["Codigo"].Value;
                    vect[fila.Index][2] = fila.Cells["cantidad"].Value;
                    vect[fila.Index][3] = fila.Cells["precio"].Value;
                    vect[fila.Index][4] = fila.Cells["Subtotal"].Value;
                }

                object[] factura = new object[4];
                factura[0] = this.codigoCliente;
                factura[1] = Convert.ToDouble(lblTotal.Text);
                factura[2] = this.secuencia;
                factura[3] = fecha;

                dataSet11.generarFactura(factura, vect);
                
                MessageBox.Show("Factura agregado correctamente");
                this.Close();
                
            }
            catch (Exception ex)
            {

               MessageBox.Show("Error al agregar factura " + ex.Message);
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

                object[] cliente = dataSet11.getClienteByCedula(cedulaStr);

                if(cliente != null)
                {
                    this.codigoCliente = Convert.ToInt32(cliente[9]);
                    this.lblCliente.Text = cliente[1].ToString() + " " + cliente[2].ToString();
                } else
                {
                    RegistroUsuario rg = new RegistroUsuario();
                    if(rg.ShowDialog() == DialogResult.OK)
                    {
                        object[] cliente2 = dataSet11.getClienteByCedula(cedulaStr);

                        if (cliente2 != null)
                        {
                            this.codigoCliente = Convert.ToInt32(cliente2[9]);
                            this.lblCliente.Text = cliente2[1].ToString() + " " + cliente2[2].ToString();
                        } else
                        {
                            codigoCliente = 0;
                            this.txtCliente.Clear();
                            this.lblCliente.Text = "CONSUMIDOR FINAL";
                        }
                    } else
                    {
                        codigoCliente = 0;
                        this.txtCliente.Clear();
                        this.lblCliente.Text = "CONSUMIDOR FINAL";
                    }
                }


                // si no existe llamar a la clase nuevo cliente
            }
        }
        int filaDetalle = 0;

        private void calcularTotal()
        {
            double total = 0;
            foreach(DataGridViewRow fila in dGVDetalleFactura.Rows)
            {
                if (fila.Cells["Subtotal"].Value == null)
                {
                    continue;
                }

                total += Convert.ToDouble(fila.Cells["Subtotal"].Value);
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

            object[] ultimaFactura = dataSet11.getUltimaFactura();

            if(ultimaFactura != null)
            {
                this.txtSecuencia.Text = (Convert.ToInt32(ultimaFactura[2]) + 1).ToString();
                this.secuencia = Convert.ToInt32(ultimaFactura[2]) + 1;
            }
            
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
                    object[] producto = dataSet11.getVerduraByCodigo(Convert.ToInt32(this.dGVDetalleFactura.Rows[e.RowIndex].Cells["Codigo"].Value.ToString())); 
                    if(producto != null && int.Parse(producto[4].ToString()) < i)
                    {
                        e.Cancel = true;
                        MessageBox.Show("No existe esa cantidad");
                    }
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

        private void txtSecuencia_Validating(object sender, CancelEventArgs e)
        {
            int codigo = Convert.ToInt32(txtSecuencia.Text);
            
            object[] factura = dataSet11.getFacturaByCodigoFact(codigo);
            if (factura != null)
            {
                   MessageBox.Show("Ya existe una factura con ese código");
                   txtSecuencia.Clear();
            }
        }


        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if(dGVDetalleFactura.CurrentRow == null)
            {
                return;
            }

            int filaSeleccionada = dGVDetalleFactura.CurrentRow.Index;
            if(filaSeleccionada == -1)
            {
                return;
            }

            dGVDetalleFactura.Rows.RemoveAt(filaSeleccionada);
        }
    }
}
