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
    public partial class Buscar_Cliente : Form
    {
        public Buscar_Cliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                panel2.Visible = false;
                string ruta = AppDomain.CurrentDomain.BaseDirectory;

                dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));

                string cedula = textBox1.Text.Trim();

                DataRow[] datos;
                datos = dataSet11.Cliente.Select("cedula =" + cedula);
                if (datos.Length > 0)
                {
                    txtNombre.Text = datos[0]["nombre"].ToString();
                    txtCedula.Text = datos[0]["cedula"].ToString();
                    txtApellido.Text = datos[0]["apellido"].ToString();
                    txtFechaNac.Text = datos[0]["fechaNac"].ToString();
                    //
                    Console.WriteLine("El tipo de dato de la fecha es: " + datos[0]["fechaNac"].GetType());
                    //
                    txtDireccion.Text = datos[0]["direccion"].ToString();
                    txtCiudad.Text = datos[0]["ciudad"].ToString();
                    txtEmail.Text = datos[0]["email"].ToString();
                    txtEstado.Text = datos[0]["estadociv"].ToString();
                    txtGenero.Text = datos[0]["genero"].ToString();
                    txtEdad.Text = datos[0]["edad"].ToString();
                    txtCodigo.Text = datos[0]["codigocli"].ToString();
                    
                    Console.WriteLine("---Ingreso--- Cédula Encontrada" + cedula);
                    panel2.Visible = true;
                }
                else
                {
                    MessageBox.Show("No se ha encontrado un cliente con esa cédula");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error.");
                Console.WriteLine("---BuscarCliente---" + ex.Message);
                textBox1.Clear();

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter) { 
                button1_Click(sender, e);
            }
        }
    }
}
