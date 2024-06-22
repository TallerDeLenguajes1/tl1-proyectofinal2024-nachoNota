using System;

public class Caracteristicas
{
    private int salud { get; set; }
    private int daño { get; set; }
    private int defensa { get; set; }
    private int mana { get; set; }
    private List<Especial> especiales { get; set; }

    public int Salud { get => salud; set => salud = value; }
    public int Daño { get => daño; set => daño = value; }
    public int Defensa { get => defensa; set => defensa = value; }
    public int Mana { get => mana; set => mana = value; }
    public List<Especial> Especiales { get => especiales; set => especiales = value; }

    public Caracteristicas()
    {
        Salud = 100;
        Mana = 100;
        Daño = GenerarAleatorio();
        Defensa = GenerarAleatorio();
        Especiales = new List<Especial>
        {
            new Especial("Curacion", "+25 de Salud (-50 de Mana)", 0, 0, 25),
            new Especial("Proteccion Divina", "+25 de Defensa (-50 de Mana)", 0, 25, 0),
            new Especial("Corte Profundo", "25 de daño (-50 de Mana)", 25, 0, 0)
        };
    }

    Random rdm = new Random();

    public int GenerarAleatorio()
    {
        return rdm.Next(10, 20);
    }

}

public class Especial
{
    private string nombre { get; set; }
    private string descripcion {  get; set; }
    private int daño {  get; set; }
    private int defensa { get; set; }
    private int salud { get; set; }
    private int costoMana { get; set; }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Daño { get => daño; set => daño = value; }
    public int Defensa { get => defensa; set => defensa = value; }
    public int Salud { get => salud; set => salud = value; }
    public int CostoMana { get => costoMana; set => costoMana = value; }

    public Especial(string Nombre, string Descripcion, int Daño, int Defensa, int Salud)
    {
        this.Nombre = Nombre;
        this.Descripcion = Descripcion;
        this.Daño = Daño;
        this.Defensa = Defensa;
        CostoMana = 50;
    }
}