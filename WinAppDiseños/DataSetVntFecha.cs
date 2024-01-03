using System.IO;
using System;

namespace WinAppDiseños
{


    partial class DataSetVntFecha
    {
        string ruta = AppDomain.CurrentDomain.BaseDirectory;
        string archivoXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpt_vnt_fecha.xml");



        // cargar cuando se inicie el programa
        public void leerXml()
        {
            this.ReadXml(archivoXml);
        }
        public void guardarXml()
        {
            this.WriteXml(archivoXml);
        }



    }
}
