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
                        return;
                    }
                    else if (marcadorNpc >= 1)
                    {
                        ganador = 2;
                        Win();
                        return;
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

                    lock (lockObject) //Bloquea Imprime si ya lo está usando el temporizador
                    {
                        Tablero.Imprime(matriz, marcadorP1, marcadorNpc);
                    }

                };

                //MÉTODO WIN
                void Win()
                {
                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    jugando = false;
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
                    marcadorP1 = puntos.getJugadorPts();
                    marcadorNpc = puntos.getNpcPts();

                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    

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
                    Reset();
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

                            final = true;
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

                        puntos.resetPts();
                        pelota.setPelotaX(tablero.getX() / 2);
                        pelota.setPelotaY(tablero.getY() / 2);
                        ganador = 0;
                        goal = 0;
                        
                        reloj.Change(0, 300);
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
            //Situación actual, el juego funciona, pero al limpiar la consola y mostrar mensajes o menú, se cuela la impresión del juego

            //Resolver bug cuando acaba la partida, también configurar el poder jugar de nuevo
            //1- Configurar el perder o ganar puntos
            //2- Tablero reset cuando pierdes, por ejemplo marcador >= 5.
            //3- Cuando pierdes o ganas puntos resetear posiciones, manteniendo el marcador.
            //4- Configurar velocidades y tamaño jugador
            //5- El rebote de la pelota que no siempre sea igual
        }
    }
}
