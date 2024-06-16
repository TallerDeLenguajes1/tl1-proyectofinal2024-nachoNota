using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlamadaAPI;

var ListaNombresDescripciones = await APICLIENT.GetCharactersAsync();

foreach (var Personaje in ListaNombresDescripciones.data.Resultados)
{
    if(Personaje.Descripcion  != "")
    {
        Console.WriteLine($"Name: {Personaje.Nombre}, Description: {Personaje.Descripcion}\n");
    }
}