﻿using System;
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
    public partial class Buscar : Form
    {
        public Buscar()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ruta = AppDomain.CurrentDomain.BaseDirectory;
            dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml")); 

            System.Data.DataRow[] vect;
            vect = dataSet11.Verdura.Select("codigover =" + textBox1.Text.ToString());
            if(vect.Length > 0)
            {
                Mostrar muestra = new Mostrar(vect);
                muestra.Show();
            }
            else
            {
                MessageBox.Show("No se encontró el código de la verdura");
                textBox1.Clear();
            }
        }
    }
}
