using System;
using System.IO;
using System.Text.Json;
using DatosPersonaje;
using CaracteristicasPersonaje;

namespace MiProyecto.FabricaDePersonajes
{
    public class FabricaDePersonajes
    {
        public Personaje CrearPersonaje(Datos datos){
            var caracteristicas = new Caracteristicas();
            var NuevoPersonaje = new Personaje(datos, caracteristicas);
            return NuevoPersonaje;
        }

        public async Task<List<Personaje>> CrearListaPersonajes()
        {
            Raiz listaNombreDescripciones = await APICLIENT.GetCharactersAsync();

            if(listaNombreDescripciones != null)
            {  
                return ListaCreadaApi(listaNombreDescripciones);
            }
            else
            {
                return ListaCreadaSinApi();
            }
        }

        public List<Personaje> ListaCreadaApi(Raiz raiz)
        {
            var listaPersonajes = new List<Personaje>();
            foreach (var Personaje in raiz.data.Resultados)
            {
                if (!string.IsNullOrEmpty(Personaje.Descripcion))
                {
                    var PersonajeCreado = CrearPersonaje(new Datos(Personaje.Nombre, Personaje.Descripcion));
                    PersonajeCreado.Caracteristicas.BalancearEstadisticas();
                    listaPersonajes.Add(PersonajeCreado);
                }
            }
            return listaPersonajes;
        }

        public List<Personaje> ListaCreadaSinApi()
        {
            var listaPersonajes = new List<Personaje>();
            var helperJson = new HelperJson();
            string nombreArchivo = "ListadoPersonajes.json";
        
            if (!File.Exists(nombreArchivo))
            {
                CrearArchivoDatosPersonajes(nombreArchivo);
            }

            string stringJson = helperJson.AbrirArchivo(nombreArchivo);

            var listaDatos = JsonSerializer.Deserialize<List<Datos>>(stringJson);

            foreach(var datos in listaDatos)
            {
                var nuevoPersonaje = CrearPersonaje(datos);
                nuevoPersonaje.Caracteristicas.BalancearEstadisticas();
                listaPersonajes.Add(nuevoPersonaje);
            }

            return listaPersonajes;
        }

        public void CrearArchivoDatosPersonajes(string nombreArchivo)
        {
            var listaDatos = new List<Datos>();
            var helperJson = new HelperJson();   
            listaDatos.Add(new Datos("Daredevil", "When Matt Murdock saved a man from an oncoming truck, it spilled a radioactive cargo that rendered Matt blind while enhancing his remaining senses. Under the harsh tutelage of blind martial arts master Stick, Matt mastered his heightened senses and became a formidable fighter."));
            listaDatos.Add(new Datos("Iron Man", "Wounded, captured and forced to build a weapon by his enemies, billionaire industrialist Tony Stark instead created an advanced suit of armor to save his life and escape captivity. Now with a new outlook on life, Tony uses his money and intelligence to make the world a safer, better place as Iron Man."));
            listaDatos.Add(new Datos("Black Panther", "T'Challa, the Black Panther, is the king of Wakanda, a highly advanced African nation. He possesses enhanced abilities given to him by the heart-shaped herb and wears a vibranium suit that makes him a formidable warrior and protector of his people."));
            listaDatos.Add(new Datos("Doctor Strange", "Once a brilliant but arrogant neurosurgeon, Stephen Strange's life changed forever after a car accident robbed him of the use of his hands. Searching for healing, he discovered the hidden world of magic and alternate dimensions, becoming the Sorcerer Supreme and the protector of Earth against mystical threats."));
            listaDatos.Add(new Datos("Rocket Raccoon", "A genetically-engineered raccoon with advanced engineering skills and a knack for high-tech weaponry. Known for his sharp wit and fiery personality, Rocket is crucial to the Guardians of the Galaxy for his expertise in gadgets and weaponry."));
            listaDatos.Add(new Datos("Groot", "A tree-like alien with impressive strength and regenerative abilities. Though he communicates with only the phrase 'I am Groot,' his loyalty and ability to grow and adapt make him a vital member of the Guardians of the Galaxy. His imposing presence and growth powers are key assets to the team."));
            listaDatos.Add(new Datos("Deadpool", "Wade Wilson, also known as Deadpool, is a mercenary with a regenerative healing factor that makes him nearly immortal. His dark humor, tendency to break the fourth wall, and unpredictable behavior set him apart in the Marvel universe. Despite his erratic nature, he's a formidable fighter."));
            listaDatos.Add(new Datos("Hawkeye", "Clint Barton, or Hawkeye, is a master archer with exceptional accuracy and a wide range of specialized arrows. His skills and bravery make him a crucial member of the Avengers. Although he lacks superpowers, his precision and expertise in archery make him a standout hero."));

            string stringJson = JsonSerializer.Serialize(listaDatos);

            helperJson.GuardarArchivo(nombreArchivo, stringJson);
        }

        public Personaje PersonajeAleatorio(List<Personaje> listaDePersonajes)
        {
            int random = new Random().Next(listaDePersonajes.Count);
            Personaje Oponente = listaDePersonajes[random];
            listaDePersonajes.Remove(Oponente);
            return Oponente;
        }

        public Personaje PersonajeElegido(List<Personaje> listaPersonajes, int opcion)
        {
            Personaje personaje = listaPersonajes[opcion - 1];
            listaPersonajes.Remove(personaje);
            return personaje;
        }
    }

    public class Personaje
    {
            
        private Datos datos { get; set; }
        private Caracteristicas caracteristicas { get; set; }

        public Datos Datos { get => datos; set => datos = value; }
        public Caracteristicas Caracteristicas { get => caracteristicas; set => caracteristicas = value; }

        public Personaje(Datos datos, Caracteristicas caracteristicas)
        {
            this.datos = datos;
            this.caracteristicas = caracteristicas;
        }
    }
}