using System.Text.Json;
using MovimientosCombate;
using MiProyecto.FabricaDePersonajes;

namespace VentanaCombate
{
    public class Combate
    {
        public async Task NuevoCombate(Personaje PersonajeElegido, Personaje Oponente, Dictionary<int, Movimientos> MovimientosPorClave)
        {
            bool Turno = true;
            var Texto = new FuncionesTexto();
            while (PersonajeElegido.Caracteristicas.Salud > 0 && Oponente.Caracteristicas.Salud > 0)
            {
                if (Turno)
                {
                    Console.WriteLine("\t\nEs tu turno! Realiza un movimiento!\n");

                    Texto.MostrarDatosCombate(PersonajeElegido, Oponente, MovimientosPorClave);

                    var (OpcionElegida, MovimientoElegido) = new ValidarOpciones().ValidarMovimiento(MovimientosPorClave);

                    Console.Clear();

                    Turno = TurnoJugador(PersonajeElegido, Oponente, OpcionElegida, MovimientoElegido);
                }
                else
                {
                    Console.WriteLine("\t\nAhora estas viendo la pantalla del oponente, espera a que realice un movimiento...\n");

                    Texto.MostrarDatosCombate(Oponente, PersonajeElegido, MovimientosPorClave);

                    await Task.Delay(3000);

                    Console.Clear();

                    TurnoOponente(Oponente, PersonajeElegido, MovimientosPorClave);

                    Turno = true;
                }
                await Task.Delay(3000);

                Console.Clear();
            }
        }

        public bool TurnoJugador(Personaje atacante, Personaje defensor, int opcion, Movimientos movimiento)
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

        public void TurnoOponente(Personaje Oponente, Personaje PersonajeElegido, Dictionary<int, Movimientos> MovimientosPorClave)
        {
            if(Oponente.Caracteristicas.Mana <= 10)
            {
                RestarSalud(Oponente, PersonajeElegido);
            }
            else
            {
                var rdm = new Random();
                if(Oponente.Caracteristicas.Salud >= 70)
                {
                    if(Oponente.Caracteristicas.Mana >= 70)
                    {
                        switch(rdm.Next(3))
                        {
                            case 0: RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1: BurlarDefensa(Oponente, PersonajeElegido, MovimientosPorClave[2]);
                                break;
                            case 2:
                                AumentarDefensa(Oponente, MovimientosPorClave[3]); //+8
                                break;
                        }
                    }
                    else
                    {
                        switch(rdm.Next(3))
                        {
                            case 0:
                            case 1: RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 2: AumentarDefensa(Oponente, MovimientosPorClave[4]); //+4
                                break;
                        }
                    }
                } else if(Oponente.Caracteristicas.Salud >= 50)
                {
                    if(Oponente.Caracteristicas.Mana >= 70)
                    {
                        switch(rdm.Next(3))
                        {
                            case 0: RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1: BurlarDefensa(Oponente, PersonajeElegido, MovimientosPorClave[2]);
                                break;
                            case 2: AumentarDefensa(Oponente, MovimientosPorClave[3]); //+8
                                break;
                        }
                    } else if(Oponente.Caracteristicas.Mana >= 40)
                    {
                        switch(rdm.Next(3))
                        {
                            case 0:
                            case 1: RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 2: AumentarDefensa(Oponente, MovimientosPorClave[4]); //+4
                                break;
                        }
                    }
                    else
                    {
                        RestarSalud(Oponente, PersonajeElegido);
                    }
                } else if(Oponente.Caracteristicas.Salud > 25)
                {
                    if(Oponente.Caracteristicas.Mana >= 50)
                    {
                        switch(rdm.Next(4))
                        {
                            case 0: RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1: BurlarDefensa(Oponente, PersonajeElegido, MovimientosPorClave[2]);
                                break;
                            case 2: AumentarSalud(Oponente, MovimientosPorClave[5]); //+25
                                break;
                            case 3: AumentarSalud(Oponente, MovimientosPorClave[6]); //+15
                                break;
                        }
                    } else if(Oponente.Caracteristicas.Mana >= 30)
                    {
                        switch (rdm.Next(3))
                        {
                            case 0: RestarSalud(Oponente, PersonajeElegido);
                                break;
                            case 1: AumentarSalud(Oponente, MovimientosPorClave[6]); //+15
                                break;
                            case 2: AumentarDefensa(Oponente, MovimientosPorClave[4]); //+4
                                break;
                        }
                    }
                    else
                    {
                        RestarSalud(Oponente, PersonajeElegido);
                    }
                } else if(Oponente.Caracteristicas.Salud <= 25)
                {
                    if(PersonajeElegido.Caracteristicas.Salud <= 15)
                    {
                        if(Oponente.Caracteristicas.Mana >= 30)
                        {
                            BurlarDefensa(Oponente, PersonajeElegido, MovimientosPorClave[2]);
                        } else
                        {
                            RestarSalud(Oponente, PersonajeElegido);
                        }
                    } else
                    {
                        if(Oponente.Caracteristicas.Mana >= 15)
                        {
                            if((PersonajeElegido.Caracteristicas.Salud - Oponente.Caracteristicas.Salud) > 12)
                            {
                                if (Oponente.Caracteristicas.Mana >= 40)
                                {
                                    AumentarSalud(Oponente, MovimientosPorClave[5]); //+25
                                } else
                                {
                                    AumentarSalud(Oponente, MovimientosPorClave[6]); //+15
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

        public void RestarSalud(Personaje atacante, Personaje defensor)
        {
            int dañoRealizado = (int)(atacante.Caracteristicas.Daño - defensor.Caracteristicas.Defensa * 0.3);

            if (GolpeCritico(atacante))
            {
                dañoRealizado = (int) (dañoRealizado * 1.35);
                Console.WriteLine($"\t\t\nGOLPE CRITICO!!! Haz realizado {dañoRealizado} de daño a tu oponente");
            } else
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
            if(atacante.Caracteristicas.Mana < 0)
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

        public bool EsGanador(Personaje personaje)
        {
            return (personaje.Caracteristicas.Salud > 0) ? true : false;
        }

        public Dictionary<int, Movimientos> CrearClavesMovimientos()
        {
            var listaMovimientos = CrearListaMovimientos();
            var movimientosPorClave = new Dictionary<int, Movimientos>();
            var categorias = listaMovimientos.GroupBy(m => m.Categoria);
            int i = 1;

            foreach(var categoria in categorias)
            {
                foreach(var movimiento in categoria)
                {
                    movimientosPorClave.Add(i, movimiento);
                    i++;
                }
            }
            return movimientosPorClave;
        }

        public void GuardarCampeon(string nombreArchivo, Personaje campeon)
        {
            var jsonHelper = new HelperJson();
            var listaCampeones = new List<Personaje>();

            if (!File.Exists(nombreArchivo))
            {
                listaCampeones.Add(campeon);
                string stringJson = JsonSerializer.Serialize(listaCampeones);
                jsonHelper.GuardarArchivo(nombreArchivo, stringJson);
            }
            else
            {
                string recuperadoJson = jsonHelper.AbrirArchivo(nombreArchivo);

                var listaRecuperada = JsonSerializer.Deserialize<List<Personaje>>(recuperadoJson);

                listaRecuperada.Add(campeon);

                string stringJsonNuevo = JsonSerializer.Serialize(listaRecuperada);

                jsonHelper.GuardarArchivo(nombreArchivo, stringJsonNuevo);
            }
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