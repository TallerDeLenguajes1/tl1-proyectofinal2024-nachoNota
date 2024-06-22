using System;
using System.Collections.Generic;
using System.Threading.Tasks;


Console.Clear();
Random rdm = new Random(); //Para seleccionar personajes al azar
var Texto = new FuncionesTexto(); //Para las funciones de texto

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
    } 
}


//Muestro el menu con las opciones a elegir 
int opcion_menu = 0;
bool opcion_valida = false;
Texto.Logo();
Texto.Menu();

while (!opcion_valida)
{
    Console.WriteLine("Elija una de las opciones: ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out opcion_menu) && (opcion_menu >= 1 && opcion_menu <= 3))
    {
        opcion_valida = true;
    }
    else
    {
        Console.WriteLine("Por favor, ingrese un número válido entre 1 y 3.");
    }
}

Console.Clear();

//Muestro los personajes para que el usuario pueda elegir el suyo
int opcion_personaje = 0;
opcion_valida = false;
Texto.MostrarPersonajes(ListaDePersonajes);

while (!opcion_valida)
{
    Console.WriteLine("Con qué personaje quiere pelear? (ingresar su numero): ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out opcion_personaje) && (opcion_personaje >= 1 && opcion_personaje <= 8))
    {
        opcion_valida = true;
    }
    else
    {
        Console.WriteLine("Por favor, ingrese un número válido entre 1 y 8.");
    }
}

Console.Clear();

Personaje PersonajeElegido = ListaDePersonajes[opcion_personaje - 1];
Console.WriteLine($"\nMUY BIEN! El personaje elegido es: ");
Texto.MostrarPersonaje(PersonajeElegido, opcion_personaje);
ListaDePersonajes.Remove(PersonajeElegido);

switch (opcion_menu)
{
    case 1: 
            Console.WriteLine("Estamos armando el torneo, aguarde unos segundos.");
            await Task.Delay(10000);

            Console.Clear();

            Console.WriteLine("En cuartos de final tendras que enfrentarte a...");
            await Task.Delay(4000);
            int random = rdm.Next(0, 6);
            Personaje PersonajeACombatir = ListaDePersonajes[random];
            Texto.MostrarPersonaje(PersonajeACombatir, random+1);
            ListaDePersonajes.Remove(PersonajeACombatir);
            await Task.Delay(5000);

            Console.Clear();

            Console.WriteLine("\nComienza el combate!!!");

            await Task.Delay(2000);

            Console.Clear();
        
            while (PersonajeElegido.CaracteristicasPersonaje.Salud > 0 && PersonajeACombatir.CaracteristicasPersonaje.Salud > 0)
            {
                
            }
        
        break;
    case 2: 
            await Task.Delay(5000);
            
        break;
    case 3: Console.WriteLine("\nEsperamos que vuelvas a jugar pronto, suerte!");
            await Task.Delay(3000);
        break;
}