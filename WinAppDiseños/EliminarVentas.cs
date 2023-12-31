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
    public partial class EliminarVentas : Form
    {
        int codigoVerdura, codigoCliente;   
        public EliminarVentas()
        {
            InitializeComponent();
            txtCodigo.Focus();
        }
        private void EliminarVentas_Load(object sender, EventArgs e)
        {
            string ruta = AppDomain.CurrentDomain.BaseDirectory;
            dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRow[] vect1, vect2, vect3, vect4;
                vect1 = dataSet11.Factura.Select("codigoFact =" + txtCodigo.Text.ToString());
                vect2 = dataSet11.DetalleFact.Select("codigoFact =" + txtCodigo.Text.ToString());

                if (vect1.Length > 0 && vect2.Length > 0)
                {
                    codigoVerdura = Convert.ToInt32(vect1[0]["codigocli"].ToString());
                    codigoCliente = Convert.ToInt32(vect2[0]["codigover"].ToString());

                    vect3 = dataSet11.Cliente.Select("codigocli =" + codigoCliente);
                    vect4 = dataSet11.Verdura.Select("codigover =" + codigoVerdura);

                    cedula.Text = vect3[0]["cedula"].ToString();
                    nomCliente.Text = $"{vect3[0]["nombre"].ToString()} {vect3[0]["apellido"].ToString()}";

                    codigoVerd.Text = vect4[0]["codigover"].ToString();
                    nombreVer.Text = vect4[0]["nombre"].ToString();

                    lbCantidad.Text = vect2[0]["cantidad"].ToString();
                    lbPrecio.Text = vect2[0]["precio"].ToString();
                    lbFecha.Text = vect1[0]["fechacompra"].ToString();
                    lbSubtotal.Text = vect2[0]["subtotal"].ToString();
                    lbTotal.Text = vect1[0]["total"].ToString();
                }
                else
                {
                    MessageBox.Show("No se encontró el código de la verdura");
                    VaciarTextBox();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ingrese un código válido.");
                Console.WriteLine("---InputBuscar----" + ex.Message);
            }
        }

        private void BTN_Eliminar_Click(object sender, EventArgs e)
        {
            System.Data.DataRow[] vect, vect2;
            vect = dataSet11.Factura.Select("codigoFact =" + txtCodigo.Text.ToString());
            vect2 = dataSet11.DetalleFact.Select("codigoFact =" + txtCodigo.Text.ToString());
            
            if (vect.Length > 0 && vect2.Length > 0)
            {
                Console.WriteLine("---Eliminar---" + vect[0]["codigoFact"].ToString() + " Eliminado");

                dataSet11.Factura.Rows.Remove(vect[0]);
                dataSet11.DetalleFact.Rows.Remove(vect2[0]);

                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                dataSet11.WriteXml(Path.Combine(ruta, "Inventario.xml"));

                MessageBox.Show("Se ha eliminado la factura con éxito.");
                VaciarTextBox();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error.");
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void VaciarTextBox()
        {
            cedula.Text = "";
            nomCliente.Text = "";
            codigoVerd.Text = "";
            nombreVer.Text = "";
            lbCantidad.Text = "";
            lbPrecio.Text = "";
            lbFecha.Text = "";
            lbSubtotal.Text = "";
            lbTotal.Text = "";
        }




    }
}
