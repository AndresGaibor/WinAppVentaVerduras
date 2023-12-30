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
    public partial class Buscar_editar : Form
    {
        string nombre, dist;
        double precio;
        int stock,codigo;

        System.Data.DataRow[] vectRow, vectNew;

        public Buscar_editar()
        {
            InitializeComponent();
        }


        private void Buscar_editar_Load(object sender, EventArgs e)
        {
            txtCod.Focus();
            string ruta = AppDomain.CurrentDomain.BaseDirectory;
            dataSet11.ReadXml(Path.Combine(ruta, "Inventario.xml"));
            panel2.Visible = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                codigo = Convert.ToInt32(txtCod.Text.Trim());
                vectRow = dataSet11.Verdura.Select("codigover =" + codigo.ToString());

                if (vectRow.Length > 0)
                {
                    panel2.Visible = true;
                    txtNombre.Text = vectRow[0]["nombre"].ToString();
                    txtDist.Text = vectRow[0]["distribuidora"].ToString();
                    txtPrecio.Text = vectRow[0]["precio"].ToString();
                    nudStock.Value = Convert.ToInt32(vectRow[0]["stock"].ToString());
                    txtNombre.Focus();
                }
                else throw new Exception("No se ha encontrado el código de la verdura");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error del tipo: {ex.Message}");
                txtCod.Focus();
            }

        }

        private void txtCod_TextChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            lbNoti.Text = "";   
        }

        private void txtCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) btnBuscar_Click(sender, e);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                nombre = txtNombre.Text.Trim();
                if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("El nombre no puede estar vacío");

                dist = txtDist.Text.Trim();
                if (string.IsNullOrWhiteSpace(dist)) throw new Exception("La distribuidora no puede estar vacía");

                precio = double.Parse(txtPrecio.Text.Trim());
                if (precio <= 0) throw new Exception("El precio no puede ser menor o igual a 0");

                stock = Convert.ToInt32(nudStock.Value);
                if (stock < 0) throw new Exception("El stock no puede ser menor a 0");

                vectRow[0]["nombre"] = nombre;
                vectRow[0]["distribuidora"] = dist;
                vectRow[0]["precio"] = precio;
                vectRow[0]["stock"] = stock;

                vectRow[0].AcceptChanges();
                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                dataSet11.WriteXml(Path.Combine(ruta, "Inventario.xml"));
                lbNoti.Text = $"¡¡Verdura {vectRow[0]["codigover"]}, editada con éxito!!";
                txtCod.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error del tipo: {ex.Message}");

            }
        }

    }
}
