using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlamadaAPI;

var ListaNombresDescripciones = await APICLIENT.GetCharactersAsync();
List<Datos> ListaDatosPersonajes = new List<Datos>(); 

//Creo una lista con los datos de cada personaje (Nombre y Descripcion)
foreach (var Personaje in ListaNombresDescripciones.data.Resultados)
{
    if(Personaje.Descripcion != "") ListaDatosPersonajes.Add(new Datos(Personaje.Nombre, Personaje.Descripcion));
}