using System;
using System.Threading.Tasks;
using MiProyecto.FabricaDePersonajes;
using VentanaCombate;

public class Juego
{
	private FabricaDePersonajes fabrica;
	private ValidarOpciones validar;
    private HelperJson helperJson;

	public Juego()
	{
		fabrica = new FabricaDePersonajes();
		validar = new ValidarOpciones();
        helperJson = new HelperJson();
    }

	public async Task EmpezarNuevoJuego()
	{
		int opcionMenu = 0;
		do
		{
			MostrarLogo();
			MostrarMenu();
			
			Console.WriteLine();

			Console.Write(CentrarLinea("Elija una de las opciones: "));
			opcionMenu = validar.ValidarOpcion(4);

            Console.Clear();

			if(opcionMenu != 4)
			{
				var ListaDePersonajes = await fabrica.CrearListaPersonajes();

				switch (opcionMenu)
				{
					case 1: NuevoTorneo(ListaDePersonajes);
						break;
					case 2: NuevoCombate2v2(ListaDePersonajes);
						break;
					case 3: //MostrarCampeones();
						break;
				}
			} else
			{
                Console.WriteLine("Esperamos que vuelvas a jugar pronto, suerte!");
                Thread.Sleep(3000);
            }

            await Task.Delay(1000);
            Console.Write("\n\nPresione cualquier tecla para salir...");
            Console.ReadKey();

        } while (opcionMenu != 4);
	}

	private void NuevoTorneo(List<Personaje> ListaDePersonajes)
	{
        Console.WriteLine("hola");
	}

	private void NuevoCombate2v2(List<Personaje> ListaDePersonajes)
	{
        MostrarPersonajes(ListaDePersonajes);
        Console.Write("Quién será tu primer peleador? (ingresar su numero): ");
        int opcionPersonaje = validar.ValidarOpcion(ListaDePersonajes.Count);
        Personaje PrimerElegido = fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);

        Console.Clear();
        Thread.Sleep(1000);

        MostrarPersonajes(ListaDePersonajes);
        Console.Write("Pongamosle un compañero a tu peleador, quien será?: ");
        opcionPersonaje = validar.ValidarOpcion(ListaDePersonajes.Count);
        Personaje SegundoElegido = fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);

        Console.Clear();
        var Combate = new Combate();
        Personaje PrimerOponente = fabrica.PersonajeAleatorio(ListaDePersonajes);
        Personaje SegundoOponente = fabrica.PersonajeAleatorio(ListaDePersonajes);

        Combate.ElegirDificultad(PrimerElegido, SegundoElegido, PrimerOponente, SegundoOponente);

        Console.Clear();

        Console.WriteLine("Muy bien!! Tus oponentes en este caso serán: ");
        Thread.Sleep(2000);

        MostrarPersonaje(PrimerOponente);
        MostrarPersonaje(SegundoOponente);
        Thread.Sleep(6000);

        Console.Clear();

        Combate.NuevoCombate(PrimerElegido, PrimerOponente, SegundoElegido, SegundoOponente);

        if (EsGanador(PrimerElegido, SegundoElegido))
        {
            AnunciarGanador(PrimerElegido, SegundoElegido, PrimerOponente, SegundoOponente);
        }
        else
        {
            AnunciarGanador(PrimerOponente, SegundoOponente, PrimerElegido, SegundoElegido);
        }
    }

    private void MostrarOpcionesDificultad()
    {
        Console.WriteLine("\nElegir dificultad: ");
        Console.WriteLine("\t1 = Facil\n" +
                            "\t2 = Normal\n" +
                            "\t3 = Dificil");
        Console.Write("Opcion: ");
    }

    private bool EsGanador(Personaje personaje1, Personaje personaje2 = null)
    {
        if (personaje2 == null)
        {
            return personaje1.Caracteristicas.Salud > 0;
        }
        else
        {
            return personaje1.Caracteristicas.Salud > 0 || personaje2.Caracteristicas.Salud > 0;
        }
    }

    private void MostrarLogo()
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

    private void MostrarMenu()
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

    private void MostrarPersonaje(Personaje personaje)
    {
        Console.WriteLine(CentrarLinea($"═══════════════════════════════════════════ {personaje.Datos.Nombre} ═══════════════════════════════════════════"));
        Console.WriteLine($"║ Descripcion: {personaje.Datos.Descripcion}\n" +
                            $"║ Nivel:    {personaje.Caracteristicas.Nivel}\n" +
                            $"║ Salud:    {personaje.Caracteristicas.Salud}\n" +
                            $"║ Mana:     {personaje.Caracteristicas.Mana}\n" +
                            $"║ Daño:     {personaje.Caracteristicas.Daño}\n" +
                            $"║ Defensa:  {personaje.Caracteristicas.Defensa}\n" +
                            $"║ Precision:  {personaje.Caracteristicas.Precision}%");
        Console.WriteLine(CentrarLinea($"═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════"));
    }

    private void MostrarPersonajes(List<Personaje> ListaPersonajes)
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

    public void FraseIntroduccionCombate(Personaje atacante1, Personaje atacante2, Personaje oponente1, Personaje oponente2)
    {
        int random = new Random().Next(6);
        switch (random)
        {
            case 0:
                 EscribirTextoAsync($"La tierra tiembla y el cielo se oscurece mientras {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se preparan para enfrentarse a {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡Una batalla épica está a punto de desatarse!");
                break;
            case 1:
                 EscribirTextoAsync($"En el umbral del destino, {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se enfrentan con una determinación feroz contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡Prepárense para el choque de sus vidas!");
                break;
            case 2:
                 EscribirTextoAsync($"Con el rugido del trueno como testigo, {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se disponen a medir sus fuerzas contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡La guerra está por empezar!");
                break;
            case 3:
                 EscribirTextoAsync($"Las leyendas se escriben con sangre, y hoy, {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} añadirán un nuevo capítulo al enfrentarse a {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡El combate final está por comenzar!");
                break;
            case 4:
                 EscribirTextoAsync($"El destino de muchos pende de un hilo mientras {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se preparan para el duelo definitivo contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡La historia será testigo de su enfrentamiento!");
                break;
            case 5:
                 EscribirTextoAsync($"Los antiguos espíritus observan expectantes mientras {atacante1.Datos.Nombre} y {atacante2.Datos.Nombre} se preparan para desatar todo su poder contra {oponente1.Datos.Nombre} y {oponente2.Datos.Nombre}. ¡Un duelo titánico está a punto de comenzar!");
                break;
        }

        Thread.Sleep(2000);
    }

    public void AnunciarGanador(Personaje campeon1, Personaje campeon2, Personaje perdedor1, Personaje perdedor2)
    {
        int random = new Random().Next(5);
        switch (random)
        {
            case 0:
                 EscribirTextoAsync($"Con una demostración de habilidad y valentía, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} emergen victoriosos en esta épica batalla contra {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡La gloria es suya!");
                break;
            case 1:
                 EscribirTextoAsync($"Después de un enfrentamiento titánico, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} se alzan con la victoria, dejando a {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre} derrotados en el campo de batalla. ¡El honor les pertenece!");
                break;
            case 2:
                 EscribirTextoAsync($"En una lucha que será recordada por la eternidad, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} han derrotado a {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡La victoria está escrita en sus nombres!");
                break;
            case 3:
                 EscribirTextoAsync($"El destino ha hablado y {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} han triunfado sobre {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡Que su victoria resuene a través del tiempo!");
                break;
            case 4:
                 EscribirTextoAsync($"Con un último golpe decisivo, {campeon1.Datos.Nombre} y {campeon2.Datos.Nombre} reclaman la victoria sobre {perdedor1.Datos.Nombre} y {perdedor2.Datos.Nombre}. ¡Que sus nombres sean recordados en las leyendas!");
                break;
        }
    }

    public void EscribirTextoAsync(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(40); // Pausa entre cada letra
        }
    }

    private string CentrarLinea(string texto)
    {
        return String.Format("{0," + ((Console.WindowWidth / 2) + (texto.Length / 2)) + "}", texto);
    }

    private void CentrarLineas(string[] lineas)
    {
        int consoleWidth = Console.WindowWidth; // Obtengo el ancho de la terminal

        foreach (string linea in lineas)
        {
            Console.WriteLine(CentrarLinea(linea));
        }
    }
}
