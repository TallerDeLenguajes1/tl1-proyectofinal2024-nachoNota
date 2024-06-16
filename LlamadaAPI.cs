using System.Text.Json.Serialization;
using System.Text.Json;

namespace LlamadaAPI
{
    public class DatosRaiz
    {
        [JsonPropertyName("results")]
        public List<DatosPersonaje> Resultados { get; set; }
    }

    public class DatosPersonaje
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

    public class APICLIENT
    {
        public static async Task<Raiz> GetCharactersAsync()
        {
            string publicKey = "650b489211f65652098aedd5afbb79bf";
            string hash = "abde502c3b8786278606247295cf4767";
            string baseUrl = "https://gateway.marvel.com:443/v1/public/";

            try
            {
                HttpClient client = new HttpClient();

                string url = $"{baseUrl}characters?comics=32477&limit=13&offset=1&ts=1&apikey={publicKey}&hash={hash}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var personajes = JsonSerializer.Deserialize<Raiz>(responseBody);
                return personajes;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Lo sentimos, se ha producido un error inesperado, intente de nuevo mas tarde. Gracias!");
                Console.WriteLine("Message :{0}", e.Message);
                return null;
            }
        }
    }


}

