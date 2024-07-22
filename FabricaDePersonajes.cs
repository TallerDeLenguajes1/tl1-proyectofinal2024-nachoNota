using System;
using System.IO;

public class FabricaDePersonajes
{
    public Personaje CrearPersonaje(Datos datos){
        var caracteristicas = new Caracteristicas();
        var NuevoPersonaje = new Personaje(datos, caracteristicas);
        return NuevoPersonaje;
    }

    public List<Personaje> CrearListaPersonajes(Raiz raiz)
    {
        raiz = null;
        if(raiz != null)
        {  
            return ListaCreadaApi(raiz);
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
        string archivo = "CampeonesHistoricos.csv";
        if (!File.Exists(archivo))
        {
            CrearArchivoDatosPersonajes();
        }

        using(StreamReader strReader = new StreamReader(archivo))
        {
            string linea;
            while((linea = strReader.ReadLine()) != null) //Leo y utilizo cada linea dentro del archivo hasta que llegue al final
            {
                var NombreDescripcion = linea.Split(';');
                var PersonajeCreado = CrearPersonaje(new Datos(NombreDescripcion[0], NombreDescripcion[1]));
                PersonajeCreado.Caracteristicas.BalancearEstadisticas();
                listaPersonajes.Add(PersonajeCreado);
            }
        }

        return listaPersonajes;
        
    }

    public void CrearArchivoDatosPersonajes()
    {
        FileStream Archivo = new FileStream("CampeonesHistoricos.csv", FileMode.Create);
        using(StreamWriter strWriter = new StreamWriter(Archivo))
        {
            strWriter.WriteLine("Daredevil;When Matt Murdock saved a man from an oncoming truck, it spilled a radioactive cargo that rendered Matt blind while enhancing his remaining senses. Under the harsh tutelage of blind martial arts master Stick, Matt mastered his heightened senses and became a formidable fighter.");
            strWriter.WriteLine("Iron Man;Wounded, captured and forced to build a weapon by his enemies, billionaire industrialist Tony Stark instead created an advanced suit of armor to save his life and escape captivity. Now with a new outlook on life, Tony uses his money and intelligence to make the world a safer, better place as Iron Man.");
            strWriter.WriteLine("Black Panther;T'Challa, the Black Panther, is the king of Wakanda, a highly advanced African nation. He possesses enhanced abilities given to him by the heart-shaped herb and wears a vibranium suit that makes him a formidable warrior and protector of his people.");
            strWriter.WriteLine("Doctor Strange;Once a brilliant but arrogant neurosurgeon, Stephen Strange's life changed forever after a car accident robbed him of the use of his hands. Searching for healing, he discovered the hidden world of magic and alternate dimensions, becoming the Sorcerer Supreme and the protector of Earth against mystical threats.");
            strWriter.WriteLine("Rocket Raccoon;A genetically-engineered raccoon with advanced engineering skills and a knack for high-tech weaponry. Known for his sharp wit and fiery personality, Rocket is crucial to the Guardians of the Galaxy for his expertise in gadgets and weaponry.");
            strWriter.WriteLine("Groot;A tree-like alien with impressive strength and regenerative abilities. Though he communicates with only the phrase 'I am Groot,' his loyalty and ability to grow and adapt make him a vital member of the Guardians of the Galaxy. His imposing presence and growth powers are key assets to the team.");
            strWriter.WriteLine("Deadpool;Wade Wilson, also known as Deadpool, is a mercenary with a regenerative healing factor that makes him nearly immortal. His dark humor, tendency to break the fourth wall, and unpredictable behavior set him apart in the Marvel universe. Despite his erratic nature, he's a formidable fighter.");
            strWriter.WriteLine("Hawkeye;Clint Barton, or Hawkeye, is a master archer with exceptional accuracy and a wide range of specialized arrows. His skills and bravery make him a crucial member of the Avengers. Although he lacks superpowers, his precision and expertise in archery make him a standout hero.");

            strWriter.Close();
        }
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