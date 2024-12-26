using System;
using System.Security.Policy;

namespace PinPongC_
{
    internal class Tablero
    {
        char[,] tablero;
        int x, y, velocidad;

        //CONSTRUCTORES DE LA CLASE TABLERO
        public Tablero()//constructor predeterminado.
        {
            x = 21;
            y = 9;
            tablero = new char[y, x];
        }
        
        //Geter, obtiene la matriz desde el objeto que se ha incovado, objeto de clase Tablero.
        public char[,] getTablero() => tablero;
        public int getX() => x;
        public int getY() => y;
        public int getVelocidad() => velocidad;
        
        //SETTER
        public void setXY(int tamaño)
        {
            switch (tamaño)
            {
                case 1:
                    this.x = 17;
                    this.y = 7; 
                    break;
                case 2:
                    this.x = 20;
                    this.y = 8;
                    break;
                case 3:
                    this.x = 23;
                    this.y = 9;
                    break;
                case 4:
                    this.x = 26;
                    this.y = 10;
                    break;
                case 5:
                    this.x = 29;
                    this.y = 11;
                    break;
            }
            this.tablero = new char[y, x];
        }

        //MÉTODO DE CLASE TABLERO QUE DIBUJARÁ LOS BORDES DEL MAPA/TABLERO
        public static char[,] DibujaTablero(char[,] tablero)//Método de esta clase, público pero sólo accesible invocando clase primero.
        {
            //Introduce paredes
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                tablero[i, 0] = ']';
                tablero[i, tablero.GetLength(1) - 1] = '[';
            }
            //Introduce techo y suelo
            for (int i = 0; i < tablero.GetLength(1); i++)
            {
                tablero[0, i] = '_';
                tablero[tablero.GetLength(0) - 1, i] = '^';
            }
            return tablero;
        }

        //IMPRIME EN PANTALLA LA MATRIZ
        public static void Imprime(char[,] array, int p1Pts, int npcPts, int puntosParaGanar, int dificultad)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"---------MARCADOR---------\nJugador ({p1Pts}) - ({npcPts}) NpcRival");
            
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\nPRIMERO EN MARCAR {puntosParaGanar} GOL/ES\nDIFICULTAD ({dificultad}%)\nVELOCIDAD({Program.menuObj.getVelPelotaText()})");
            Console.WriteLine("\nFlecha arriba/abajo para moverte\nESC: Menú de pausa\n\nCreado por Jacob Parra Silva");
        }
    }
}
