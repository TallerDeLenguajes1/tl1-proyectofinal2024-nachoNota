using System;
using System.IO;
using System.Text.Json;

public class HelperJson
{
    public void GuardarArchivo(string nombreArchivo, string datos)
    {
        using (FileStream archivo = new FileStream(nombreArchivo, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(archivo))
            {
                sw.WriteLine(datos);
                sw.Close();
            }
        }
    }

    public string AbrirArchivo(string nombreArchivo)
    {
        string linea;
        using (FileStream archivo = new FileStream(nombreArchivo, FileMode.Open))
        {
            using (StreamReader sr = new StreamReader(archivo))
            {
                linea = sr.ReadToEnd();
                archivo.Close();
            }
        }
        return linea;
    }
}