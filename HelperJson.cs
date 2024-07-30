using System;
using System.IO;
using System.Text.Json;
using MiProyecto.FabricaDePersonajes;

public class HelperJson
{
    public void GuardarCampeon(string nombreArchivo, Personaje campeon)
    {
        var listaCampeones = new List<Personaje>();

        campeon.Datos.FechaCampeon = DateTime.Now;

        if (!File.Exists(nombreArchivo))
        {
            listaCampeones.Add(campeon);
            string stringJson = JsonSerializer.Serialize(listaCampeones);
            GuardarArchivo(nombreArchivo, stringJson);
        }
        else
        {
            string recuperadoJson = AbrirArchivo(nombreArchivo);

            var listaRecuperada = JsonSerializer.Deserialize<List<Personaje>>(recuperadoJson);

            listaRecuperada.Add(campeon);

            string stringJsonNuevo = JsonSerializer.Serialize(listaRecuperada);

            GuardarArchivo(nombreArchivo, stringJsonNuevo);
        }
    }

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