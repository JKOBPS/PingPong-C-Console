using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPongC_
{
    internal class Pelota
    {
        int x, y;
        //CONSTRUCTORES
        public Pelota()
        {
            x = 15;
            y = 5;
        }
        public Pelota(int y, int x)
        {
            this.x = x / 2;
            this.y = y / 2;
        }

        //GETTERS Y SETTERS
        public void setPelotaXY(int y, int x) 
        {
            this.x = x;
            this.y = y;
        }
        public int getPelotaX() => x;
        public int getPelotaY() => y;

        //MÉTODO PARA MOVER LA PELOTA
        //TODO, PINTAR LA PELOTA Y GENERAR MOVIMIENTO DE LA PELOTA
        public char[,] DibujaPelota(char[,] matriz, int pelotaX, int pelotaY, int tabX, int tabY)
        {
            matriz[pelotaY, pelotaX] = '*';
            //if (matriz[pelotaX - 1, pelotaY] == '|')
            //{
            //}
            return matriz;
        }

    }
}
