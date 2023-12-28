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
    public partial class Mostrar : Form
    {
        public Mostrar(System.Data.DataRow[] vect1)
        {
            InitializeComponent();
            codigo1.Text = vect1[0]["codigover"].ToString();
            nombre.Text = vect1[0]["nombre"].ToString();
            distribuidora.Text = vect1[0]["distribuidora"].ToString();
            precio.Text = vect1[0]["precio"].ToString();
            stock.Text = vect1[0]["stock"].ToString();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Mostrar_Load(object sender, EventArgs e)
        {

        }
    }
}
