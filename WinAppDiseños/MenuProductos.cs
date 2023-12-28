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
    public partial class MenuProductos : Form
    {
        public MenuProductos()
        {
            InitializeComponent();
        }

        private void MenuProductos_Load(object sender, EventArgs e)
        {

        }

        //Menu de Opciones, hace que aparezca un panel verde cada que pasa el mouse por encima y desaperece cuando no esta el mouse
        //Opcion de Ingresar
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        //Opcion de buscar

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
        //Opcion de editar
        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }
        //Opcion de eliminar
        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
           
        }

    }
}
