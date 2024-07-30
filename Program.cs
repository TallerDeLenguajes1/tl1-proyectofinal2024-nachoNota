using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovimientosCombate;
using VentanaCombate;
using MiProyecto.FabricaDePersonajes;

Console.Clear();

var Texto = new FuncionesTexto();
var Valid = new ValidarOpciones();
var Combate = new Combate();
var Fabrica = new FabricaDePersonajes();
var helperJson = new HelperJson();
var ListaNombresDescripciones = await APICLIENT.GetCharactersAsync();

int OpcionMenu = 0;
do
{
    //Muestro el menu con las opciones a elegir 
    Texto.Logo();
    Texto.Menu();

    Console.WriteLine();

    Console.Write(Texto.CentrarLinea("Elija una de las opciones: "));
    OpcionMenu = Valid.ValidarOpcion(4);

    Console.Clear();

    if(OpcionMenu != 4)
    {
        //Creo personajes con sus datos y caracteristicas y hago lista que los contenga
        var ListaDePersonajes = Fabrica.CrearListaPersonajes(ListaNombresDescripciones);
        switch (OpcionMenu)
        {
            case 1:
                //Muestro los personajes para que el usuario pueda elegir el suyo
                Texto.MostrarPersonajes(ListaDePersonajes);
                Console.Write("Con qué personaje quiere pelear? (ingresar su numero): ");
                int opcionPersonaje = Valid.ValidarOpcion(ListaDePersonajes.Count);

                Console.Clear();

                Personaje PersonajeElegido = Fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);
                Console.WriteLine($"\nMUY BIEN! El personaje elegido es: \n\n");
                Texto.MostrarPersonaje(PersonajeElegido);
                await Task.Delay(4000);

                Console.Clear();

                Console.WriteLine("En cuartos de final tendras que enfrentarte a...\n");
                await Task.Delay(2000);

                Personaje Oponente = Fabrica.PersonajeAleatorio(ListaDePersonajes);
                Texto.MostrarPersonaje(Oponente);
                await Task.Delay(4000);

                Console.Clear();

                await Combate.NuevoCombate(PersonajeElegido, Oponente);

                if(Combate.EsGanador(PersonajeElegido))
                {
                    await Texto.EscribirTextoAsync("Felicidades, lograste avanzar a las semifinales de este torneo, pero no cantes victoria, las cosas de ahora en adelante se pondrán mas complicadas...\n\n");
                    await Task.Delay(1000);
                    PersonajeElegido.Caracteristicas.SubirEstadisticasJugador();

                    Console.WriteLine("Para estas instancias, tu rival será...\n");
                    await Task.Delay(2000);
                    
                    Oponente = Fabrica.PersonajeAleatorio(ListaDePersonajes);
                    Oponente.Caracteristicas.SubirNivelOponente();
                    
                    Texto.MostrarPersonaje(Oponente);
                    
                    await Task.Delay(4000);
                    
                    Console.Clear();

                    await Combate.NuevoCombate(PersonajeElegido, Oponente);

                    if (Combate.EsGanador(PersonajeElegido))
                    {
                        await Texto.EscribirTextoAsync("¿Que? ¿En serio llegaste a la final? La verdad que no me esperaba que llegues tan lejos, voy a tener que ponerte las cosas más complicadas...\n\n");
                        await Task.Delay(1000);
                        PersonajeElegido.Caracteristicas.SubirEstadisticasJugador();

                        Console.WriteLine("Para estas instancias, tu rival será...\n");
                        await Task.Delay(2000);

                        Oponente = Fabrica.PersonajeAleatorio(ListaDePersonajes);
                        for(int i = 0; i < 2; i++)
                        {
                            Oponente.Caracteristicas.SubirNivelOponente();
                        }
                        Texto.MostrarPersonaje(Oponente);

                        await Task.Delay(4000);

                        Console.Clear();

                        await Combate.NuevoCombate(PersonajeElegido, Oponente);

                        if (Combate.EsGanador(PersonajeElegido))
                        {
                            Texto.LogoCampeon();
                            Console.WriteLine("\n\n");
                            await Texto.EscribirTextoAsync("EN SERIO???? TE PUSE A MI MEJOR PELEADOR Y NO PUDO HACERTE NADA???");
                            await Task.Delay(1000);
                            await Texto.EscribirTextoAsync($" Bueno, supongo que tu nombre, {PersonajeElegido.Datos.Nombre}, merece estar en nuestro listado de campeones historicos, te lo ganaste.");
                            await Task.Delay(1000);
                            await Texto.EscribirTextoAsync("\nPor cierto, acá estaré esperando por mi revancha...");
                            
                            helperJson.GuardarCampeon("CampeonesHistoricos.json", PersonajeElegido);
                        }
                        else
                        {
                            Console.WriteLine("\n\n");
                            await Texto.EscribirTextoAsync("JAAAAAAAAAAAAAAAAAAAAAAAA JA JA JA JA JA JA\n");
                            await Task.Delay(1000);
                            await Texto.EscribirTextoAsync("ah, perdon. Donde estan mis modales...\n");
                            await Task.Delay(1000);
                            await Texto.EscribirTextoAsync("Te felicito por haber llegado tan lejos, pocos héroes logran estar donde estás parado ahora mismo, lástima que tuviste que verte las caras con mi peleador mas fuerte.\n");
                            await Task.Delay(1000);
                            await Texto.EscribirTextoAsync("ah, casi se me olvida, he agregado el nombre de mi campeón a la lista de historicos, para que cada vez que entres puedas ver el dia exacto en el que perdiste");
                            await Task.Delay(1000);
                            Console.Write(" :D");

                            helperJson.GuardarCampeon("CampeonesHistoricos.json", Oponente);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n");
                        await Texto.EscribirTextoAsync("Bueno... Sabia que no llegarías mas lejos que esto.\n");
                        await Task.Delay(1000);
                        await Texto.EscribirTextoAsync("Al fin y al cabo, pocos fueron los que lograron enfrentarse a mi campeón mas fuerte\n");
                        await Task.Delay(1000);
                        await Texto.EscribirTextoAsync("Pero bueno, ¿quien te dice que en el próximo intento no llegarás más lejos? Acá estaré esperando...\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n\n");
                    await Texto.EscribirTextoAsync("Rival complicado eh?\n");
                    await Task.Delay(1000);
                    await Texto.EscribirTextoAsync("No te preocupes, siempres puedes arrancar el torneo desde cero, solo trata de no perder la paciencia!\n");
                    await Task.Delay(1000);
                    await Texto.EscribirTextoAsync("Te deseo suerte para la proxima!!");
                }
                break;
            case 2:
                Texto.MostrarPersonajes(ListaDePersonajes);
                Console.Write("Quién será tu primer peleador? (ingresar su numero): ");
                opcionPersonaje = Valid.ValidarOpcion(ListaDePersonajes.Count);
                Personaje PrimerElegido = Fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);

                Console.Clear();
                await Task.Delay(1000);

                Texto.MostrarPersonajes(ListaDePersonajes);
                Console.Write("Pongamosle un compañero a tu peleador, quien será?: ");
                opcionPersonaje = Valid.ValidarOpcion(ListaDePersonajes.Count);
                Personaje SegundoElegido = Fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);

                Console.Clear();

                Console.WriteLine("Muy bien!! Tus oponentes en este caso serán: ");
                await Task.Delay(2000);

                Personaje PrimerOponente = Fabrica.PersonajeAleatorio(ListaDePersonajes);
                Personaje SegundoOponente = Fabrica.PersonajeAleatorio(ListaDePersonajes);
                Texto.MostrarPersonaje(PrimerOponente);
                Texto.MostrarPersonaje(SegundoOponente);
                await Task.Delay(8000);

                Console.Clear();
                
                await Combate.NuevoCombate(PrimerElegido, PrimerOponente, SegundoElegido, SegundoOponente);

                if (Combate.EsGanador(PrimerElegido, SegundoElegido))
                {
                    await Texto.AnunciarGanador(PrimerElegido, SegundoElegido, PrimerOponente, SegundoOponente);
                }
                else
                {
                    await Texto.AnunciarGanador(PrimerOponente, SegundoOponente, PrimerElegido, SegundoElegido);
                }
                break;
            case 3:
                string NombreArchivo = "CampeonesHistoricos.json";
                string rutaCompleta = Path.GetFullPath(NombreArchivo);
                
                if (!File.Exists(rutaCompleta))
                {
                    Console.WriteLine("\n\nTodavia no tienes ningun campeón dentro de esta lista, prueba jugando nuestra modalidad de torneo para grabar el nombre de tus campeones en este sector!");
                } else
                {
                    Texto.MostrarCampeones(NombreArchivo);
                }

                break;
        }
        await Task.Delay(1000);
        Console.Write("\n\nPresione cualquier tecla para salir...");
        Console.ReadKey();

    }
    else
    {
        Console.WriteLine("Esperamos que vuelvas a jugar pronto, suerte!");
        await Task.Delay(3000);
    }

    Console.Clear();

} while (OpcionMenu != 4);