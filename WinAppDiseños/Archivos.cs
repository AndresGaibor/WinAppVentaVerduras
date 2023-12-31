using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDiseños
{
    internal class Archivos
    {
        public static string rutaDebug =  AppDomain.CurrentDomain.BaseDirectory;
        public void crearArchivoXml()
        {
            string archivo = Path.Combine(rutaDebug, "Inventario.xml");

            // Verifica si el archivo ya existe
            if (!File.Exists(archivo))
            {
                    // Puedes agregar contenido al archivo aquí si lo deseas
                    File.Create(archivo).Close();
            }
                
            
        }
    }
}
