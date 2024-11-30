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
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Tablero tablero = new Tablero();
            Pelota pelota = new Pelota(tablero.getY(), tablero.getX());
            Jugadores p1 = new Jugadores();
            Jugadores p2 = new Jugadores();

            char[,] matriz = Tablero.DibujaTablero(tablero.getTablero());
            bool jugando = false;
            int yPlayer = tablero.getY() / 2;
            bool final = false;
            
            do
            {
                matriz = pelota.DibujaPelota(matriz, pelota.getPelotaX(), pelota.getPelotaY(), tablero.getX(), tablero.getY());
                matriz = Jugadores.Jugador1(matriz, ref yPlayer, tablero.getY(), ref jugando);
                Console.WriteLine("BIENVENIDO A PING PONG EN C#");
                Tablero.Imprime(matriz);
                Console.WriteLine("Pulsa flecha arriba para subir o flecha abajo para bajar\n\nCreado por Jacob Parra Silva");
                Console.WriteLine();
                Thread.Sleep(tablero.getVelocidad());
            } while (final != true);
        }
        //TODO
        //1- Crear pelota
        //2- Crear enemigo
        //3- Configurar velocidades y tamaño jugador
    }
}
