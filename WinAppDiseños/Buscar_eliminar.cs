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
    public partial class Buscar_eliminar : Form
    {
        public Buscar_eliminar()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            try
            {

                System.Data.DataRow[] vect;
                vect = dataSet11.Verdura.Select("codigover =" + textBox1.Text.ToString().Trim());

                if (vect.Length > 0)
                {
                    nombre.Text = vect[0]["nombre"].ToString();
                    distribuidora.Text = vect[0]["distribuidora"].ToString();
                    precio.Text = vect[0]["precio"].ToString();
                    stock.Text = vect[0]["stock"].ToString();

                    panel2.Visible = true;
                }
                else
                {
                    MessageBox.Show("No se encontró el código de la verdura");
                    textBox1.Clear();
                    nombre.Text = "";
                    distribuidora.Text = "";
                    precio.Text = "";
                    stock.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese un código válido.");
                Console.WriteLine("---InputEliminar----" +ex.Message);
            }
        }

        private void Buscar_eliminar_Load(object sender, EventArgs e)
        {
            string ruta = AppDomain.CurrentDomain.BaseDirectory;
            dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));
            panel2.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
                button1_Click(sender, e);
        }

        private void BTN_Eliminar_Click(object sender, EventArgs e)
        {
            System.Data.DataRow[] vect;
            vect = dataSet11.Verdura.Select("codigover =" + textBox1.Text.ToString());

            if( vect.Length > 0 )
            {
                EliminarRegistro(vect[0]);

                MessageBox.Show("Se ha eliminado la verdura con éxito.");
                textBox1.Clear();
                nombre.Text = "";
                distribuidora.Text = "";
                precio.Text = "";
                stock.Text = "";
                panel2 .Visible = false;
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error.");
            }
            
        }

        private void EliminarRegistro (DataRow elim)
        {
            if (elim == null)
            {
                return;
            }
            else
            {
                Console.WriteLine("---Eliminar---" + elim["codigover"].ToString() +" Eliminado");
                dataSet11.Verdura.Rows.Remove(elim);

                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                dataSet11.WriteXml(Path.Combine(ruta, "Inventario.xml"));
                
            }
        }

    }
}
