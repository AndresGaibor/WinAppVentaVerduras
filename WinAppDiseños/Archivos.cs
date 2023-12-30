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
        public string rutaDebug =  AppDomain.CurrentDomain.BaseDirectory;
        public void crearArchivos(string[] nombres)
        {
               for (int i = 0; i < nombres.Length; i++)
            {
                string nombre = Path.Combine(rutaDebug, nombres[i]);


                // Verifica si el archivo ya existe
                if (!File.Exists(nombre))
                {
                    // El archivo no existe, así que créalo
                    using (FileStream fs = File.Create(nombre))
                    {
                        // Puedes agregar contenido al archivo aquí si lo deseas
                        File.Create(nombre).Close();
                    }
                }
                
            }
        }
    }
}
