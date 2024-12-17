using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PinPongC_
{
    internal class Jugadores
    {
        int sizeP1, y, yAnterior, velocidad;
        
        public Jugadores ()
        {
            sizeP1 = 1;
            velocidad = 500;
        }
        public Jugadores(int sizeP1, int y)
        {
            this.sizeP1 = sizeP1;
            this.y = y;
            yAnterior = 1;
        }
        //SETTER
        public void setNpcY(int y) => this.y = y;
        public void setAnteriorY(int y) => yAnterior = y;

        //GETTERS
        public int getNpcY() => y;
        public int getAnteriorY() => yAnterior;

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
            return jugador;
        }

        //MÉTODO PARA JUGADOR 2, MODIFICA POSICIÓN DE ESTE
        //Tiene 50% posibilidades de acertar la posición de la pelota.
        public char[,] Jugador2(char[,] jugador, int pelotaY, int tableroX, int direccionPelota, int dificultad)
        {
            Random random = new Random();
            int aleatorio = random.Next(1, dificultad);
            tableroX = tableroX - 2;

            switch (aleatorio)
            { //primeros case el jugador se mueve preciso entorno a pelota
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    jugador[this.getAnteriorY(), tableroX] = ' ';
                    jugador[pelotaY, tableroX] = '|';
                    this.setAnteriorY(pelotaY);
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                case 10: //Estos cases se mueve el jugador una posición más que la pelota, haciendo que esté desalineado con ella y pueda fallar.
                    if (direccionPelota == 1 && jugador[pelotaY -1, tableroX] != '_')
                    {
                        jugador[this.getAnteriorY(), tableroX] = ' ';
                        jugador[pelotaY - 1, tableroX] = '|';
                        this.setAnteriorY(pelotaY - 1);
                    } else if (direccionPelota == 2 && jugador[pelotaY + 1, tableroX] != '^')
    {
                        jugador[this.getAnteriorY(), tableroX] = ' ';
                        jugador[pelotaY + 1, tableroX] = '|';
                        this.setAnteriorY(pelotaY + 1);
                    }
                    break;
            }
            return jugador;
        }
    }
}
