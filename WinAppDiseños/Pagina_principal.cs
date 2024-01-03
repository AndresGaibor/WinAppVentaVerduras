using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppDiseños
{
    public partial class Pagina_principal : Form
    {
        string bienvenida = "Bienvenido";
        string espacio = "  ";

        public Pagina_principal()
        {
            InitializeComponent();
        }

        private void Pagina_principal_Load(object sender, EventArgs e)
        {
            Archivos archivos = new Archivos();
            archivos.crearArchivoXml();
        }
        public void RecibirInformacion(string informacion)
        {
            string nombre = informacion;
            label1.Text = bienvenida + espacio + nombre;
        }
        //Animaciones de las imaganes del menú
        //Opción 1

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(width: 144, height: 131);
            panel2.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(width: 124, height: 121);
            panel2.Visible = false;
        }

        //Opción2


        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(width: 144, height: 131);
            panel3.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(width: 124, height: 111);
            panel3.Visible = false;
        }

        //Opcion3

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(width: 144, height: 131);
            panel4.Visible = true;  
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(width: 124, height: 111);
            panel4.Visible= false;  
        }
        //Opcion4
        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(width: 144, height: 131);
            panel7.Visible = true;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(width: 124, height: 111);
            panel7.Visible = false;
        }

        //Opcion5
        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(width: 144, height: 131);
            panel5.Visible = true;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(width: 124, height: 111);
            panel5.Visible = false; 
        }

        //LLamo al forms donde esta las opciones de productos(verduras)
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MenuProductos llamar = new MenuProductos();
            llamar.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MenuVentas mv = new MenuVentas();
            mv.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MenuClientes mc = new MenuClientes();
            mc.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Reportes rp = new Reportes();
            rp.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string rutaCarpetaDebug = Application.StartupPath;
            string rutaCarpetaImgDebug = Path.Combine(rutaCarpetaDebug, "ACERCA DE ARCHIVOS2_archivos"); // Ruta de la carpeta de imágenes

            // Combinar la ruta de la carpeta debug con el nombre del archivo HTML
            string rutaArchivoHTML = Path.Combine(rutaCarpetaDebug, "ACERCA DE ARCHIVOS2.htm");

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(rutaArchivoHTML);
                startInfo.WorkingDirectory = rutaCarpetaImgDebug;
                Process.Start(startInfo); // Iniciar el proceso con la información proporcionada
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el archivo HTML: " + ex.Message);
            }
        }
    }
}
