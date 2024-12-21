using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PinPongC_
{
    internal class Program
    {
        //OBJETOS Y VARIABLES DE LA CLASE PROGRAM, GLOBALES PARA SER ACCEDIDAS POR OTRAS CLASES
        public static Timer reloj;
        public static Pelota pelota;
        public static Menu menuObj;
        public static Puntos puntos;
        public static Tablero tablero;
        public static int ganador;
        public static int goal, yPlayer;
        public static bool reseteaMarcador = false;
        public static bool menu = false;
        public static bool reinicio = false;
        public static bool p1NoImprimido = true;
        public static bool reseteaJuego = false;
        public static char[,] matriz;
        static readonly object lockObject = new object();//Lock para evitar que el temporizador y el jugador varien la matriz a la vez

        //MÉTODOS GLOBALES, PARA PODER USARLOS EN OTRAS CLASES

        //RESETEA A VALORES INICIALES 
        public static void Reset()
        {
            if (reseteaMarcador == true)
            {
                puntos.resetPts();
                ganador = 0;
                goal = 0;
                reinicio = true;
                activadorAjustes();
            }
            reinicio = true;
            pelota.setPelotaX(tablero.getX() / 2);
            pelota.setPelotaY(tablero.getY() / 2);
            menu = false;
            reseteaMarcador = false;

            reloj.Change(0, 150);
        }
        //Abre el menú principal, setea ajustes
        public static void activadorAjustes()
        {
            menuObj.PrincipalMenu();
            tablero.setXY(menuObj.getSizeCampo());
            pelota.setPelotaY(tablero.getY() / 2);
            pelota.setPelotaX(tablero.getX() / 2);
            yPlayer = pelota.getPelotaY();
            matriz = Tablero.DibujaTablero(tablero.getTablero());
        }


        //MAIN
        static void Main(string[] args)
        {
            //Objetos y variables PERTENECIENTES A MAIN
            Console.CursorVisible = false;
            menuObj = new Menu();
            tablero = new Tablero();
            pelota = new Pelota(tablero.getY(), tablero.getX());
            Jugadores p1 = new Jugadores(tablero.getY() / 2);
            Jugadores p2 = new Jugadores(pelota.getDireccionY());
            puntos = new Puntos();
            Random random = new Random();

            menu = menuObj.getMenuProgram();
            int marcadorP1 = 0, marcadorNpc = 0, ganador = 0, goal = 0, puntosParaGanar = 3;

            do
            {
                activadorAjustes();

                //Ajustes Temporizadores
                reloj = new Timer(Temporizador, null, 0, 150);

                //MÉTODO MUEVE LA PELOTA Y AL ENEMIGO EN TORNO A ESTA AUTOMÁTICAMENTE CADA X MILISEGUNDOS
                //EN ESTE MÉTODO SEGÚN LA POSICIÓN DE LA PELOTA, SE DETECTAN, JUGADORES, PORTERÍA, TECHOS Y SUELOS.
                void Temporizador(object sender)
                {
                    marcadorP1 = puntos.getJugadorPts();
                    marcadorNpc = puntos.getNpcPts();
                    puntosParaGanar = menuObj.getLimGol();

                    if (reseteaJuego) //Si se ha reseteado la partida cogemos de nuevo la matriz con el tamaño actual del tablero.
                    {
                        matriz = Tablero.DibujaTablero(tablero.getTablero());
                        tablero.setXY(menuObj.getSizeCampo());
                        pelota.setPelotaY(tablero.getY() / 2);
                        pelota.setPelotaX(tablero.getX() / 2);
                        yPlayer = pelota.getPelotaY();
                    }

                    //IF y ELSE IF, DETECTA SI SE HA MARCADO GOL EN UNA U OTRA PORTERÍA Y LE DA EL GOL AL QUE LO HA METIDO,
                    //ADEMÁS COMPRUEBA SI HA LLEGADO AL LÍMITE DE PUNTUACIÓN, PARA FINALIZAR O CONTINUAR PARTIDO

                    if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '[')
                    {
                        puntos.setJugadorPts();//Puntos Jugador ++
                        marcadorP1 = puntos.getJugadorPts();
                        goal = 1;
                        menu = true;

                        Goal();
                        if (marcadorP1 >= puntosParaGanar) //Si marcador es igual a puntos para ganar se llamará a Win()
                        {
                            ganador = 1;
                            Win();
                        }
                        else
                        {//Resetea jugadores y pelota al centro, imprime durante medio segundo antes de continuar el partido
                            pelota.setPelotaX(tablero.getX() / 2);
                            pelota.setPelotaY(tablero.getY() / 2);
                            matriz = p2.Jugador2(pelota.PosicionaPelota(matriz), pelota.getPelotaY(), tablero.getX(), pelota.getDireccionY(), menuObj.getDificultad());
                            matriz = p1.Jugador1(matriz, tablero.getY(), ref reinicio, menu);
                            Console.SetCursorPosition(0, 0);
                            Tablero.Imprime(matriz, marcadorP1, marcadorNpc, puntosParaGanar, menuObj.getDifPorcentaje());
                            Thread.Sleep(500);
                            Reset();

                        }
                    }
                    else if (matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == ']')
                    {
                        puntos.setNpcPts();//puntosNpc ++
                        marcadorNpc = puntos.getNpcPts();
                        goal = 2;
                        menu = true;

                        Goal();
                        if (marcadorNpc >= puntosParaGanar) //Si marcador es igual a puntos para ganar se llamará a Win()
                        {
                            ganador = 2;
                            Win();
                        }
                        else
                        {//Resetea jugadores y pelota al centro, imprime durante medio segundo antes de continuar el partido
                            pelota.setPelotaX(tablero.getX() / 2);
                            pelota.setPelotaY(tablero.getY() / 2);
                            matriz = p2.Jugador2(pelota.PosicionaPelota(matriz), pelota.getPelotaY(), tablero.getX(), pelota.getDireccionY(), menuObj.getDificultad());
                            matriz = p1.Jugador1(matriz, tablero.getY(), ref reinicio, menu);
                            Console.SetCursorPosition(0, 0);
                            Tablero.Imprime(matriz, marcadorP1, marcadorNpc, puntosParaGanar, menuObj.getDifPorcentaje());
                            Thread.Sleep(500);
                            Reset();

                        }
                    }

                    //Cambia la dirección de X e Y al tocar jugador
                    if (matriz[pelota.getPelotaY(), pelota.getPelotaX() + 1] == '|')
                    {
                        pelota.setDireccionX(1);
                        if (matriz[pelota.getPelotaY() - 1, pelota.getPelotaX()] != '_')//No cambia dirección si está cerca del techo o suelo
                        {
                            pelota.setComportamiento(random.Next(1, 5));
                            pelota.setDireccionY(random.Next(1, 3)); //dirección y comportamiento cambia  aleatoriamente
                        }
                    }
                    else if (matriz[pelota.getPelotaY(), pelota.getPelotaX() - 1] == '|')
                    {
                        pelota.setDireccionX(2);
                        if (matriz[pelota.getPelotaY() + 1, pelota.getPelotaX()] != '^')//No cambia dirección si está cerca del techo o suelo
                        {
                            pelota.setComportamiento(random.Next(1, 5));
                            pelota.setDireccionY(random.Next(1, 3)); //dirección y comportamiento cambia  aleatoriamente
                        } 
                    }

                    //CAMBIA LA DIRECCIÓN DE Y AL TOCAR TECHO O SUELO
                    if (matriz[pelota.getPelotaY() - 1, pelota.getPelotaX()] == '_')
                    {
                        pelota.setDireccionY(2);
                        pelota.setContador();
                    }
                    else if (matriz[pelota.getPelotaY() + 1, pelota.getPelotaX()] == '^')
                    {
                        pelota.setDireccionY(1);
                        pelota.setContador();
                    }

                    //Posición pelota se actualiza en base a los parámetros anteriores
                    pelota.CambiaY(pelota.getComportamiento()); //cambia pelotaY según comportamiento
                    pelota.CambiaX();

                    //Matriz que será imprimida, jugador2 toma como parámetro la matriz devuelta por la pelota.
                    matriz = p2.Jugador2(pelota.PosicionaPelota(matriz), pelota.getPelotaY(), tablero.getX(), pelota.getDireccionY(), menuObj.getDificultad());

                    lock (lockObject) //Bloquea Imprime si ya lo está usando el do while de abajo
                    {
                        Tablero.Imprime(matriz, marcadorP1, marcadorNpc, puntosParaGanar, menuObj.getDifPorcentaje());
                    }
                }

                //BLOQUE MOVER JUGADOR CON TECLAS
                while (true)
                {
                    if (!menu) //primer !menu evita que el jugador haga un readkey dentro de un menú, el segundo !menu hace que si hay un readkey activo y estamos en el menú, no lo imprima.
                    {
                        matriz = p1.Jugador1(matriz, tablero.getY(), ref reinicio, menu);

                        lock (lockObject) //Bloquea Imprime si ya lo está usando el temporizador
                        {
                            if (!menu) Tablero.Imprime(matriz, marcadorP1, marcadorNpc, puntosParaGanar, menuObj.getDifPorcentaje());
                            if (reseteaJuego == true)
                            {
                                Console.Clear();
                                reseteaJuego = false;
                            }
                        }
                    }
                };

                //MÉTODO WIN
                void Win()
                {
                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    Console.Clear();
                    switch (ganador)
                    {
                        case 1:
                            Console.WriteLine($"Enhorabuena has ganado!\nRESULTADO: Jugador({marcadorP1}) - ({marcadorNpc})Npc");
                            Thread.Sleep(3000);
                            gameOver();
                            break;
                        case 2:
                            Console.WriteLine($"Lo sentimos, has perdido, la próxima vez será!\nRESULTADO: Jugador({marcadorP1}) - ({marcadorNpc})Npc");
                            Thread.Sleep(3000);
                            gameOver();
                            break;
                    }
                }

                //MÉTODO GOL
                void Goal()
                {
                    menuObj.SetMenProgram(true); //Inhabilita acceso a menupausa durante celebración
                    reloj.Change(Timeout.Infinite, Timeout.Infinite);
                    marcadorP1 = puntos.getJugadorPts();
                    marcadorNpc = puntos.getNpcPts();

                    switch (goal)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"GOOOOOOL del Jugador\nEl marcador se posiciona {marcadorP1} a {marcadorNpc}\n\nPulsa dos veces para continuar");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine($"GOOOOOOL del Npc\nEl marcador se posiciona {marcadorP1} a {marcadorNpc}\n\nPulsa dos veces para continuar");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                    menuObj.SetMenProgram(false);
                }

                //MÉTODO GAME OVER, EL USUARIO ELIGE QUÉ HACER.
                void gameOver()
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    char tryAgain;
                    bool succes = false;

                    do
                    {
                        Console.WriteLine("El partido ha acabado, ¿Quieres volver al menu principal? (Y/N)");
                        succes = char.TryParse(Console.ReadLine().ToUpper(), out tryAgain);
                    } while (succes == false && (tryAgain != 'Y' || tryAgain != 'N'));

                    switch (tryAgain)
                    {
                        case 'Y':

                            reseteaMarcador = true;
                            Console.Clear();
                            Console.WriteLine("Volviendo al menú principal");
                            Thread.Sleep(1000);
                            Reset();

                            break;
                        case 'N':

                            Console.Clear();
                            Console.WriteLine("Cerrando juego");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                            break;
                    }

                    Console.CursorVisible = false;
                }

                
            } while (true);

            //TODO
            //Situación actual, seguir construyendo menús.
            //Si inicias partida cambiando el tamaño del campo, el jugador visualmente está en un sitio, pero realmente está en otro.
            //Se sigue bugueando cambiar el tamaño del campo al volver del menupausa.
            //1- Mejoras en el rebote de la pelota, más patrones.
            //2- los char no son igual de altos que de ancho, lo que dá la sensación de un movimiento raro de la pelota.
            //3- Configurar velocidades.

        }
    }
}
