using System.Text.Json;
using MovimientosCombate;
using MiProyecto.FabricaDePersonajes;

namespace VentanaCombate
{
    public class Combate
    {
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