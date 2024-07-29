# AVENGERS: A NEW MULTIVERSE
## Descripci�n general
Este juego trata sobre un multiverso completamente nuevo en el que los personajes m�s poderosos del UCM pelean entre s� para poder quedarse con la victoria, y as� dejar su nombre grabado en el recuerdo de todos. 

## Mec�nicas de juego
Las mec�nicas de juego son simples. Ingresas a nuestro juego y debes elegir una de las opciones para comenzar a batallar con el personaje que quieras. Tenemos dos opciones para batallar:  
- TORNEO: En esta modalidad ingresar�s con el personaje de tu elecci�n y deber�s abrir tu camino a la final midiendote con distintos personajes, cada uno con su propio nivel, que ir� aumentando progresivamente a medida que avances.
- COMBATE 1V1: Por otra parte, puedes llevar a cabo un combate cara a cara con un oponente de tu eleccion, pudiendo tambi�n ponerle el nivel que quieras para probarte a ti mismo.  

Ya dentro de la batalla, tienes distintos movimientos para elegir dependiendo de la cantidad de man� disponible que tengas. Puedes elegir entre ir al frente y atacar, curarte en el caso de que tengas poca vida, o aumentar tu defensa para que tu oponente haga menos da�o al atacarte.

## Estructura general
Este proyecto presenta dos carpetas y distintas clases para asegurar el correcto funcionamiento de este proyecto.
- **Combate**  
	- *Combate.cs*: Esta clase presenenta los distintos m�todos para que el combate entre los personajes funcione, como la l�gica del da�o que se realiza al atacar, o c�mo est� estructurada la inteligencia del oponente para saber qu� movimiento realizar, etc.
	- *Movimientos.cs*: Una clase para inicializar los movimientos, en los que cada uno presenta su propio nombre, descripcion, costo de man�, y una categor�a asignada seg�n la funci�n del movimiento. 
- **Personaje**  
	- *Datos.cs*: Son los datos del personaje en particular, los cuales incluyen su nombre, una corta descripci�n del mismo 	