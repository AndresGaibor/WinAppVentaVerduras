using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppDiseños
{
    public partial class Ingreso : Form
    {
        public Ingreso()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Filtrar para mostrar solo archivos de imagen
            openFileDialog1.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";

            // Mostrar el diálogo para seleccionar archivos
            DialogResult result = openFileDialog1.ShowDialog();

            // Verificar si se seleccionó un archivo
            if (result == DialogResult.OK)
            {
                // Obtener la ruta del archivo seleccionado y asignarlo a un PictureBox o usarlo según tus necesidades
                string rutaImagen = openFileDialog1.FileName;
                // Ejemplo de cómo podrías mostrar la imagen en un control PictureBox
                pictureBox1.ImageLocation = rutaImagen;
            }
            MessageBox.Show("Imagen caragada correctamente");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Producto agregado correctamente");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
