using System;
using System.CodeDom;
using System.Collections.Generic;
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
            Random random = new Random();

            char[,] matriz = Tablero.DibujaTablero(tablero.getTablero());
            bool jugando = false;
            int yPlayer = tablero.getY() / 2;
            bool final = false;
            bool volverJugar = false;
            bool saque = true;
            int marcadorP1, marcadorNpc;

            //Ajustes Temporizadores
            reloj = new Timer(Temporizador, null, 0, 500);

            //MÉTODO MUEVE LA PELOTA Y AL ENEMIGO EN TORNO A ESTA
            void Temporizador(object sender)
            {
                if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '[' || matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == ']')
                {
                    gameOver();
                }
                if (matriz[pelota.getPelotaY() - 1, pelota.getPelotaX()] == '_') pelota.setDireccionY(2);
                if (matriz[pelota.getPelotaY() + 1, pelota.getPelotaX()] == '^') pelota.setDireccionY(1);
                if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '|') 
                {
                   pelota.setDireccionX(1);
                    if (matriz[pelota.getPelotaY() - 1, pelota.getPelotaX()] != '_')//No cambia dirección si está cerca del techo o suelo
                        pelota.setDireccionY(random.Next(1, 3));//Toca jugador y dirección cambia a 1 o 2 aleatoriamente
                }
                if (matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == '|') 
                {
                    pelota.setDireccionX(2);
                    if (matriz[pelota.getPelotaY() + 1, pelota.getPelotaX()] != '^')//No cambia dirección si está cerca del techo o suelo
                        pelota.setDireccionY(random.Next(1, 3));//Toca jugador y dirección cambia a 1 o 2 aleatoriamente
                }
                pelota.CambiaY();
                pelota.CambiaX();
                
                matriz = p2.Jugador2(pelota.PosicionaPelota(matriz), pelota.getPelotaY(), tablero.getX(), pelota.getDireccionY());

                lock (lockObject) //Bloquea Imprime si ya lo está usando el do while de abajo
                {
                    Tablero.Imprime(matriz);
                }
            }

            //BLOQUE MOVER JUGADOR CON TECLAS
            do
            {
                Thread.Sleep(500);//Velocidad mover jugador
                matriz = Jugadores.Jugador1(matriz, ref yPlayer, tablero.getY(), ref jugando);
                Console.WriteLine();
                lock (lockObject) //Bloquea Imprime si ya lo está usando el temporizador
                {
                    Tablero.Imprime(matriz);
                }
            } while (final != true);
            
            void gameOver()
            {
                reloj.Change(Timeout.Infinite, Timeout.Infinite);
                Console.WriteLine("Has perdido, lo sentimos mucho, ¿quieres volver a jugar? Y/N");
                char tryAgain = char.Parse(Console.ReadLine().ToUpper());
                switch(tryAgain)
                {
                    case 'Y':
                        final = false;
                        Tablero.Reset();
                        break;
                    case 'N':
                        final = true;
                        Tablero.Reset();
                        //OpenMenu();
                        break;

                }
            }
        }

        //TODO
        //1- Configurar el perder o ganar puntos
        //2- Tablero reset cuando pierdes, por ejemplo marcador >= 5.
        //3- Cuando pierdes o ganas puntos resetear posiciones, manteniendo el marcador.
        //4- Configurar velocidades y tamaño jugador
        
    }
}
