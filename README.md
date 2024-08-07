# AVENGERS: THE NEW MULTIVERSE
## Descripci�n general
Este juego trata sobre un multiverso completamente nuevo en el que los personajes m�s poderosos del UCM pelean entre s� para poder quedarse con la victoria, y as� dejar su nombre grabado en el recuerdo de todos. Para este juego se ha utilizado una API de Marvel, la cual, entre mucha otra informaci�n, brinda el nombre y una breve descripci�n de los personajes mas ic�nicos de la franquicia. Si queres probarla, ac� tenes el link:  
https://developer.marvel.com/docs

## Mec�nicas de juego
Las mec�nicas de juego son simples. Ingresas a nuestro juego y debes elegir una de las opciones para comenzar a batallar con el personaje que quieras. Tenemos dos opciones para batallar:  
- TORNEO: En esta modalidad ingresar�s con el personaje de tu elecci�n y deber�s abrir tu camino a la final midiendote con distintos personajes, cada uno con su propio nivel, que ir� aumentando progresivamente a medida que avances.
- COMBATE 2V2: Por otra parte, puedes llevar a cabo un combate por equipos entre 4 personajes (2 equipos), en el que puedes elegir 2 heroes o villanos de tu preferencia para unir fuerzas. Puedes adem�s elegir la dificultad del combate, dependiendo tu nivel de juego. Ya dentro del combate, puedes intercambiar con tu compa�ero en cualquier momento de la batalla.    

CAMPEONES HISTORICOS: Al ingresar a esta opci�n, puedes consultar sobre los campeones que ganaron el gran torneo en toda su historia, pudiendo adem�s ver la fecha y hora exactas en las que se consagraron victoriosos.

Ya dentro de la batalla, tienes distintos movimientos para elegir dependiendo de la cantidad de man� disponible que tengas. Puedes elegir entre ir al frente y atacar, curarte en el caso de que tengas poca vida, o aumentar tu defensa para que tu oponente haga menos da�o al atacarte.

## Estructura general
Este proyecto presenta dos carpetas y distintas clases para asegurar el correcto funcionamiento de este proyecto.  

###  Combate  
- *Combate.cs*: Esta clase presenta los distintos m�todos para que el combate entre los personajes funcione, como la l�gica del da�o que se realiza al atacar, o c�mo est� estructurada la inteligencia del oponente para saber qu� movimiento realizar, etc.
- *Movimientos.cs*: Una clase para inicializar los movimientos, en los que cada uno presenta su propio nombre, descripcion, costo de man�, y una categor�a asignada seg�n la funci�n del movimiento.   

### Personaje  
- *Datos.cs*: Son los datos del personaje en particular, los cuales incluyen su nombre, una corta descripci�n del mismo y la fecha en la que salieron campeones del torneo (si es que lo hicieron). 	
- *Caracteristicas.cs*: Contiene todo lo relacionado a la cantidad de da�o y defensa que vaya a tener un personaje en particular.
- *FabricaDePersonajes.cs*: En esta parte se encuentran los metodos para crear distintos personajes dependiendo de si se pudo o no realizar la conexion a la API.

### Funciones Varias
- *FuncionesTexto.cs*: Refiere a todos los metodos que muestran distintas cosas por pantalla.
- *HelperJson.cs*: Contiene metodos para poder abrir y guardar archivos JSON.
- *DatosAPI.cs:* Contiene las clases necesarias para poder acceder a los personajes devueltos desde la Api de Marvel (si es que pudo haber conexi�n) en los que obtengo el nombre y la descripcion de los mismos.
- *ValidarOpciones:* En esta parte est�n todas las opciones que se deben tener en cuenta en el caso que el usuario ingrese un valor no v�lido dentro del contexto del juego.

### Principales
- *Program.cs*: Es el programa principal desde donde se llama al metodo EmpezarNuevoJuego proveniente de la clase Juego, m�todo el cu�l, al ser llamado, se puede dar inicio al juego en cuesti�n
- *Juego.cs*: Contiene la l�gica principal del juego, coordinando las distintas partes y asegurando el flujo correcto desde el inicio hasta el final del juego. Contiene metodos como para empezar un nuevo torneo, para un combate 2v2 o para mostrar los campeones historicos del torneo.