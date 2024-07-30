using System;
using MovimientosCombate;
using MiProyecto.FabricaDePersonajes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;

public class FuncionesTexto
{
    public void Logo()
    {
        string[] logoLineas = {
        "╔═════════════════════════════════════════════╗",
        "║                                             ║",
        "║                                             ║",
        "║   __  __    _    ______     _______ _       ║",
        "║  |  \\/  |  / \\  |  _ \\ \\   / / ____| |      ║",
        "║  | |\\/| | / _ \\ | |_) \\ \\ / /|  _| | |      ║",
        "║  | |  | |/ ___ \\|  _ < \\ V / | |___| |___   ║",
        "║  |_|  |_/_/   \\_\\_| \\_\\ \\_/  |_____|_____|  ║",
        "║                                             ║",
        "║                                             ║",
        "╚═════════════════════════════════════════════╝"
    };

        CentrarLineas(logoLineas);
    }

    public void Menu()
    {
        Console.WriteLine("\n");
        string[] lineasMenu = {
            "┌──────────────────────────┐",
            "│       MENU PRINCIPAL     │",
            "│──────────────────────────│",
            "│                          │",
            "│  1- Comenzar nuevo       │",
            "│     torneo               │",
            "│                          │",
            "│  2- Combate 2v2          │",
            "│                          │",
            "│  3- Ganadores            │",
            "│  historicos del torneo   │",
            "│                          │",
            "│  4- Salir                │",
            "│                          │",
            "└──────────────────────────┘"
        };

        CentrarLineas(lineasMenu);
    }

    public void LogoCampeon()
    {
        string[] campeonLineas = {
        "   ____     _      __  __    ____   U _____ u U  ___ u  _   _    ",
        "U /\"___|U  /\"\\  uU|' \\/ '|uU|  _\"\\ u\\| ___\"|/  \\/\"_ \\/ | \\ |\"|   ",
        "\\| | u   \\/ _ \\/ \\| |\\/| |/\\| |_) |/ |  _|\"    | | | |<|  \\| |>  ",
        " | |/__  / ___ \\  | |  | |  |  __/   | |___.-,_| |_| |U| |\\  |u  ",
        "  \\____|/_/   \\_\\ |_|  |_|  |_|      |_____|\\_)-\\___/  |_| \\_|   ",
        " _// \\\\  \\\\    >><<,-,,-.   ||>>_    <<   >>     \\\\    ||   \\\\,-.",
        "(__)(__)(__)  (__)(./  \\.) (__)__)  (__) (__)   (__)   (_\")  (_/ "
        };
        
        CentrarLineas(campeonLineas);
    }

    public void CartelCampeones()
    {
        string[] campeonesLineas =
        {
        " _  ____                                                 _ ",
        "| |/ ___|__ _ _ __ ___  _ __   ___  ___  _ __   ___  ___| |",
        "| | |   / _` | '_ ` _ \\| '_ \\ / _ \\/ _ \\| '_ \\ / _ \\/ __| |",
        "| | |__| (_| | | | | | | |_) |  __/ (_) | | | |  __/\\__ \\ |",
        "| |\\____\\__,_|_| |_| |_| .__/ \\___|\\___/|_| |_|\\___||___/ |",
        "|_|                    |_|                              |_|"
    };

        CentrarLineas(campeonesLineas);
    }


    public void MostrarCampeones(string nombreArchivo)
    {
        var helperJson = new HelperJson();
        string stringjson = helperJson.AbrirArchivo(nombreArchivo);

        var listaCampeones = JsonSerializer.Deserialize<List<Personaje>>(stringjson);

        CartelCampeones();
        Console.WriteLine("\n");
        foreach(var campeon in listaCampeones)
        {
            Console.WriteLine($"Se consagró victorioso el {campeon.Datos.FechaCampeon.ToString("dd/MM/yy")} a las {campeon.Datos.FechaCampeon.ToString("HH:mm tt")}");
            MostrarPersonaje(campeon);
            Console.WriteLine("\n");
        }
    }

    public async Task FraseIntroduccionCombate(Personaje atacante1, Personaje atacante2, Personaje oponente1, Personaje oponente2)
    {
        int random = new Random().Next(6);
        switch (random)
        {
            case 0:
                await EscribirTextoAsync($"La tierra tiembla y el cielo se oscurece mientras {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se preparan para enfrentarse a {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡Una batalla épica está a punto de desatarse!");
                break;
            case 1:
                await EscribirTextoAsync($"En el umbral del destino, {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se enfrentan con una determinación feroz contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡Prepárense para el choque de sus vidas!");
                break;
            case 2:
                await EscribirTextoAsync($"Con el rugido del trueno como testigo, {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se disponen a medir sus fuerzas contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡La guerra está por empezar!");
                break;
            case 3:
                await EscribirTextoAsync($"Las leyendas se escriben con sangre, y hoy, {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} añadirán un nuevo capítulo al enfrentarse a {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡El combate final está por comenzar!");
                break;
            case 4:
                await EscribirTextoAsync($"El destino de muchos pende de un hilo mientras {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se preparan para el duelo definitivo contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡La historia será testigo de su enfrentamiento!");
                break;
            case 5:
                await EscribirTextoAsync($"Los antiguos espíritus observan expectantes mientras {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se preparan para desatar todo su poder contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡Un duelo titánico está a punto de comenzar!");
                break;
        }

        await Task.Delay(2000);
    }

    public async Task AnunciarGanador(Personaje campeon1, Personaje campeon2, Personaje perdedor1, Personaje perdedor2)
    {
        int random = new Random().Next(5);
        switch (random)
        {
            case 0:
                await EscribirTextoAsync($"Con una demostración de habilidad y valentía, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} emergen victoriosos en esta épica batalla contra {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡La gloria es suya!");
                break;
            case 1:
                await EscribirTextoAsync($"Después de un enfrentamiento titánico, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} se alzan con la victoria, dejando a {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre} derrotados en el campo de batalla. ¡El honor les pertenece!");
                break;
            case 2:
                await EscribirTextoAsync($"En una lucha que será recordada por la eternidad, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} han derrotado a {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡La victoria está escrita en sus nombres!");
                break;
            case 3:
                await EscribirTextoAsync($"El destino ha hablado y {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} han triunfado sobre {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡Que su victoria resuene a través del tiempo!");
                break;
            case 4:
                await EscribirTextoAsync($"Con un último golpe decisivo, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} reclaman la victoria sobre {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡Que sus nombres sean recordados en las leyendas!");
                break;
        }
    }


    public void MostrarDatosCombate(Personaje atacante, Personaje defensor, Dictionary<int, Movimientos> movimientosPorClave)
    {
        Console.WriteLine($"| {atacante.Datos.Nombre.ToUpper()} |\n");
        Console.WriteLine($"\tMana disponible: {atacante.Caracteristicas.Mana}\n");
        Console.WriteLine($"\tSalud actual: {atacante.Caracteristicas.Salud}");
        Console.WriteLine($"\tSalud del oponente: {defensor.Caracteristicas.Salud}\n");
        MostrarMovimientos(movimientosPorClave);
    }

    public void MostrarPersonaje(Personaje personaje)
    {
        Console.WriteLine(CentrarLinea($"═══════════════════════════════════════════ {personaje.Datos.Nombre} ═══════════════════════════════════════════"));
        Console.WriteLine(  $"║ Descripcion: {personaje.Datos.Descripcion}\n" +
                            $"║ Nivel:    {personaje.Caracteristicas.Nivel}\n" +
                            $"║ Salud:    {personaje.Caracteristicas.Salud}\n" +
                            $"║ Mana:     {personaje.Caracteristicas.Mana}\n" +
                            $"║ Daño:     {personaje.Caracteristicas.Daño}\n" +
                            $"║ Defensa:  {personaje.Caracteristicas.Defensa}\n" +
                            $"║ Precision:  {personaje.Caracteristicas.Precision}%");
        Console.WriteLine(CentrarLinea($"═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════"));
    }

    public void MostrarPersonajes(List<Personaje> ListaPersonajes)
    {
        int i = 1;
        foreach (var personaje in ListaPersonajes)
        {
            Console.WriteLine(CentrarLinea($"Opcion: {i}"));
            MostrarPersonaje(personaje);
            Console.WriteLine("\n");
            i++;
        }
    }

    public void MostrarMovimientos(Dictionary<int, Movimientos> movimientosPorClave)
    {
        int i = 1;
        var categorias = movimientosPorClave.Values.GroupBy(m => m.Categoria); //Agrupo los movimientos segun sus categorias

        foreach(var categoria in categorias)
        {
            Console.WriteLine($"\t──── {categoria.Key.ToUpper()} ────"); //Obtengo la clave del grupo (categoria)
            foreach(var movimiento in categoria)
            {
                Console.WriteLine($"\t{i}- '{movimiento.Nombre}' | {movimiento.Descripcion}");
                i++;
            }
            Console.WriteLine();
        }
    }

    public async Task EscribirTextoAsync(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            await Task.Delay(40); // Pausa entre cada letra
        }
    }
    public string CentrarLinea(string texto){
        return String.Format("{0," + ((Console.WindowWidth / 2) + (texto.Length / 2)) + "}", texto);
    }

    public void CentrarLineas(string[] lineas)
    {
        int consoleWidth = Console.WindowWidth; // Obtengo el ancho de la terminal

        foreach (string linea in lineas)
        {
            Console.WriteLine(CentrarLinea(linea));
        }
    }
}
