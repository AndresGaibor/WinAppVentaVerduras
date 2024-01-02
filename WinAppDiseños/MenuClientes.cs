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
    public partial class MenuClientes : Form
    {
        public MenuClientes()
        {
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RegistroUsuario registroUsuario = new RegistroUsuario();
            registroUsuario.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Buscar_Cliente buscar_Cliente = new Buscar_Cliente();
            buscar_Cliente.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            EditarCliente editarCliente = new EditarCliente();
            editarCliente.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            EliminarCliente eliminarCliente = new EliminarCliente();
            eliminarCliente.ShowDialog();
        }

        private void MenuClientes_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }
    }
}
