﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PinPongC_
{
    internal class Program
    {
        static Timer reloj;
        static readonly object lockObject = new object();//Lock para evitar que el temporizador y el jugador varien la matriz a la vez
        static void Main(string[] args)
        {
            //Objetos y variables
            Console.CursorVisible = false;
            Tablero tablero = new Tablero();
            Pelota pelota = new Pelota(tablero.getY(), tablero.getX());
            Jugadores p1 = new Jugadores();
            Jugadores p2 = new Jugadores(1, pelota.getDireccionY());
            Puntos puntos = new Puntos();
            Random random = new Random();

            char[,] matriz = Tablero.DibujaTablero(tablero.getTablero());
            int yPlayer = tablero.getY() / 2;
            bool menu = false;
            bool jugando = false;
            bool pausarJugador = false;
            bool reseteaMarcador = false;
            bool final = false;
            bool cerrar = false;
            int marcadorP1 = 0, marcadorNpc = 0, ganador = 0, goal = 0;

            do
            {
                //Ajustes Temporizadores
                reloj = new Timer(Temporizador, null, 0, 300);

                //MÉTODO MUEVE LA PELOTA Y AL ENEMIGO EN TORNO A ESTA AUTOMÁTICAMENTE CADA X MILISEGUNDOS
                void Temporizador(object sender)
                {
                    marcadorP1 = puntos.getJugadorPts();
                    marcadorNpc = puntos.getNpcPts();

                    //IF E ELSE IF, DETECTA SI SE HA MARCADO GOL EN UNA U OTRA PORTERÍA Y LE DA EL GOL AL QUE LO HA METIDO,
                    //ADEMÁS COMPRUEBA SI HA LLEGADO AL LÍMITE DE PUNTUACIÓN, PARA ACABAR O RESETEAR PARTIDO
                    if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '[')
                    {
                        puntos.setJugadorPts();//Puntos Jugador ++
                        marcadorP1 = puntos.getJugadorPts();
                        goal = 1;
                        menu = true;

                        Goal();
                        if (marcadorP1 >= 1) //Si marcador es igual a número se llamará a Win()
                        {
                            ganador = 1;
                            Win();
                        } else Reset();
                    }
                    else if (matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == ']')
                    {
                        puntos.setNpcPts();//puntosNpc ++
                        marcadorNpc = puntos.getNpcPts();
                        goal = 2;
                        menu = true;

                        Goal();
                        if (marcadorNpc >= 1) //Si marcador es igual a número se llamará a Win()
                        {
                            ganador = 2;
                            Win();
                        } else Reset();
                    }

                    //Cambian la dirección de Y al tocar techo o suelo
                    if (matriz[pelota.getPelotaY() - 1, pelota.getPelotaX()] == '_') pelota.setDireccionY(2);
                    else if (matriz[pelota.getPelotaY() + 1, pelota.getPelotaX()] == '^') pelota.setDireccionY(1);

                    //Cambia la dirección de X e Y al tocar jugador
                    if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '|')
                    {
                        pelota.setDireccionX(1);
                        if (matriz[pelota.getPelotaY() - 1, pelota.getPelotaX()] != '_')//No cambia dirección si está cerca del techo o suelo
                            pelota.setDireccionY(random.Next(1, 3));//Toca jugador y dirección cambia a 1 o 2 aleatoriamente
                    }
                    else if (matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == '|')
                    {
                        pelota.setDireccionX(2);
                        if (matriz[pelota.getPelotaY() + 1, pelota.getPelotaX()] != '^')//No cambia dirección si está cerca del techo o suelo
                            pelota.setDireccionY(random.Next(1, 3));//Toca jugador y dirección cambia a 1 o 2 aleatoriamente
                    }

                    //Posición pelota se actualiza en base a los parámetros anteriores
                    pelota.CambiaY();
                    pelota.CambiaX();

                    //Matriz que será imprimida, jugador2 toma como parámetro la matriz devuelta por la pelota.
                    matriz = p2.Jugador2(pelota.PosicionaPelota(matriz), pelota.getPelotaY(), tablero.getX(), pelota.getDireccionY());

                    lock (lockObject) //Bloquea Imprime si ya lo está usando el do while de abajo
                    {
                        Tablero.Imprime(matriz, marcadorP1, marcadorNpc);
                    }
                }

                //BLOQUE MOVER JUGADOR CON TECLAS
                while (pausarJugador != true)
                {
                    if (!menu) //Evita que el jugador se mueva e imprima si hay algún menú activo
                    {
                        Thread.Sleep(100);//Velocidad mover jugador
                        matriz = Jugadores.Jugador1(matriz, ref yPlayer, tablero.getY(), ref jugando);

                        lock (lockObject) //Bloquea Imprime si ya lo está usando el temporizador
                        {
                            Tablero.Imprime(matriz, marcadorP1, marcadorNpc);
                        }
                    }
                };

                //MÉTODO WIN
                void Win()
                {
                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    Thread.Sleep(1000);
                    Console.Clear();

                    switch (ganador)
                    {
                        case 1:
                            Console.WriteLine($"Enhorabuena has ganado!\nRESULTADO: Jugador({marcadorP1}) - ({marcadorNpc})Npc");
                            gameOver();
                            break;
                        case 2:
                            Console.WriteLine($"Lo sentimos, has perdido, la próxima vez será!\nRESULTADO: Jugador({marcadorP1}) - ({marcadorNpc})Npc");
                            gameOver();
                            break;
                    }
                }

                //MÉTODO GOL
                void Goal()
                {
                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    marcadorP1 = puntos.getJugadorPts();
                    marcadorNpc = puntos.getNpcPts();

                    switch (goal)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"GOOOOOOL del Jugador\nEl marcador se posiciona {marcadorP1} a {marcadorNpc}");
                            Thread.Sleep(1500);
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine($"GOOOOOOL del Npc\nEl marcador se posiciona {marcadorP1} a {marcadorNpc}");
                            Thread.Sleep(1500);
                            break;
                    }
                }

                //MÉTODO GAME OVER, EL USUARIO ELIGE QUÉ HACER.
                void gameOver()
                {
                    Console.CursorVisible = true;
                    char tryAgain;
                    bool succes = false;

                    do
                    {
                        Console.WriteLine("El partido ha acabado, ¿quieres volver a jugar? (Y/N)");
                        succes = char.TryParse(Console.ReadLine().ToUpper(), out tryAgain);
                    } while (succes == false && (tryAgain != 'Y' || tryAgain != 'N'));

                    switch (tryAgain)
                    {
                        case 'Y':

                            reseteaMarcador = true;
                            Console.Clear();
                            Console.WriteLine("NUEVO PARTIDO\n(Ambos Marcadores a 0)");
                            Thread.Sleep(1000);
                            Reset();

                            break;
                        case 'N':

                            Console.Clear();
                            Console.WriteLine("Cerrando juego");
                            cerrar = true;
                            Thread.Sleep(1000);

                            break;
                    }

                    Console.CursorVisible = false;
                }

                //RESETEA EL TABLERO 
                void Reset()
                {
                    if (reseteaMarcador == true)
                    {
                        reseteaMarcador = false;
                        menu = false;
                        puntos.resetPts();
                        pelota.setPelotaX(tablero.getX() / 2);
                        pelota.setPelotaY(tablero.getY() / 2);
                        ganador = 0;
                        goal = 0;
                        
                        reloj.Change(0, 300);
                    }
                    else
                    {
                        menu = false;
                        pelota.setPelotaX(tablero.getX() / 2);
                        pelota.setPelotaY(tablero.getY() / 2);
                        reloj.Change(0, 300);
                    }
                }
            } while (cerrar != true);

            //TODO
            //Situación actual, todo funciona como esperado, excepto al celebrar gol, si pulsas una tecla el jugador imprime su matriz. También pasa en la segunda vez que abres el menú de volver a jugar.
            //1- Configurar velocidades y tamaño jugador
            //2- El rebote de la pelota que no siempre sea igual
        }
    }
}
