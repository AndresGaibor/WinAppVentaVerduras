using Formulalrio_validation;
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
    public partial class RegistroUsuario : Form
    {
        string cedula, nombre, apellido, direccion, ciudad, email = "";
        int edad = 0;
        char estadoCivil = 'S', genero = 'M';

        public RegistroUsuario()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = AppDomain.CurrentDomain.BaseDirectory;

                dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));

                object[] vect = new object[11];



            }
            catch (Exception ex)
            {
                MessageBox.Show("Todos los campos deben ser válidos y llenados.");
                Console.WriteLine("---RegistroUsuario---"+ex.Message);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(char)Keys.Enter)
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
                        textBox2.Focus();
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show("Cédula no válida.");
                    Console.WriteLine("---IngresoCedula---" + ex.Message);
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

            }
        }
    }
}
