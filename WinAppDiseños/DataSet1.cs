using System.Data;
using System.IO;
using System;

namespace WinAppDiseños
{


    partial class DataSet1
    {
        partial class VerduraDataTable
        {
        }

        string ruta = AppDomain.CurrentDomain.BaseDirectory;
        string archivoXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Inventario.xml");

        // cargar cuando se inicie el programa
        public void leerXml()
        {
            this.ReadXml(archivoXml);
        }

        public void guardarXml()
        {
            this.WriteXml(archivoXml);
        }

        public object[] getClienteByCedula(string cedula)
        {
            System.Data.DataRow[] vect;
            vect = this.Cliente.Select($"cedula = {cedula}");
            object[] res = new object[11];

            if (vect.Length > 0)
            {
                res[0] = vect[0]["cedula"].ToString();
                res[1] = vect[0]["nombre"].ToString();
                res[2] = vect[0]["apellido"].ToString();
                res[3] = vect[0]["fechaNac"].ToString();
                res[4] = vect[0]["direccion"].ToString();
                res[5] = vect[0]["ciudad"].ToString();
                res[6] = vect[0]["email"].ToString();
                res[7] = vect[0]["estadociv"].ToString();
                res[8] = vect[0]["genero"].ToString();
                res[9] = vect[0]["codigocli"].ToString();
                res[10] = vect[0]["edad"].ToString();

                return res;

            }
            else
            {
                return null;
            }
        }

        public object[] getUltimaFactura()
        {
            object[] res = new object[4];

            System.Data.DataRow[] vect;
            vect = this.Factura.Select("codigoFact = MAX(codigoFact)");

            if (vect.Length > 0)
            {
                res[0] = vect[0]["codigocli"].ToString();
                res[1] = vect[0]["total"].ToString();
                res[2] = vect[0]["codigoFact"].ToString();
                res[3] = vect[0]["fechacompra"].ToString();

                return res;
            }
            else
            {
                return null;
            }
        }

        public object[] getFacturaByCodigoFact(int codigo)
        {
            object[] res = new object[4];

            System.Data.DataRow[] vect;
            vect = this.Factura.Select($"codigoFact = {codigo}");

            if (vect.Length > 0)
            {
                res[0] = vect[0]["codigocli"].ToString();
                res[1] = vect[0]["total"].ToString();
                res[2] = vect[0]["codigoFact"].ToString();
                res[3] = vect[0]["fechacompra"].ToString();

                return res;
            }
            else
            {
                return null;
            }
        }

        public object[] getVerduraByCodigo(int codigo)
        {
            object[] res = new object[5];

            System.Data.DataRow[] vect;
            vect = this.Verdura.Select($"codigover = {codigo}");

            if (vect.Length > 0)
            {
                res[0] = vect[0]["codigover"].ToString();
                res[1] = vect[0]["nombre"].ToString();
                res[2] = vect[0]["distribuidora"].ToString();
                res[3] = vect[0]["precio"].ToString();
                res[4] = vect[0]["stock"].ToString();

                return res;
            }
            else
            {
                return null;
            }
        }

        public void generarFactura(object[] factura, object[][] detalle)
        {
            this.Factura.Rows.Add(factura);

            foreach (object[] item in detalle)
            {
                int codigoVerdura = Convert.ToInt32(item[1]);
                int cantidad = Convert.ToInt32(item[2]);

                // quita la cantidad de verduras del inventario
                int stockActual = Convert.ToInt32(this.Verdura.Select($"codigover = {codigoVerdura}")[0]["stock"]);
                this.Verdura.Select($"codigover = {codigoVerdura}")[0]["stock"] = stockActual - cantidad;
                this.Verdura.AcceptChanges();

                this.DetalleFact.Rows.Add(item);
            }

            this.guardarXml();
        }
    }
}
