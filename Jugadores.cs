using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPongC_
{
    internal class Jugadores
    {
        int sizeP1, velocidad;
        
        public Jugadores ()
        {
            sizeP1 = 1;
            velocidad = 150;
        }
        public Jugadores(int sizeP1, int velocidad)
        {
            this.sizeP1 = sizeP1;
            this.velocidad = velocidad;
        }
        //MÉTODO JUGADOR 1, MODIFICA LA POSICIÓN DE ESTE.
        public static char[,] Jugador1(char[,] jugador, ref int yPlayer, int y, ref bool jugando)
        {
            ConsoleKeyInfo tecla1;

            if (jugando == true)
            {
                //Recogerá la tecla si no tiene otra tecla en cola
                do
                {
                    tecla1 = Console.ReadKey(intercept: true);
                } while (Console.KeyAvailable);

                //Si la tecla es la flecha hacia abajo, añadira el carácter en una fila más
                if (tecla1.Key != ConsoleKey.DownArrow)
                {
                    if (yPlayer == 1) jugador[yPlayer, 1] = '|'; //Se quedará en fila 1 para no llegar al borde del mapa (fila 0)
                    else
                    {
                        jugador[yPlayer--, 1] = ' ';
                        jugador[yPlayer, 1] = '|';
                    }
                }
                //Si la tecla es la flecha hacia arriba, añadira el carácter en fila menos
                if (tecla1.Key != ConsoleKey.UpArrow)
                {
                    if (yPlayer == y - 2) jugador[yPlayer, 1] = '|'; //Se quedará en fila 9 para no llegar al borde del mapa (fila 10)
                    else
                    {
                        jugador[yPlayer++, 1] = ' ';
                        jugador[yPlayer, 1] = '|';
                    }
                }
            }
            else
            {
                jugador[yPlayer, 1] = '|';
                jugando = true;
            }
            return jugador; //Jugador2(jugador, yPlayer, y, pelotaX,  jugando);
        }

        //MÉTODO PARA JUGADOR 2, MODIFICA POSICIÓN DE ESTE
        public static char[,] Jugador2(char[,] jugador, ref int yPlayer, int y, int x, int pelotaX, int pelotaY)
        {
            Random random = new Random();
            int aleatorio = random.Next(1, 4);

            switch(aleatorio)
            {
                case 1:
                    if (yPlayer == 1) jugador[yPlayer, x - 1] = '|'; //Se quedará en fila 1 para no llegar al borde del mapa (fila 0)
                    else
                    {
                        jugador[yPlayer-2, x - 1] = ' ';
                        jugador[pelotaY-1, x - 1] = '|';
                    }
                    break;
                case 2:
                    if (yPlayer == 1) jugador[yPlayer, x - 1] = '|'; //Se quedará en fila 1 para no llegar al borde del mapa (fila 0)
                    else
                    {
                        jugador[yPlayer--, x - 1] = ' ';
                        jugador[pelotaY, x - 1] = '|';
                        jugador[yPlayer++, x - 1] = ' ';
                    }
                    break;
                case 3:
                    if (yPlayer == 1) jugador[yPlayer, x - 1] = '|'; //Se quedará en fila 1 para no llegar al borde del mapa (fila 0)
                    else
                    {
                        jugador[yPlayer + 2, x - 1] = ' ';
                        jugador[pelotaY + 1, x - 1] = '|';
                    }
                    break;
            }
            return jugador;
        }
    }
}
