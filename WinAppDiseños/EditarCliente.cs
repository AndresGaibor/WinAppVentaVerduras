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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinAppDiseños
{
    public partial class EditarCliente : Form
    {
        DataRow[] datos;
        string cedula, nombre, apellido, direccion, ciudad, email = "";
        char estadoCivil = 'S', genero = 'M';
        int edadEnAnios;

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = AppDomain.CurrentDomain.BaseDirectory;

                //object[] vect = new object[11];

                //
                LeerCedula();
                nombre = txtNombre.Text.Trim();
                apellido = txtApellido.Text.Trim();
                ciudad = txtCiudad.SelectedItem.ToString();
                email = txtEmail.Text.Trim();
                direccion = txtDireccion.Text.Trim();
                genero =Convert.ToChar( txtGenero.Text);
                estadoCivil = Convert.ToChar( txtEstado.Text);
                LeerFechaNac();
                //

                
                
                if (datos.Length > 1)
                {
                    MessageBox.Show("Ya existe un cliente con esa cédula.");
                    Console.WriteLine("---Ingreso--- Cédula repetida" + cedula);
                    return;
                }
                else if (edadEnAnios < 13)
                {
                    MessageBox.Show("La edad mínima debe ser 13 años.");
                    Console.WriteLine("---Ingreso--- Edad no válida" + edadEnAnios);
                    return;
                }
                else
                {

                    datos[0]["cedula"] = cedula;
                    datos[0]["nombre"] = nombre;
                    datos[0]["apellido"] = apellido;
                    datos[0]["fechaNac"] = fechaNac;
                    datos[0]["direccion"] = direccion;
                    datos[0]["ciudad"] = ciudad;
                    datos[0]["email"] = email;
                    datos[0]["estadociv"] = estadoCivil;
                    datos[0]["genero"] = genero;
                    //vect[9] = null;
                    datos[0]["edad"] = edadEnAnios;

                    datos[0].AcceptChanges();

                    MessageBox.Show("Cliente Editado con éxito.");
                    Console.WriteLine("---EditadoUsuario--- Cliente Editado con éxito.");
                    dataSet11.WriteXml(Path.Combine(ruta, "Inventario.xml"));

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Todos los campos deben ser válidos y llenados.");
                Console.WriteLine("---RegistroUsuario---" + ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LeerFechaNac();
        }

        DateTime fechaNac;

        public EditarCliente()
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

                datos = dataSet11.Cliente.Select("cedula =" + cedula);
                if (datos.Length > 0)
                {
                    //cargar datos para ser modificados
                    dateTimePicker1.Value = Convert.ToDateTime( datos[0]["fechaNac"]);

                    txtNombre.Text = datos[0]["nombre"].ToString();
                    txtCedula.Text = datos[0]["cedula"].ToString();
                    txtApellido.Text = datos[0]["apellido"].ToString();
                    
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        void LeerFechaNac()
        {
            fechaNac = dateTimePicker1.Value.Date;
            DateTime fechaActual = DateTime.Today;
            TimeSpan diferencia = fechaActual - fechaNac;

            edadEnAnios = (int)(diferencia.TotalDays / 365.25);

            txtEdad.Text = edadEnAnios.ToString();

        }
        void LeerCedula()
        {
            try
            {
                Cedula comprCedula = new Cedula(textBox1.Text);
                if (!comprCedula.ComprobarCedula())
                {
                    MessageBox.Show("Cédula inválida.");
                    textBox1.Clear();
                    textBox1.Focus();
                }
                else
                {
                    cedula = textBox1.Text.Trim();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cédula no válida.");
                Console.WriteLine("---IngresoCedula---" + ex.Message);
            }
        }
    }
}
