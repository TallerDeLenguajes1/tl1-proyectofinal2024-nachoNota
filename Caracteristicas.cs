using System;

public class Caracteristicas
{
    private int valorBase {  get; set; }
    private int salud { get; set; }
    private int daño { get; set; }
    private int defensa { get; set; }
    private int mana { get; set; }
    private int nivel { get; set; }
    private int agilidad { get; set; }
    private int fuerza { get; set; }
    private int precision { get; set; }
    private int resistencia { get; set; }

    public int Salud { get => salud; set => salud = value; }
    public int Daño { get => daño; set => daño = value; }
    public int Defensa { get => defensa; set => defensa = value; }
    public int Mana { get => mana; set => mana = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Agilidad { get => agilidad; set => agilidad = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Precision { get => precision; set => precision = value; }
    public int Resistencia { get => resistencia; set => resistencia = value; }

    public Caracteristicas()
    {
        valorBase = 100;
        salud = valorBase;
        mana = valorBase;
        nivel = 1;
        agilidad = GenerarAleatorio(12, 19);
        fuerza = GenerarAleatorio(15, 24);
        precision = GenerarAleatorio(21, 26);
        resistencia = GenerarAleatorio(10, 20);
        defensa = CalcularDefensa();
        daño = CalcularDaño(); 
    }

    public int GenerarAleatorio(int min, int max)
    {
        return new Random().Next(min, max);
    }

    public int CalcularDaño()
    {
        int dañoBase = 10;
        double multiplicadorFuerza = 0.35;
        double multiplicadorAgilidad = 0.2;
        double multiplicadorPrecision = 0.1;
        int componenteAleatorio = GenerarAleatorio(1, 4);

        return (int)(dañoBase + (fuerza * multiplicadorFuerza) + (agilidad * multiplicadorAgilidad) + (precision * multiplicadorPrecision) + componenteAleatorio);
    }

    public int CalcularDefensa()
    {
        int defensaBase = 12;
        double multiplicadorAgilidad = 0.4;
        double multiplicadorResistencia = 0.6;
        int componenteAleatorio = GenerarAleatorio(1, 4);
        return (int)(defensaBase + (agilidad * multiplicadorAgilidad) + (resistencia * multiplicadorResistencia) + componenteAleatorio);
    }

    public void BalancearEstadisticas()
    {
        if(daño <= 23 && defensa <= 26)
        {
            daño += 2;
            defensa += 3;
        }
    }

}