using MovimientosCombate;
using MiProyecto.FabricaDePersonajes;

namespace VentanaCombate
{
    public class Combate
    {
        private Personaje elegido1;
        private Personaje oponente1;
        private Personaje elegido2;
        private Personaje oponente2;
        private Dictionary<int, Movimientos> movimientosPorClave;

        private Personaje personajeActivo;
        private Personaje oponenteActivo;

        public Combate(Personaje elegido1, Personaje oponente1, Personaje elegido2, Personaje oponente2)
        {
            this.elegido1 = elegido1;
            this.oponente1 = oponente1;
            this.elegido2 = elegido2;
            this.oponente2 = oponente2;

            personajeActivo = elegido1;
            oponenteActivo = oponente1;
            movimientosPorClave = CrearClavesMovimientos();
        }

        public Combate(Personaje elegido1, Personaje oponente1)
        {
            this.elegido1 = elegido1;
            this.oponente1 = oponente1;

            personajeActivo = elegido1;
            oponenteActivo = oponente1;
            movimientosPorClave = CrearClavesMovimientos();
        }

        public void NuevoCombate()
        {
            bool turno = true;

            if (elegido2 != null)
            {
                movimientosPorClave.Add(movimientosPorClave.Count + 1, new Movimientos("Refuerzos en camino", "Cambia con tu compañero de equipo", "Cambio", 0, 0));
            }

            while (ContinuarCombate())
            {
                if (turno)
                {
                    if (personajeActivo.Caracteristicas.Salud <= 0)
                    {
                        personajeActivo = IntercambiarPersonaje(personajeActivo, personajeActivo == elegido1 ? elegido2 : elegido1);
                        Thread.Sleep(2400);
                        Console.Clear();
                    }

                    turno = TurnoJugador();
                }
                else
                {
                    if (oponenteActivo.Caracteristicas.Salud <= 0 && oponenteActivo == oponente1)
                    {
                        oponenteActivo = IntercambiarPersonaje(oponente1, oponente2);
                        Thread.Sleep(2400);
                        Console.Clear();
                    }

                    TurnoOponente();

                    turno = true;
                }
                Thread.Sleep(2400);
                Console.Clear();
            }
        }

        private bool TurnoJugador()
        {
            Console.WriteLine("\t\nEs tu turno! Realiza un movimiento!\n");
            MostrarDatosCombate(personajeActivo, oponenteActivo);

            var (opcionElegida, movimientoElegido) = new ValidarOpciones().ValidarMovimiento(movimientosPorClave);

            Console.Clear();

            if (movimientoElegido.Categoria.ToUpper() == "CAMBIO")
            {
                personajeActivo = IntercambiarPersonaje(personajeActivo, personajeActivo == elegido1 ? elegido2 : elegido1);
                return true;
            }
            else
            {
                return RealizarMovimientoJugador(opcionElegida, movimientoElegido);
            }
        }

        private bool RealizarMovimientoJugador(int opcion, Movimientos movimiento)
        {
            bool turno = true;

            if (opcion == 1)
            {
                RestarSalud(personajeActivo, oponenteActivo);
                turno = false;
            }
            else
            {
                if (personajeActivo.Caracteristicas.Mana < movimiento.CostoMana)
                {
                    Console.WriteLine("\tMana insuficiente, elija otra opcion.");
                }
                else
                {
                    switch (opcion)
                    {
                        case 2:
                            BurlarDefensa(personajeActivo, oponenteActivo, movimiento);
                            break;
                        case 3:
                        case 4:
                            AumentarDefensa(personajeActivo, movimiento);
                            break;
                        case 5:
                        case 6:
                            AumentarSalud(personajeActivo, movimiento);
                            break;
                    }
                    turno = false;
                }
            }
            return turno;
        }

        private void TurnoOponente()
        {
            Console.WriteLine("\t\nAhora estas viendo la pantalla del oponente, espera a que realice un movimiento...\n");
            MostrarDatosCombate(oponenteActivo, personajeActivo);

            Thread.Sleep(3000);

            Console.Clear();

            RealizarMovimientoOponente();
        }

        private void RealizarMovimientoOponente()
        {
            if (oponenteActivo.Caracteristicas.Mana <= 10)
            {
                RestarSalud(oponenteActivo, personajeActivo);
            }
            else
            {
                /*
                 Movimientos
                    1- Ataque normal
                    2- Carga abrasadora
                    3- Proteccion divina  +8
                    4- Barrera magica +4
                    5- Elixir de vida +25
                    6- Pocion de curacion +15
                 */
                var rdm = new Random();
                if (oponenteActivo.Caracteristicas.Salud >= 70)
                {
                    if (oponenteActivo.Caracteristicas.Mana >= 70)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                                RestarSalud(oponenteActivo, personajeActivo);
                                break;
                            case 1:
                                BurlarDefensa(oponenteActivo, personajeActivo, movimientosPorClave[2]);
                                break;
                            case 2:
                                AumentarDefensa(oponenteActivo, movimientosPorClave[3]); //+8
                                break;
                        }
                    }
                    else
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                            case 1:
                                RestarSalud(oponenteActivo, personajeActivo);
                                break;
                            case 2:
                                AumentarDefensa(oponenteActivo, movimientosPorClave[4]); //+4
                                break;
                        }
                    }
                }
                else if (oponenteActivo.Caracteristicas.Salud >= 50)
                {
                    if (oponenteActivo.Caracteristicas.Mana >= 70)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                                RestarSalud(oponenteActivo, personajeActivo);
                                break;
                            case 1:
                                BurlarDefensa(oponenteActivo, personajeActivo, movimientosPorClave[2]);
                                break;
                            case 2:
                                AumentarDefensa(oponenteActivo, movimientosPorClave[3]); //+8
                                break;
                        }
                    }
                    else if (oponenteActivo.Caracteristicas.Mana >= 40)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                            case 1:
                                RestarSalud(oponenteActivo, personajeActivo);
                                break;
                            case 2:
                                AumentarDefensa(oponenteActivo, movimientosPorClave[4]); //+4
                                break;
                        }
                    }
                    else
                    {
                        RestarSalud(oponenteActivo, personajeActivo);
                    }
                }
                else if (oponenteActivo.Caracteristicas.Salud > 25)
                {
                    if (oponenteActivo.Caracteristicas.Mana >= 50)
                    {
                        switch (rdm.Next(4))
                        {
                            case 0:
                                RestarSalud(oponenteActivo, personajeActivo);
                                break;
                            case 1:
                                BurlarDefensa(oponenteActivo, personajeActivo, movimientosPorClave[2]);
                                break;
                            case 2:
                                AumentarSalud(oponenteActivo, movimientosPorClave[5]); //+25
                                break;
                            case 3:
                                AumentarSalud(oponenteActivo, movimientosPorClave[6]); //+15
                                break;
                        }
                    }
                    else if (oponenteActivo.Caracteristicas.Mana >= 30)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0:
                                RestarSalud(oponenteActivo, personajeActivo);
                                break;
                            case 1:
                                AumentarSalud(oponenteActivo, movimientosPorClave[6]); //+15
                                break;
                            case 2:
                                AumentarDefensa(oponenteActivo, movimientosPorClave[4]); //+4
                                break;
                        }
                    }
                    else
                    {
                        RestarSalud(oponenteActivo, personajeActivo);
                    }
                }
                else if (oponenteActivo.Caracteristicas.Salud <= 25)
                {
                    if (personajeActivo.Caracteristicas.Salud <= 15)
                    {
                        if (oponenteActivo.Caracteristicas.Mana >= 30)
                        {
                            BurlarDefensa(oponenteActivo, personajeActivo, movimientosPorClave[2]);
                        }
                        else
                        {
                            RestarSalud(oponenteActivo, personajeActivo);
                        }
                    }
                    else
                    {
                        if (oponenteActivo.Caracteristicas.Mana >= 15)
                        {
                            if ((personajeActivo.Caracteristicas.Salud - oponenteActivo.Caracteristicas.Salud) > 12)
                            {
                                if (oponenteActivo.Caracteristicas.Mana >= 40)
                                {
                                    AumentarSalud(oponenteActivo, movimientosPorClave[5]); //+25
                                }
                                else
                                {
                                    AumentarSalud(oponenteActivo, movimientosPorClave[6]); //+15
                                }
                            }
                            else
                            {
                                RestarSalud(oponenteActivo, personajeActivo);
                            }
                        }
                        else
                        {
                            RestarSalud(oponenteActivo, personajeActivo);
                        }
                    }
                }
            }
        }

        private bool ContinuarCombate()
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

        private Personaje IntercambiarPersonaje(Personaje personajeActual, Personaje personajeACambiar)
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

        private void RestarSalud(Personaje atacante, Personaje defensor)
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

        private bool GolpeCritico(Personaje atacante)
        {
            double probabilidadCritico = atacante.Caracteristicas.Precision / 100.0;
            bool golpeCritico = (new Random().NextDouble() < probabilidadCritico); //NextDouble genera un numero entre 0.0 y 1.0
            return golpeCritico;
        }

        private void BurlarDefensa(Personaje atacante, Personaje defensor, Movimientos movimiento)
        {
            int dañoRealizado = atacante.Caracteristicas.Daño;
            defensor.Caracteristicas.Salud -= dañoRealizado;
            atacante.Caracteristicas.Mana -= movimiento.CostoMana;

            Console.WriteLine($"\t\t\nBurlaste la defensa enemiga e hiciste {dañoRealizado} de daño!");
        }

        private void AumentarSalud(Personaje atacante, Movimientos movimiento)
        {
            atacante.Caracteristicas.Salud += movimiento.CantidadAumento;
            atacante.Caracteristicas.Mana -= movimiento.CostoMana;

            Console.WriteLine($"\t\t\nBien! Aumentaste +{movimiento.CantidadAumento} tu salud, ahora tienes {atacante.Caracteristicas.Salud}");
        }

        private void AumentarDefensa(Personaje atacante, Movimientos movimiento)
        {
            atacante.Caracteristicas.Defensa += movimiento.CantidadAumento;
            atacante.Caracteristicas.Mana -= movimiento.CostoMana;

            Console.WriteLine($"\t\t\nBien jugado! Aumentaste +{movimiento.CantidadAumento}, ahora tu defensa es de {atacante.Caracteristicas.Defensa}");
        }

        private void MostrarDatosCombate(Personaje atacante, Personaje defensor)
        {
            Console.WriteLine($"| {atacante.Datos.Nombre.ToUpper()} |\n");
            Console.WriteLine($"\tMana disponible: {atacante.Caracteristicas.Mana}\n");
            Console.WriteLine($"\tSalud actual: {atacante.Caracteristicas.Salud}");
            Console.WriteLine($"\tSalud del oponente: {defensor.Caracteristicas.Salud}\n");
            MostrarMovimientos();
        }

        private void MostrarMovimientos()
        {
            var categorias = movimientosPorClave.GroupBy(kvp => kvp.Value.Categoria); //Agrupo los movimientos segun sus categorias

            foreach (var categoria in categorias)
            {
                Console.WriteLine($"\t──── {categoria.Key.ToUpper()} ────"); //Obtengo la clave del grupo (categoria)
                foreach (var kvp in categoria)
                {
                    Console.WriteLine($"\t{kvp.Key}- '{kvp.Value.Nombre}' | {kvp.Value.Descripcion}");
                }
                Console.WriteLine();
            }
        }

        private Dictionary<int, Movimientos> CrearClavesMovimientos()
        {
            var listaMovimientos = CrearListaMovimientos();
            var movimientosPorClave = new Dictionary<int, Movimientos>();
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

        private List<Movimientos> CrearListaMovimientos()
        {
            return new List<Movimientos>
            {
                new Movimientos("Ataque normal", "Inflige daño a tu Oponente", "Ataque", 0, 0),
                new Movimientos("Carga abrasadora", "Burla la defensa enemiga e inflinge todo el daño posible (-30 de Mana)", "Ataque", 0, 30),
                new Movimientos("Protección Divina", "+8 de Defensa (-20 de Mana)", "Defensa", 8, 20),
                new Movimientos("Elixir de vida", "+25 de Salud (-25 de Mana)", "Salud", 25, 25),
                new Movimientos("Barrera mágica", "+4 de Defensa (-10 de Mana)", "Defensa", 4, 10),
                new Movimientos("Poción de curación", "+15 de salud (-15 de Mana)", "Salud", 15, 15)
            };
        }
    }
}