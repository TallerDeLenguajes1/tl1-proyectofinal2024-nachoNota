using System;

namespace CaracteristicasPersonaje
{
    public class Caracteristicas
    {
        private int valorBase;
        private int agilidad;
        private int resistencia;
        private int fuerza;
        private int salud;

        public int Salud
        {
            get { return salud; }
            set
            {
                if(value < 0)
                {
                    salud = 0;
                }
                else
                {
                    salud = value;
                }
            }
        }
        public int Daño { get; set; }
        public int Defensa { get; set; }
        public int Mana { get; set; }
        public int Nivel { get; set; }
        public int Precision { get; set; }

        public Caracteristicas()
        {
            valorBase = 100;
            Salud = valorBase;
            Mana = valorBase;
            Nivel = 1;
            agilidad = GenerarAleatorio(12, 19);
            fuerza = GenerarAleatorio(15, 24);
            Precision = GenerarAleatorio(21, 26);
            resistencia = GenerarAleatorio(10, 20);
            Defensa = CalcularDefensa();
            Daño = CalcularDaño(); 
        }

        private int GenerarAleatorio(int min, int max)
        {
            return new Random().Next(min, max);
        }

        private int CalcularDaño()
        {
            int dañoBase = 10;
            double multiplicadorFuerza = 0.35;
            double multiplicadorAgilidad = 0.2;
            double multiplicadorPrecision = 0.1;
            int componenteAleatorio = GenerarAleatorio(1, 4);

            return (int)(dañoBase + (fuerza * multiplicadorFuerza) + (agilidad * multiplicadorAgilidad) + (Precision * multiplicadorPrecision) + componenteAleatorio);
        }

        private int CalcularDefensa()
        {
            int defensaBase = 12;
            double multiplicadorAgilidad = 0.4;
            double multiplicadorResistencia = 0.6;
            int componenteAleatorio = GenerarAleatorio(1, 4);
            return (int)(defensaBase + (agilidad * multiplicadorAgilidad) + (resistencia * multiplicadorResistencia) + componenteAleatorio);
        }

        public void BalancearEstadisticas()
        {
            if(Daño <= 23 && Defensa <= 26)
            {
                Daño += 2;
                Defensa += 3;
            }
        }

        public void ReestablecerEstadisticas()
        {
            Salud = 100;
            Mana = valorBase;
        }

        public void SubirNivelOponente()
        {
            Nivel++;

            Salud += 10;
            Mana += 5;

            agilidad += GenerarAleatorio(2, 5);
            fuerza += GenerarAleatorio(2, 5);
            Precision += GenerarAleatorio(2, 5);
            
            Daño = CalcularDaño();
        }

        public void SubirEstadisticasJugador()
        {
            ReestablecerEstadisticas();

            valorBase += 10;

            Mana = valorBase;
            Precision += 5;
        }
    }
}