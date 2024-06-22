using System;

public class FabricaDePersonajes()
{
    public Personaje CrearPersonaje(Datos datos){
        var Caracteristicas = new Caracteristicas();
        return new Personaje(datos, Caracteristicas);
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