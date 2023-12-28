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
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataSet11.ReadXml("C:\\xml\\Inventario.xml");
            object[] vect = new object[5];
            vect[0] = null;
            vect[1] = textBox1.Text;
            vect[2] = textBox3.Text;
            vect[3] = Convert.ToDouble(textBox2.Text);
            vect[4] = textBox4.Text;
            dataSet11.Verdura.Rows.Add(vect);
            dataSet11.WriteXml("C:\\xml\\Inventario.xml");
            MessageBox.Show("Producto agregado correctamente");
            this.Close();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ingreso_Load(object sender, EventArgs e)
        {

        }
    }
}
