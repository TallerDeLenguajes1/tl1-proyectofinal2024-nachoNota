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
        var MovimientosPorClave = Combate.CrearClavesMovimientos();
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

                Personaje Oponente = Fabrica.PersonajeAlAzar(ListaDePersonajes);
                Texto.MostrarPersonaje(Oponente);
                await Task.Delay(4000);

                Console.Clear();

                //await Combate.NuevoCombate(PersonajeElegido, Oponente, MovimientosPorClave);

                if(Combate.EsGanador(PersonajeElegido))
                {
                    await Texto.EscribirTextoAsync("Felicidades, lograste avanzar a las semifinales de este torneo, pero no cantes victoria, las cosas de ahora en adelante se pondrán mas complicadas...\n\n");
                    await Task.Delay(1000);
                    PersonajeElegido.Caracteristicas.SubirEstadisticasJugador();

                    Console.WriteLine("Para estas instancias, tu rival será...\n");
                    await Task.Delay(2000);
                    
                    Oponente = Fabrica.PersonajeAlAzar(ListaDePersonajes);
                    Oponente.Caracteristicas.SubirNivelOponente();
                    
                    Texto.MostrarPersonaje(Oponente);
                    
                    await Task.Delay(4000);
                    
                    Console.Clear();

                  //  await Combate.NuevoCombate(PersonajeElegido, Oponente, MovimientosPorClave);

                        PersonajeElegido.Caracteristicas.Salud = 0;
                    if (Combate.EsGanador(PersonajeElegido))
                    {
                        await Texto.EscribirTextoAsync("¿Que? ¿En serio llegaste a la final? La verdad que no me esperaba que llegues tan lejos, voy a tener que ponerte las cosas más complicadas...\n\n");
                        await Task.Delay(1000);
                        PersonajeElegido.Caracteristicas.SubirEstadisticasJugador();

                        Console.WriteLine("Para estas instancias, tu rival será...\n");
                        await Task.Delay(2000);

                        Oponente = Fabrica.PersonajeAlAzar(ListaDePersonajes);
                        for(int i = 0; i < 2; i++)
                        {
                            Oponente.Caracteristicas.SubirNivelOponente();
                        }
                        Texto.MostrarPersonaje(Oponente);

                        await Task.Delay(4000);

                        Console.Clear();

                        //await Combate.NuevoCombate(PersonajeElegido, Oponente, MovimientosPorClave);
                        if (Combate.EsGanador(PersonajeElegido))
                        {
                            Texto.MensajeCampeon();
                            Console.WriteLine("\n\n");
                            await Texto.EscribirTextoAsync($"EN SERIO???? TE PUSE A MI MEJOR PELEADOR Y NO PUDO HACERTE NADA???");
                            await Task.Delay(1000);
                            await Texto.EscribirTextoAsync($" Bueno, supongo que tu nombre, {PersonajeElegido.Datos.Nombre}, merece estar en nuestro listado de campeones historicos.");
                            Combate.GuardarCampeon("CampeonesHistoricos.json", PersonajeElegido);
                            await Task.Delay(2000);
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
  
                            Combate.GuardarCampeon("CampeonesHistoricos.json", Oponente);

                            await Task.Delay(1000);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n");
                        await Texto.EscribirTextoAsync("Bueno... Sabia que no llegarías mas lejos que esto.\n");
                        await Task.Delay(1000);
                        await Texto.EscribirTextoAsync("Al fin y al cabo, pocos fueron los que lograron enfrentarse a mi campeón mas fuerte\n");
                        await Task.Delay(1000);
                        await Texto.EscribirTextoAsync("Pero bueno, ¿quien te dice que en el proximo intento no llegarás más lejos? Acá estaré esperando...\n");
                        await Task.Delay(1500);
                    }
                }
                else
                {
                    Console.WriteLine("\n\n");
                    await Texto.EscribirTextoAsync("Rival complicado eh?\n");
                    await Task.Delay(1000);
                    await Texto.EscribirTextoAsync("No te preocupers, siempres puedes arrancar el torneo desde cero, solo trata de no perder la paciencia!\n");
                    await Task.Delay(1000);
                    await Texto.EscribirTextoAsync("Te deseo suerte para la proxima!!");
                    await Task.Delay(1000);
                }

                break;
            case 2:
                Texto.MostrarPersonajes(ListaDePersonajes);
                Console.Write("Con qué personaje quiere pelear? (ingresar su numero): ");
                opcionPersonaje = Valid.ValidarOpcion(ListaDePersonajes.Count);

                Console.Clear();

                PersonajeElegido = Fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);
                Console.WriteLine($"\nMUY BIEN! El personaje elegido es: \n");
                Texto.MostrarPersonaje(PersonajeElegido);
                await Task.Delay(4000);

                Console.Clear();

                Console.WriteLine("\nQuieres elegir tu propio Oponente o elegir uno al azar? 0 = Elegir propio, 1 = Al azar\n");
                int OpcionOponente = Valid.ValidarOponente();

                Console.Clear();

                if (OpcionOponente == 1)
                {
                    Console.WriteLine("\nTu rival es...\n");
                    await Task.Delay(2000);
                    Oponente = Fabrica.PersonajeAlAzar(ListaDePersonajes);
                }
                else
                {
                    Texto.MostrarPersonajes(ListaDePersonajes);
                    Console.Write("Contra qué personaje quiere pelear? (ingresar su numero): ");
                    opcionPersonaje = Valid.ValidarOpcion(ListaDePersonajes.Count);

                    Console.Clear();
                    Oponente = Fabrica.PersonajeElegido(ListaDePersonajes, opcionPersonaje);
                    Console.WriteLine($"\nMUY BIEN! Tu oponente en este caso será: \n");
                }
                Texto.MostrarPersonaje(Oponente);
                await Task.Delay(4000);

                Console.Clear();

                await Texto.FraseIntroduccionCombate(PersonajeElegido, Oponente);

                Console.Clear();

                await Combate.NuevoCombate(PersonajeElegido, Oponente, MovimientosPorClave);

                if (Combate.EsGanador(PersonajeElegido))
                {
                    await Texto.AnunciarGanador(PersonajeElegido, Oponente);
                }
                else
                {
                    await Texto.AnunciarGanador(Oponente, PersonajeElegido);
                }
                break;
        }
    }
    else
    {
        Console.WriteLine("Esperamos que vuelvas a jugar pronto, suerte!");
        await Task.Delay(3000);
    }

    Console.Clear();

} while (OpcionMenu != 4);