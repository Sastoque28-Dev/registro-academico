# APUNTES

## - Que es .NET
Una herramienta que permita la ejecucion y compilacion de C# *(CShrarp)*.
Este mismo provee bibliotecas que se pueden reutilizar en codigo para un codigo
mas optimizado.

## - Que es C#
Lenguage de codigo orientado a la creacion de objetos, este lenguage es fuertemente
tipado (int,float,bool,string,char). Funciona apartir de la herramienta .NET.

## - Que es Node
Herramienta que nos permite el hacer uso del lenguage de programacion JavaScript fuera
de los navegadores.

## - Que es JVM (Java Virtual Machine)
Como su nombre lo dice, es una maquina virtual que simula y ejecuta codigo de Java,
siendo intermediado con el hardware.

## - Entity Framework Core
Esta herramienta es la que permite la traduccion de objetos de C# y SQL.
Funcionando igualmente en vicebersa.

## - Inyeccion de dependencias
 **API -> CORE -> DATA -> SQL**
Cada capa del proyecto cumple con una funcion y comparten con su dependencia.
API recibe una peticion por parte del Frontend y esta utiliza a CORE al estar conectados. CORE contiene 
los conceptos, logica de negocio y los objetos descritos para hacer uso de la base de datos, utilizando a DATA para que esta
con su configuracion de EF *(Entity Framework)* traduzca lo enviado a SQL, devolviendo esa informacion en tabla (filas, columnas).
resultando en API en una respuesta tipo JSON.




### Pendiente
Profundizar y practicar con los anteriores conceptos.