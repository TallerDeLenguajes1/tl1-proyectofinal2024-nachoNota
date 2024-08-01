using System.Text.Json;
using MovimientosCombate;
using MiProyecto.FabricaDePersonajes;

namespace VentanaCombate
{
    public class Combate
    {
        /*      private Personaje elegido1
                private Personaje oponente1 
                private Personaje elegido2 
                private Personaje oponente2 

                public Combate(Personaje elegido1, Personaje oponente1, Personaje elegido2, Personaje oponente2)
                {
                    this.elegido1 = elegido1;
                }

                public Combate(Personaje elegido1, Personaje oponente1)
                {

                }*/
        public void NuevoCombate(Personaje elegido1, Personaje oponente1, Personaje elegido2 = null, Personaje oponente2 = null)
        {
            var movimientosPorClave = CrearClavesMovimientos();
            bool turno = true;

            if (elegido2 != null)
            {
                movimientosPorClave.Add(movimientosPorClave.Count + 1, new Movimientos("Refuerzos en camino", "Cambia con tu compañero de equipo", "Cambio", 0, 0));
            }

            Personaje personajeActivo = elegido1;
            Personaje oponenteActivo = oponente1;

            while (ContinuarCombate(elegido1, oponente1, elegido2, oponente2))
            {
                if (turno)
                {
                    if (personajeActivo.Caracteristicas.Salud <= 0)
                    {
                        personajeActivo = IntercambiarPersonaje(personajeActivo, personajeActivo == elegido1 ? elegido2 : elegido1);
                        Thread.Sleep(2400);
                        Console.Clear();
                    }

                    turno = TurnoJugador(ref personajeActivo, oponenteActivo, elegido1, elegido2, movimientosPorClave);
                }
                else
                {
                    if (oponenteActivo.Caracteristicas.Salud <= 0 && oponenteActivo == oponente1)
                    {
                        oponenteActivo = IntercambiarPersonaje(oponente1, oponente2);
                        Thread.Sleep(2400);
                        Console.Clear();
                    }

                    TurnoOponente(oponenteActivo, personajeActivo, movimientosPorClave);

                    turno = true;
                }
                Thread.Sleep(2400);
                Console.Clear();
            }
        }

        public bool TurnoJugador(ref Personaje personajeActivo, Personaje oponenteActivo, Personaje elegido1, Personaje elegido2, Dictionary<int, Movimientos> movimientosPorClave)
        {

            Console.WriteLine("\t\nEs tu turno! Realiza un movimiento!\n");
            new FuncionesTexto().MostrarDatosCombate(personajeActivo, oponenteActivo, movimientosPorClave);

            var (opcionElegida, movimientoElegido) = new ValidarOpciones().ValidarMovimiento(movimientosPorClave);

            Console.Clear();

            if (opcionElegida == movimientosPorClave.Count)
            {
                personajeActivo = IntercambiarPersonaje(personajeActivo, personajeActivo == elegido1 ? elegido2 : elegido1);
                return true;
            }
            else
            {
                return RealizarMovimientoJugador(personajeActivo, oponenteActivo, opcionElegida, movimientoElegido);
            }
        }

        public bool RealizarMovimientoJugador(Personaje atacante, Personaje defensor, int opcion, Movimientos movimiento)
        {
            bool turno = true;

            if (opcion == 1)
            {
                RestarSalud(atacante, defensor);
                turno = false;
            }
            else
            {
                if (atacante.Caracteristicas.Mana < movimiento.CostoMana)
                {
                    Console.WriteLine("\tMana insuficiente, elija otra opcion.");
                }
                else
                {
                    switch (opcion)
                    {
                        case 2:
                            BurlarDefensa(atacante, defensor, movimiento);
                            break;
                        case 3:
                        case 4:
                            AumentarDefensa(atacante, movimiento);
                            break;
                        case 5:
                        case 6:
                            AumentarSalud(atacante, movimiento);
                            break;
                    }
                    turno = false;
                }
            }
            return turno;
        }

        public void TurnoOponente(Personaje oponenteActivo, Personaje personajeActivo, Dictionary<int, Movimientos> movimientosPorClave)
        {

            Console.WriteLine("\t\nAhora estas viendo la pantalla del oponente, espera a que realice un movimiento...\n");
            new FuncionesTexto().MostrarDatosCombate(oponenteActivo, personajeActivo, movimientosPorClave);

            Thread.Sleep(3000);

            Console.Clear();

            RealizarMovimientoOponente(oponenteActivo, personajeActivo, movimientosPorClave);
        }

        public void RealizarMovimientoOponente(Personaje Oponente, Personaje PersonajeElegido, Dictionary<int, Movimientos> movimientosPorClave)
        {
            if (Oponente.Caracteristicas.Mana <= 10)
            {
                RestarSalud(Oponente, PersonajeElegido);
            }
            else
            {
                var rdm = new Random();
                if (Oponente.Caracteristicas.Salud >= 70)
                {
                    if (Oponente.Caracteristicas.Mana >= 70)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                                RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1:
                                BurlarDefensa(Oponente, PersonajeElegido, movimientosPorClave[2]);
                                break;
                            case 2:
                                AumentarDefensa(Oponente, movimientosPorClave[3]); //+8
                                break;
                        }
                    }
                    else
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                            case 1:
                                RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 2:
                                AumentarDefensa(Oponente, movimientosPorClave[4]); //+4
                                break;
                        }
                    }
                }
                else if (Oponente.Caracteristicas.Salud >= 50)
                {
                    if (Oponente.Caracteristicas.Mana >= 70)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                                RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1:
                                BurlarDefensa(Oponente, PersonajeElegido, movimientosPorClave[2]);
                                break;
                            case 2:
                                AumentarDefensa(Oponente, movimientosPorClave[3]); //+8
                                break;
                        }
                    }
                    else if (Oponente.Caracteristicas.Mana >= 40)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                            case 1:
                                RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 2:
                                AumentarDefensa(Oponente, movimientosPorClave[4]); //+4
                                break;
                        }
                    }
                    else
                    {
                        RestarSalud(Oponente, PersonajeElegido);
                    }
                }
                else if (Oponente.Caracteristicas.Salud > 25)
                {
                    if (Oponente.Caracteristicas.Mana >= 50)
                    {
                        switch (rdm.Next(4))
                        {
                            case 0:
                                RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1:
                                BurlarDefensa(Oponente, PersonajeElegido, movimientosPorClave[2]);
                                break;
                            case 2:
                                AumentarSalud(Oponente, movimientosPorClave[5]); //+25
                                break;
                            case 3:
                                AumentarSalud(Oponente, movimientosPorClave[6]); //+15
                                break;
                        }
                    }
                    else if (Oponente.Caracteristicas.Mana >= 30)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                                RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1:
                                AumentarSalud(Oponente, movimientosPorClave[6]); //+15
                                break;
                            case 2:
                                AumentarDefensa(Oponente, movimientosPorClave[4]); //+4
                                break;
                        }
                    }
                    else
                    {
                        RestarSalud(Oponente, PersonajeElegido);
                    }
                }
                else if (Oponente.Caracteristicas.Salud <= 25)
                {
                    if (PersonajeElegido.Caracteristicas.Salud <= 15)
                    {
                        if (Oponente.Caracteristicas.Mana >= 30)
                        {
                            BurlarDefensa(Oponente, PersonajeElegido, movimientosPorClave[2]);
                        }
                        else
                        {
                            RestarSalud(Oponente, PersonajeElegido);
                        }
                    }
                    else
                    {
                        if (Oponente.Caracteristicas.Mana >= 15)
                        {
                            if ((PersonajeElegido.Caracteristicas.Salud - Oponente.Caracteristicas.Salud) > 12)
                            {
                                if (Oponente.Caracteristicas.Mana >= 40)
                                {
                                    AumentarSalud(Oponente, movimientosPorClave[5]); //+25
                                }
                                else
                                {
                                    AumentarSalud(Oponente, movimientosPorClave[6]); //+15
                                }
                            }
                            else
                            {
                                RestarSalud(Oponente, PersonajeElegido);
                            }
                        }
                        else
                        {
                            RestarSalud(Oponente, PersonajeElegido);
                        }
                    }
                }
            }
        }

        public bool ContinuarCombate(Personaje elegido1, Personaje oponente1, Personaje elegido2 = null, Personaje oponente2 = null)
        {
            if (elegido2 == null)
            {
                return elegido1.Caracteristicas.Salud > 0 && oponente1.Caracteristicas.Salud > 0;
            }
            else
            {
                return (elegido1.Caracteristicas.Salud > 0 || elegido2.Caracteristicas.Salud > 0) &&
                    (oponente1.Caracteristicas.Salud > 0 || oponente2.Caracteristicas.Salud > 0);
            }
        }

        public Personaje IntercambiarPersonaje(Personaje personajeActual, Personaje personajeACambiar)
        {
            if (personajeACambiar.Caracteristicas.Salud > 0)
            {
                Console.WriteLine($"ATENCION!! {personajeActual.Datos.Nombre} abandona el campo de batalla y en su lugar ingresa {personajeACambiar.Datos.Nombre}");
                return personajeACambiar;
            }
            else
            {
                Console.WriteLine("No puedes usar a tu compañero ya que no tiene salud.");
                return personajeActual;
            }
        }

        public void RestarSalud(Personaje atacante, Personaje defensor)
        {
            int dañoRealizado = (int)(atacante.Caracteristicas.Daño - defensor.Caracteristicas.Defensa * 0.3);

            if (GolpeCritico(atacante))
            {
                dañoRealizado = (int)(dañoRealizado * 1.35);
                Console.WriteLine($"\t\t\nGOLPE CRITICO!!! Haz realizado {dañoRealizado} de daño a tu oponente");
            }
            else
            {
                Console.WriteLine($"\t\t\nHaz realizado {dañoRealizado} de daño a tu oponente");
            }
            defensor.Caracteristicas.Salud -= dañoRealizado;
        }

        public bool GolpeCritico(Personaje atacante)
        {
            double probabilidadCritico = atacante.Caracteristicas.Precision / 100.0;
            bool golpeCritico = (new Random().NextDouble() < probabilidadCritico); //NextDouble genera un numero entre 0.0 y 1.0
            return golpeCritico;
        }

        public void BurlarDefensa(Personaje atacante, Personaje defensor, Movimientos movimiento)
        {
            int dañoRealizado = atacante.Caracteristicas.Daño;
            defensor.Caracteristicas.Salud -= dañoRealizado;
            atacante.Caracteristicas.Mana -= movimiento.CostoMana;
            if (atacante.Caracteristicas.Mana < 0)
            {
                atacante.Caracteristicas.Mana = 0;
            }
            Console.WriteLine($"\t\t\nBurlaste la defensa enemiga e hiciste {dañoRealizado} de daño!");
        }

        public void AumentarSalud(Personaje atacante, Movimientos movimiento)
        {
            atacante.Caracteristicas.Salud += movimiento.CantidadAumento;
            atacante.Caracteristicas.Mana -= movimiento.CostoMana;
            if (atacante.Caracteristicas.Mana < 0)
            {
                atacante.Caracteristicas.Mana = 0;
            }
            Console.WriteLine($"\t\t\nBien! Aumentaste +{movimiento.CantidadAumento} tu salud, ahora tienes {atacante.Caracteristicas.Salud}");
        }

        public void AumentarDefensa(Personaje atacante, Movimientos movimiento)
        {
            atacante.Caracteristicas.Defensa += movimiento.CantidadAumento;
            atacante.Caracteristicas.Mana -= movimiento.CostoMana;
            if (atacante.Caracteristicas.Mana < 0)
            {
                atacante.Caracteristicas.Mana = 0;
            }
            Console.WriteLine($"\t\t\nBien jugado! Aumentaste +{movimiento.CantidadAumento}, ahora tu defensa es de {atacante.Caracteristicas.Defensa}");
        }

        public bool EsGanador(Personaje personaje1, Personaje personaje2 = null)
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

        public void ElegirDificultad(Personaje elegido1, Personaje elegido2, Personaje oponente1, Personaje oponente2)
        {
            Console.WriteLine("\nElegir dificultad: ");
            Console.WriteLine("\t1 = Facil\n" +
                                "\t2 = Normal\n" +
                                "\t3 = Dificil");
            Console.Write("Opcion: ");
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

        public Dictionary<int, Movimientos> CrearClavesMovimientos()
        {
            var listaMovimientos = CrearListaMovimientos();
            var movimientosPorClave = new Dictionary<int, Movimientos>();
            var categorias = listaMovimientos.GroupBy(m => m.Categoria);
            int i = 1;

            foreach (var categoria in categorias)
            {
                foreach (var movimiento in categoria)
                {
                    movimientosPorClave.Add(i, movimiento);
                    i++;
                }
            }
            return movimientosPorClave;
        }

        public List<Movimientos> CrearListaMovimientos()
        {
            return new List<Movimientos>
            {
                new Movimientos("Ataque normal", "Inflige daño a tu Oponente", "Ataque", 0, 0),
                new Movimientos("Carga abrasadora", "Burla la defensa enemiga e inflinge todo el daño posible (-30 de Mana)", "Ataque", 0, 30),
                new Movimientos("Protección Divina", "+8 de Defensa (-20 de Mana)", "Defensa", 8, 20),
                new Movimientos("Pocion de curación", "+25 de Salud (-25 de Mana)", "Salud", 25, 25),
                new Movimientos("Barrera mágica", "+4 de Defensa (-10 de Mana)", "Defensa", 4, 10),
                new Movimientos("Elixir de vida", "+15 de salud (-15 de Mana)", "Salud", 15, 15)
            };
        }
    }
}