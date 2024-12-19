using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PinPongC_
{
    internal class Pelota
    {
        int x, y, direccionX, direccionY, comportamiento, yAnterior, xAnterior, contador;
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
            comportamiento = 3;
            contador = 0;
        }
        public Pelota(int y, int x)
        {
            this.x = x / 2;
            this.y = y / 2;
            direccionX = 1;
            direccionY = 1;
            xAnterior = 5;
            yAnterior = 5;
            comportamiento = 3;
            contador = 0;
        }

        //SETTERS
        public void setContador()
        {
            this.contador++;
        }
        public void ResetContador()
        {
            contador = 0;
        }
        public void setComportamiento(int comportamiento)
        {
            this.comportamiento = comportamiento;
        }
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
        public int getContador() => contador;
        public int getComportamiento() => comportamiento;
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
            else if (this.getDireccionX() == 2) this.setPelotaX(this.getPelotaX() + 1);
        }
        public void CambiaY(int comportamiento)
        {
            //Comportamiento de la pelota es el tipo de movimiento que hace, más vertical, mas diagonal o más recto.
            switch(comportamiento)
            {
                case 1:
                    if (this.getDireccionY() == 1) this.setPelotaY(this.getPelotaY() - 1);
                    else if (this.getDireccionY() == 2) this.setPelotaY(this.getPelotaY() + 1);
                    break;
                case 2:
                    if (this.getDireccionY() == 1)
                    {
                        if (contador < 1)
                        {
                            this.setPelotaY(this.getPelotaY());
                            contador++;
                        }
                        else
                        {
                            this.setPelotaY(this.getPelotaY() - 1);
                            contador = 0;
                        }
                    }
                    else
                    {
                        if (contador < 1)
                        {
                            this.setPelotaY(this.getPelotaY());
                            contador++;
                        } else
                        {
                            this.setPelotaY(this.getPelotaY() + 1);
                            contador = 0;
                        }
                    }
                    break;
                case 3:
                    if (this.getDireccionY() == 1)
                    {
                        if (contador < 2)
                        {
                            this.setPelotaY(this.getPelotaY());
                            contador++;
                        }
                        else
                        {
                            this.setPelotaY(this.getPelotaY() - 1);
                            contador = 0;
                        }
                    }
                    else
                    {
                        if (contador < 2)
                        {
                            this.setPelotaY(this.getPelotaY());
                            contador++;
                        }
                        else
                        {
                            this.setPelotaY(this.getPelotaY() + 1);
                            contador = 0;
                        }
                    }
                    break;
                case 4:
                    if (this.getDireccionY() == 1)
                    {
                        if (contador < 3)
                        {
                            this.setPelotaY(this.getPelotaY());
                            contador++;
                        }
                        else
                        {
                            this.setPelotaY(this.getPelotaY() - 1);
                            contador = 0;
                        }
                    }
                    else
                    {
                        if (contador < 3)
                        {
                            this.setPelotaY(this.getPelotaY());
                            contador++;
                        }
                        else
                        {
                            this.setPelotaY(this.getPelotaY() + 1);
                            contador = 0;
                        }
                    }
                    break;
            }
            
        }
    }
}
