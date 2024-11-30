using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace PinPongC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Objetos y variables
            Console.CursorVisible = false;
            Tablero tablero = new Tablero();
            Pelota pelota = new Pelota(tablero.getY(), tablero.getX());
            Jugadores p1 = new Jugadores();
            Jugadores p2 = new Jugadores();

            char[,] matriz = Tablero.DibujaTablero(tablero.getTablero());
            bool jugando = false;
            int yPlayer = tablero.getY() / 2;
            bool final = false;
            bool saque = true;

            //Ajustes Temporizadores
            Timer hiloX = new Timer(1000);
            hiloX.Elapsed += Temporizador;
            hiloX.AutoReset = true;
            hiloX.Enabled = true;

            void Temporizador(object sender, ElapsedEventArgs e)
            {
                pelota.CambiaY();
                pelota.CambiaX();
                if (matriz[pelota.getPelotaY() - 1, pelota.getPelotaX()] == '_') pelota.setDireccionY(2);
                if (matriz[pelota.getPelotaY() + 1, pelota.getPelotaX()] == '^') pelota.setDireccionY(1);
                if (matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == '|') pelota.setDireccionX(2);
                if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '|') pelota.setDireccionX(1);
                
                matriz = pelota.PosicionaPelota(matriz);
                Tablero.Imprime(matriz);
            }

            do
            {
                matriz = Jugadores.Jugador1(matriz, ref yPlayer, tablero.getY(), ref jugando);
                Console.WriteLine("BIENVENIDO A PING PONG EN C#");
                Tablero.Imprime(matriz);
                Console.WriteLine("Pulsa flecha arriba para subir o flecha abajo para bajar\n\nCreado por Jacob Parra Silva");
                Console.WriteLine();
            } while (final != true);
            
        }

        //TODO
        //1- Arreglar bug, moverte a la vez que la pelota rompe todo.
        //2- Crear enemigo
        //3- Configurar velocidades y tamaño jugador
        
    }
}
