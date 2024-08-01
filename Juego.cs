using System;
using System.Threading.Tasks;
using System.Text.Json;
using MiProyecto.FabricaDePersonajes;
using VentanaCombate;

public class Juego
{
	private FabricaDePersonajes fabrica;
	private ValidarOpciones validar;
    private HelperJson helperJson;
    private FuncionesTexto texto;
    private Combate Combate;

	public Juego()
	{
		fabrica = new FabricaDePersonajes();
		validar = new ValidarOpciones();
        helperJson = new HelperJson();
        texto = new FuncionesTexto();
        Combate = new Combate();
    }

	public async Task EmpezarNuevoJuego()
	{
		int opcionMenu = 0;
		do
		{
			texto.MostrarLogo();
            texto.MostrarMenu();
			
			Console.WriteLine();

			Console.Write(texto.CentrarLinea("Elija una de las opciones: "));
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
					case 3: MostrarListaCampeones();
						break;
				}
			} else
			{
                Console.WriteLine("Esperamos que vuelvas a jugar pronto, suerte!");
                Thread.Sleep(3000);
            }

            Thread.Sleep(1000);
            Console.Write("\n\nPresione cualquier tecla para salir...");
            Console.ReadKey();
            
            Console.Clear();
        } while (opcionMenu != 4);
	}

    //TORNEO
	private void NuevoTorneo(List<Personaje> ListaDePersonajes) 
	{
        texto.MostrarPersonajes(ListaDePersonajes);
        Console.Write("Con qué personaje quiere pelear? (ingresar su numero): ");
        int opcionPersonaje = validar.ValidarOpcion(ListaDePersonajes.Count);

        Console.Clear();

        Personaje PersonajeElegido = fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);
        Console.WriteLine($"\nMUY BIEN! El personaje elegido es: \n\n");
        texto.MostrarPersonaje(PersonajeElegido);
        Thread.Sleep(4000);

        Console.Clear();

        InstanciaCuartosTorneo(PersonajeElegido, ListaDePersonajes);

        if (Combate.EsGanador(PersonajeElegido))
        {
          InstanciaSemifinalTorneo(PersonajeElegido, ListaDePersonajes);

            if (Combate.EsGanador(PersonajeElegido))
            {
                InstanciaFinalTorneo(PersonajeElegido, ListaDePersonajes);
            }
            else
            {
                texto.MensajePerdedorSemifinal();
            }
        }
        else
        {
            texto.MensajePerdedorCuartos();
        }
    }

    //COMBATE 2V2
    private void NuevoCombate2v2(List<Personaje> ListaDePersonajes)
    {
        texto.MostrarPersonajes(ListaDePersonajes);
        Console.Write("Quién será tu primer peleador? (ingresar su numero): ");
        int opcionPersonaje = validar.ValidarOpcion(ListaDePersonajes.Count);
        Personaje PrimerElegido = fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);

        Console.Clear();
        Thread.Sleep(1000);

        texto.MostrarPersonajes(ListaDePersonajes);
        Console.Write("Pongamosle un compañero a tu peleador, quien será?: ");
        opcionPersonaje = validar.ValidarOpcion(ListaDePersonajes.Count);
        Personaje SegundoElegido = fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);

        Console.Clear();

        Personaje PrimerOponente = fabrica.PersonajeAleatorio(ListaDePersonajes);
        Personaje SegundoOponente = fabrica.PersonajeAleatorio(ListaDePersonajes);

        ElegirDificultad(PrimerElegido, SegundoElegido, PrimerOponente, SegundoOponente);

        Console.Clear();

        Console.WriteLine("Muy bien!! Tus oponentes en este caso serán: ");
        Thread.Sleep(2000);

        texto.MostrarPersonaje(PrimerOponente);
        texto.MostrarPersonaje(SegundoOponente);
        Thread.Sleep(6000);

        Console.Clear();

        texto.FraseIntroduccionCombate(PrimerElegido, SegundoElegido, PrimerOponente, SegundoOponente);
        Console.Clear();

        Combate.NuevoCombate(PrimerElegido, PrimerOponente, SegundoElegido, SegundoOponente);

        if (EsGanador(PrimerElegido, SegundoElegido))
        {
            texto.AnunciarGanador(PrimerElegido, SegundoElegido, PrimerOponente, SegundoOponente);
        }
        else
        {
            texto.AnunciarGanador(PrimerOponente, SegundoOponente, PrimerElegido, SegundoElegido);
        }
    }

    //MOSTRAR CAMPEONES
    private void MostrarListaCampeones()
    {
        string NombreArchivo = "CampeonesHistoricos.json";
        string rutaCompleta = Path.GetFullPath(NombreArchivo);

        if (!File.Exists(rutaCompleta))
        {
            Console.WriteLine("\n\nTodavia no tienes ningun campeón dentro de esta lista, prueba jugando nuestra modalidad de torneo para grabar el nombre de tus campeones en este sector!");
        }
        else
        {
            texto.MostrarCampeones(NombreArchivo);
        }
    }

    private void InstanciaCuartosTorneo(Personaje PersonajeElegido, List<Personaje> ListaDePersonajes)
    {
        Console.WriteLine("En cuartos de final tendras que enfrentarte a...\n");
        Thread.Sleep(2000);

        Personaje Oponente = fabrica.PersonajeAleatorio(ListaDePersonajes);
        texto.MostrarPersonaje(Oponente);
        Thread.Sleep(4000);

        Console.Clear();

        Combate.NuevoCombate(PersonajeElegido, Oponente);
    }

    private void InstanciaSemifinalTorneo(Personaje PersonajeElegido, List<Personaje> ListaDePersonajes)
    {
        texto.EscribirTextoAsync("Felicidades, lograste avanzar a las semifinales de este torneo, pero no cantes victoria, las cosas de ahora en adelante se pondrán mas complicadas...\n\n");
        Thread.Sleep(1000);
        PersonajeElegido.Caracteristicas.SubirEstadisticasJugador();

        Console.WriteLine("Para estas instancias, tu rival será...\n");
        Thread.Sleep(2000);

        Personaje Oponente = fabrica.PersonajeAleatorio(ListaDePersonajes);
        Oponente.Caracteristicas.SubirNivelOponente();

        texto.MostrarPersonaje(Oponente);
        Thread.Sleep(4000);

        Console.Clear();
        
        Combate.NuevoCombate(PersonajeElegido, Oponente);
    }

    private void InstanciaFinalTorneo(Personaje PersonajeElegido, List<Personaje> ListaDePersonajes)
    {
        texto.EscribirTextoAsync("¿Que? ¿En serio llegaste a la final? La verdad que no me esperaba que llegues tan lejos, voy a tener que ponerte las cosas más complicadas...\n\n");
        Thread.Sleep(1000);
        PersonajeElegido.Caracteristicas.SubirEstadisticasJugador();

        Console.WriteLine("Para estas instancias, tu rival será...\n");
        Thread.Sleep(2000);

        Personaje Oponente = fabrica.PersonajeAleatorio(ListaDePersonajes);
        for (int i = 0; i < 2; i++)
        {
            Oponente.Caracteristicas.SubirNivelOponente();
        }
        texto.MostrarPersonaje(Oponente);
        Thread.Sleep(4000);

        Console.Clear();

        Combate.NuevoCombate(PersonajeElegido, Oponente);

        VerificarCampeonTorneo(PersonajeElegido, Oponente);
    }

    private void VerificarCampeonTorneo(Personaje PersonajeElegido, Personaje Oponente)
    {
        if (Combate.EsGanador(PersonajeElegido))
        {
            texto.MensajeGanadorFinal(PersonajeElegido);
            GuardarCampeon("CampeonesHistoricos.json", PersonajeElegido);
        }
        else
        {
            texto.MensajePerdedorFinal();
            GuardarCampeon("CampeonesHistoricos.json", Oponente);
        }
    }

    private void ElegirDificultad(Personaje elegido1, Personaje elegido2, Personaje oponente1, Personaje oponente2)
    {
        MostrarOpcionesDificultad();
        int opcion = new ValidarOpciones().ValidarOpcion(3);
        switch (opcion)
        {
            case 1:
                elegido1.Caracteristicas.SubirEstadisticasJugador();
                elegido2.Caracteristicas.SubirEstadisticasJugador();
                break;
            case 2:
                oponente1.Caracteristicas.SubirNivelOponente();
                oponente2.Caracteristicas.SubirNivelOponente();
                break;
            case 3:
                for (int i = 0; i < 2; i++)
                {
                    oponente1.Caracteristicas.SubirNivelOponente();
                    oponente2.Caracteristicas.SubirNivelOponente();
                }
                break;
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

    private void GuardarCampeon(string nombreArchivo, Personaje campeon)
    {
        var listaCampeones = new List<Personaje>();

        campeon.Datos.FechaCampeon = DateTime.Now;

        if (!File.Exists(nombreArchivo))
        {
            listaCampeones.Add(campeon);
            string stringJson = JsonSerializer.Serialize(listaCampeones);
            helperJson.GuardarArchivo(nombreArchivo, stringJson);
        }
        else
        {
            string recuperadoJson = helperJson.AbrirArchivo(nombreArchivo);

            var listaRecuperada = JsonSerializer.Deserialize<List<Personaje>>(recuperadoJson);

            listaRecuperada.Add(campeon);

            string stringJsonNuevo = JsonSerializer.Serialize(listaRecuperada);

            helperJson.GuardarArchivo(nombreArchivo, stringJsonNuevo);
        }
    }
}