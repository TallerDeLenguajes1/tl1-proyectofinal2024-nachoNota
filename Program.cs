using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlamadaAPI;

var ListaNombresDescripciones = await APICLIENT.GetCharactersAsync();

//Creo personajes con sus datos y caracteristicas y hago lista que los contenga
var Fabrica = new FabricaDePersonajes();
var ListaDePersonajes = new List<Personaje>();
foreach (var Personaje in ListaNombresDescripciones.data.Resultados)
{
    if(Personaje.Descripcion != "")
    {        
        var PersonajeCreado = Fabrica.CrearPersonaje(new Datos(Personaje.Nombre, Personaje.Descripcion));
        ListaDePersonajes.Add(PersonajeCreado);
        Fabrica.MostrarPersonaje(PersonajeCreado); 
    } 
}
