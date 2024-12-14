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
            x = 25;
            y = 9;
            velocidad = 150;
            tablero = new char[y, x];
        }
        public Tablero(int y, int x)//Constructor que permite parámetros.
        {
            this.x = x;
            this.y = y;
            tablero = new char[y, x];
        }
        public char[,] getTablero() => tablero;//Geter, obtiene la matriz desde el objeto que se ha incovado, objeto de clase Tablero.
        public int getX() => x;
        public int getY() => y;
        public int getVelocidad() => velocidad;

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
        public static void Imprime(char[,] array, int p1Pts, int npcPts)
        {
            Console.Clear();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != null) Console.Write(array[i, j]);

                }
                Console.WriteLine();
            }
            Console.WriteLine($"MARCADOR\nJugador({p1Pts}) - ({npcPts})Maquina");
            Console.WriteLine("Pulsa flecha arriba para subir o flecha abajo para bajar\n\nCreado por Jacob Parra Silva");
        }
    }
}
