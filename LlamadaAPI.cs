using System.Text.Json.Serialization;
using System.Text.Json;

public class DatosRaiz
{
    [JsonPropertyName("results")]
    public List<Resultados> Resultados { get; set; }
}

public class Resultados
{
    [JsonPropertyName("name")]
    public string Nombre { get; set; }

    [JsonPropertyName("description")]
    public string Descripcion { get; set; }

}

public class Raiz
{
    public DatosRaiz data { get; set; }
}


