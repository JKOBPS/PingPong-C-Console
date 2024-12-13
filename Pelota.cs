﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PinPongC_
{
    internal class Pelota
    {
        int x, y, direccionX, direccionY, yAnterior, xAnterior;
        Program acceso = new Program();

        //CONSTRUCTORES
        public Pelota()
        {
            x = 15;
            y = 5;
            direccionX = 1;
            direccionY = 1;
            xAnterior = 5;
            yAnterior = 5;
        }
        public Pelota(int y, int x)
        {
            this.x = x / 2;
            this.y = y / 2;
            direccionX = 1;
            direccionY = 1;
            xAnterior = 5;
            yAnterior = 5;
        }

        //SETTERS
        public void setPelotaY(int y) 
        {
            this.y = y;
        }
        public void setPelotaX(int x)
        {
            this.x = x;
        }
        public void setDireccionX(int direccionX)
        {
            this.direccionX = direccionX;
        }
        public void setDireccionY(int direccionY)
        {
            this.direccionY = direccionY;
        }
        public void setAnteriorX(int x)
        {
            this.xAnterior = x;
        }
        public void setAnteriorY(int y)
        {
            this.yAnterior = y;
        }
        //GETTERS
        public int getDireccionX() => direccionX;
        public int getDireccionY() => direccionY;
        public int getPelotaX() => x;
        public int getPelotaY() => y;
        public int getAnteriorX() => xAnterior;
        public int getAnteriorY() => yAnterior;

        //MÉTODO PARA MOVER LA PELOTA
        public char[,] PosicionaPelota(char[,] matriz)
        {
            //Pone asterisco en la nueva posición, y pone espacio en la anterior posición
            matriz[this.getPelotaY(), this.getPelotaX()] = 'o';
            matriz[this.getAnteriorY(), this.getAnteriorX()] = ' ';
            //Setea pelota.anteriorY/X como la actual pelota.Y/X
            this.setAnteriorY(this.getPelotaY());
            this.setAnteriorX(this.getPelotaX());
            return matriz;
        }
        public void CambiaX()
        {
            if (this.getDireccionX() == 1) this.setPelotaX(this.getPelotaX()-1);
            else this.setPelotaX(this.getPelotaX() + 1);
        }
        public void CambiaY()
        {
            if (this.getDireccionY() == 1) this.setPelotaY(this.getPelotaY() - 1);
            else this.setPelotaY(this.getPelotaY() + 1);
        }
    }
}
