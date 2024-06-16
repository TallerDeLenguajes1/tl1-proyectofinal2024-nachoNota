using System;

public class Caracteristicas
{
    private int salud { get; set; }
    private int daño { get; set; }
    private int defensa { get; set; }
    private int mana { get; set; }

    public int Salud { get => salud; set => salud = value; }
    public int Daño { get => daño; set => daño = value; }
    public int Defensa { get => defensa; set => defensa = value; }
    public int Mana { get => mana; set => mana = value; }

    public Caracteristicas()
    {
        Salud = 100;
        Mana = 100;
        Daño = GenerarAleatorio();
        Defensa = GenerarAleatorio();
    }

    Random rdm = new Random();

    public int GenerarAleatorio()
    {
        return rdm.Next(10, 20);
    }


}