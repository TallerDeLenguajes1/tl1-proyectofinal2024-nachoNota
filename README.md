# AVENGERS: A NEW MULTIVERSE
## Descripción general
Este juego trata sobre un multiverso completamente nuevo en el que los personajes más poderosos del UCM pelean entre sí para poder quedarse con la victoria, y así dejar su nombre grabado en el recuerdo de todos. Para este juego se ha utilizado una API de Marvel, la cual, entre mucha otra información, brinda el nombre y una breve descripción de los personajes mas icónicos de la franquicia. Si queres probarla, acá tenes el link:  
https://developer.marvel.com/docs

## Mecánicas de juego
Las mecánicas de juego son simples. Ingresas a nuestro juego y debes elegir una de las opciones para comenzar a batallar con el personaje que quieras. Tenemos dos opciones para batallar:  
- TORNEO: En esta modalidad ingresarás con el personaje de tu elección y deberás abrir tu camino a la final midiendote con distintos personajes, cada uno con su propio nivel, que irá aumentando progresivamente a medida que avances.
- COMBATE 2V2: Por otra parte, puedes llevar a cabo un combate por equipos entre 4 personajes (2 equipos), en el que puedes elegir 2 heroes o villanos de tu preferencia para que unan fuerzas y se abran paso a la victoria. Puedes además elegir la dificultad del combate, dependiendo tu nivel de juego.    

Ya dentro de la batalla, tienes distintos movimientos para elegir dependiendo de la cantidad de maná disponible que tengas. Puedes elegir entre ir al frente y atacar, curarte en el caso de que tengas poca vida, o aumentar tu defensa para que tu oponente haga menos daño al atacarte.

## Estructura general
Este proyecto presenta dos carpetas y distintas clases para asegurar el correcto funcionamiento de este proyecto.  

###  Combate  
- *Combate.cs*: Esta clase presenenta los distintos métodos para que el combate entre los personajes funcione, como la lógica del daño que se realiza al atacar, o cómo está estructurada la inteligencia del oponente para saber qué movimiento realizar, etc.
- *Movimientos.cs*: Una clase para inicializar los movimientos, en los que cada uno presenta su propio nombre, descripcion, costo de maná, y una categoría asignada según la función del movimiento.   

### Personaje  
- *Datos.cs*: Son los datos del personaje en particular, los cuales incluyen su nombre, una corta descripción del mismo 	
- *Caracteristicas.cs*: Contiene todo lo relacionado a la cantidad de daño y defensa que vaya a tener un personaje en particular.
- *FabricaDePersonajes.cs*: En esta parte se encuentran los metodos para crear distintos personajes dependiendo de pudo realizarse o no la conexion a la API.

### Funciones Varias
- *FuncionesTexto.cs*: Refiere a todos los metodos que muestran distintas cosas por pantalla.
- *HelperJson.cs*: Contiene metodos para poder abrir y guardar archivos JSON.
- *LlamadaAPI:* *LlamadaAPI.cs*: Contiene la clase y métodos necesarios para realizar la llamada a la API de Marvel y obtener los datos de los personajes.
- *ValidarOpciones:* En esta parte están todas las opciones que se deben tener en cuenta en el caso que el usuario ingrese un valor no válido dentro del contexto del juego.

