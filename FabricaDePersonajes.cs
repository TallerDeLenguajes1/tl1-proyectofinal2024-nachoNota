using System;

public class FabricaDePersonajes()
{
    public Personaje CrearPersonaje(Datos datos){
        var Caracteristicas = new Caracteristicas();
        return new Personaje(datos, Caracteristicas);
    }

    public void MostrarPersonaje(Personaje personaje)
    {
        Console.WriteLine($"Nombre: {personaje.DatosPersonaje.Nombre}\n" +
                        $"Descripcion: {personaje.DatosPersonaje.Descripcion}\n" +
                        $"Salud: {personaje.CaracteristicasPersonaje.Salud}\n" +
                        $"Mana: {personaje.CaracteristicasPersonaje.Mana}\n" +
                        $"Daño: {personaje.CaracteristicasPersonaje.Daño}\n" +
                        $"Defensa: {personaje.CaracteristicasPersonaje.Defensa}");
        Console.WriteLine("===========================================");
    }
}

public class Personaje
{
            
    public Datos DatosPersonaje { get; set; }
    public Caracteristicas CaracteristicasPersonaje { get; set; }

    public Personaje(Datos DatosPersonaje, Caracteristicas CaracteristicasPersonaje)
    {
        this.DatosPersonaje = DatosPersonaje;
        this.CaracteristicasPersonaje = CaracteristicasPersonaje;
    }

    
}