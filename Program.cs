using System;
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
            bool jugando = false;
            int yPlayer = tablero.getY() / 2;
            bool pausarJugador = false;
            bool reseteaMarcador = false;
            bool final = false;
            int marcadorP1 = 0, marcadorNpc = 0, ganador = 0, goal = 0;

            do
            {
                //Ajustes Temporizadores
                reloj = new Timer(Temporizador, null, 0, 300);

                //MÉTODO MUEVE LA PELOTA Y AL ENEMIGO EN TORNO A ESTA
                void Temporizador(object sender)
                {
                    marcadorP1 = puntos.getJugadorPts();
                    marcadorNpc = puntos.getNpcPts();
                    if (marcadorP1 >= 1)
                    {
                        ganador = 1;
                        Win();
                    }
                    else if (marcadorNpc >= 1)
                    {
                        ganador = 2;
                        Win();
                    }

                    if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '[')
                    {
                        puntos.setJugadorPts();
                        goal = 1;
                        Goal();
                        Reset();
                    }
                    else if (matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == ']')
                    {
                        puntos.setNpcPts();
                        goal = 2;
                        Goal();
                        Reset();
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

                    //Pelota se mueve en base a los parámetros anteriores
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
                    Thread.Sleep(100);//Velocidad mover jugador
                    matriz = Jugadores.Jugador1(matriz, ref yPlayer, tablero.getY(), ref jugando);
                    Console.WriteLine();

                    lock (lockObject) //Bloquea Imprime si ya lo está usando el temporizador
                    {
                        Tablero.Imprime(matriz, marcadorP1, marcadorNpc);
                    }

                };

                //MÉTODO WIN
                //TODO resolver bug de escritura cuando se pierde
                void Win()
                {
                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
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
                        default:
                            return;
                    }
                }

                //MÉTODO GOL
                void Goal()
                {
                    marcadorP1 = puntos.getJugadorPts();
                    marcadorNpc = puntos.getNpcPts();
                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    Console.Clear();

                    switch (goal)
                    {
                        case 1:
                            Console.WriteLine($"GOOOOOOL del Jugador\nEl marcador se posiciona {marcadorP1} a {marcadorNpc}");
                            break;
                        case 2:
                            Console.WriteLine($"GOOOOOOL del Npc\nEl marcador se posiciona {marcadorP1} a {marcadorNpc}");
                            break;
                    }
                    Reset();
                }

                //MÉTODO GAME OVER, EL USUARIO ELIGE QUÉ HACER.
                void gameOver()
                {
                    Console.CursorVisible = true;
                    char tryAgain;
                    bool succes = false;
                    pausarJugador = true;
                    do
                    {
                        Console.WriteLine("El partido ha acabado, ¿quieres volver a jugar? (Y/N)");
                        succes = char.TryParse(Console.ReadLine().ToUpper(), out tryAgain);
                    } while (succes == false && (tryAgain != 'Y' || tryAgain != 'N'));
                    Console.CursorVisible = false;
                    switch (tryAgain)
                    {
                        case 'Y':
                            reseteaMarcador = true;
                            Reset();
                            break;
                        case 'N':
                            Console.WriteLine("Cerrando juego");
                            final = true;
                            Thread.Sleep(100);
                            break;
                    }
                }

                //RESETEA EL TABLERO 
                void Reset()
                {
                    //TODO hay fallos a la hora de resetear cuando acaba el partido, se buguea todo
                    if (reseteaMarcador == true)
                    {
                        Console.WriteLine("Ambos Marcadores a 0");
                        Thread.Sleep(2000);
                        puntos.resetPts();
                        pelota.setPelotaX(tablero.getX() / 2);
                        pelota.setPelotaY(tablero.getY() / 2);
                        ganador = 0;
                        goal = 0;
                        reseteaMarcador = false;
                        pausarJugador = false;
                        
                    }
                    else
                    {
                        pelota.setPelotaX(tablero.getX() / 2);
                        pelota.setPelotaY(tablero.getY() / 2);
                        reloj.Change(0, 300);
                    }
                    
                }
            } while (final != true);

            //TODO
            //Situación actual, Se resetea el juego, marcadores a 0 e inicia la partida, pero al querer mover el jugador, estoy dentro de un readline, que me bloquea poder usar arrowkey

            //Resolver bug cuando acaba la partida, también configurar el poder jugar de nuevo
            //1- Configurar el perder o ganar puntos
            //2- Tablero reset cuando pierdes, por ejemplo marcador >= 5.
            //3- Cuando pierdes o ganas puntos resetear posiciones, manteniendo el marcador.
            //4- Configurar velocidades y tamaño jugador
            //5- El rebote de la pelota que no siempre sea igual
        }
    }
}
