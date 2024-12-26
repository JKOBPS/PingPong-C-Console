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
        int y, yAnterior;
        
        public Jugadores ()
        {
            y = 2;
        }
        public Jugadores(int y)
        {
            this.y = y;
            yAnterior = 1;
        }
        //SETTER
        public void setP1Y(int y) => this.y = y;
        public void setNpcY(int y) => this.y = y;
        public void setAnteriorY(int y) => yAnterior = y;

        //GETTERS
        public int getP1Y() => y;
        public int getNpcY() => y;
        public int getAnteriorY() => yAnterior;

        //MÉTODO JUGADOR 1, MODIFICA LA POSICIÓN DE ESTE.
        public char[,] Jugador1(char[,] jugador, int y, ref bool reinicio, bool menu)
        {
            
            ConsoleKeyInfo tecla1;

            if (reinicio != true && menu != true)
            {
                Thread.Sleep(100);
                int yPlayer = this.getP1Y();
                //Recogerá la tecla si no tiene otra tecla en cola
                do
                {
                    tecla1 = Console.ReadKey(intercept: true);
                } while (Console.KeyAvailable);

                //Si la tecla es la flecha hacia abajo, añadira el carácter en una fila más
                if (tecla1.Key == ConsoleKey.UpArrow)
                {
                    if (yPlayer == 1) jugador[yPlayer, 1] = '|'; //Se quedará en fila 1 para no llegar al borde del mapa (fila 0)
                    else
                    {
                        this.setAnteriorY(yPlayer);
                        this.setP1Y(--yPlayer);
                        jugador[this.getAnteriorY(), 1] = ' ';
                        jugador[this.getP1Y(), 1] = '|';
                    }
                }
                //Si la tecla es la flecha hacia arriba, añadira el carácter en fila menos
                if (tecla1.Key == ConsoleKey.DownArrow)
                {
                    if (yPlayer == y - 2) jugador[yPlayer, 1] = '|'; //Se quedará en fila 9 para no llegar al borde del mapa (fila 10)
                    else
                    {
                        this.setAnteriorY(yPlayer);
                        this.setP1Y(++yPlayer);
                        jugador[this.getAnteriorY(), 1] = ' ';
                        jugador[this.getP1Y(), 1] = '|';
                    }
                }
                //Si se pulsa Esc, se pausa el juego y se llama al método MenuPausa() de la clase Menu
                if (tecla1.Key == ConsoleKey.Escape)
                {
                    Program.menu = true;
                    Program.reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    Program.menuObj.MenuPausa();
                    if (Program.reseteaJuego) return Program.matriz;
                }
            }
            else
            {
                this.setAnteriorY(this.getP1Y());
                if (!(this.getAnteriorY() > Program.tablero.getY() - 2)) jugador[this.getAnteriorY(), 1] = ' ';
                this.setP1Y(y / 2);
                jugador[this.getP1Y(), 1] = '|';
                reinicio = false;
            }
            return jugador;
        }

        //MÉTODO PARA JUGADOR 2, MODIFICA POSICIÓN DE ESTE
        //Tiene 50% posibilidades de acertar la posición de la pelota.
        public char[,] Jugador2(char[,] jugador, int pelotaY, int tableroX, int direccionPelota, int dificultad)
        {
            Random random = new Random();
            int aleatorio = random.Next(1, dificultad);
            tableroX = tableroX - 2; //posiciónX jugador2

            switch (aleatorio)
            { //primeros case el jugador se mueve preciso entorno a pelota
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (!(this.getAnteriorY() > Program.tablero.getY() - 2)) jugador[this.getAnteriorY(), tableroX] = ' ';
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
                        if (!(this.getAnteriorY() > Program.tablero.getY() - 2)) jugador[this.getAnteriorY(), tableroX] = ' ';
                        jugador[pelotaY - 1, tableroX] = '|';
                        this.setAnteriorY(pelotaY - 1);
                    } else if (direccionPelota == 2 && jugador[pelotaY + 1, tableroX] != '^')
    {
                        if (!(this.getAnteriorY() > Program.tablero.getY() - 2)) jugador[this.getAnteriorY(), tableroX] = ' ';
                        jugador[pelotaY + 1, tableroX] = '|';
                        this.setAnteriorY(pelotaY + 1);
                    }
                    break;
            }
            
            if(Program.p1NoImprimido) //Imprime por primera vez al P1 en la misma posiciónY que pelota y npc
            {
                Program.p1.setP1Y(pelotaY);
                jugador[pelotaY, 1] = '|';
                Program.p1NoImprimido = false;
            }
            

            return jugador;
        }
    }
}
