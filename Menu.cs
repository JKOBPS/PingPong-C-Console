using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PinPongC_
{
    internal class Menu
    {
        bool succes, succesMenu, menuProgram;
        int limiteGoles, dificultad, difPorcentaje, tamañoCampo, tamañoJugadores, velocidadJugador, velocidadPelota;
        string menu, textoTamCampo;

        public Menu()
        {
            menuProgram = false;
            limiteGoles = 3;
            dificultad = 9;
            difPorcentaje = 70;
            tamañoCampo = 3;
            textoTamCampo = "NORMAL";
            tamañoJugadores = 1;
            velocidadJugador = 1;
            velocidadPelota = 1;
        }

        //MENU PRINCIPAL
        //Método llamado desde Program Reset(), cada vez que se reinicia un juego o se sale al menú principal. Permite ajustar los valores del juego e iniciar partida.
        public void PrincipalMenu() 
        {
            do
            {
                succesMenu = false;
                Console.Clear();
                Console.WriteLine("CONSOLE PING-PONG | CSharp\n");
                Console.WriteLine("ESCRIBE START PARA JUGAR\n\nMenu:");
                Console.WriteLine($"1. Límite de goles ({this.limiteGoles})");
                Console.WriteLine($"2. Dificultad ({difPorcentaje}%)");
                Console.WriteLine($"3. Tamaño del campo ({this.textoTamCampo})");
                Console.WriteLine($"4. Velocidad pelota ({this.velocidadPelota})");
                menu = Console.ReadLine().ToLower();

                switch (menu)
                {
                    case "1":
                        Console.WriteLine("Opción 1 seleccionada");
                        LimiGol();
                        break;

                    case "2":
                        Console.WriteLine("Opción 2 seleccionada");
                        Dificultad();
                        break;

                    case "3":
                        Console.WriteLine("Opción 3 seleccionada");
                        TamañoCampo();
                        break;

                    case "4":
                        Console.WriteLine("Opción 4 seleccionada");
                        break;

                    case "start":
                        Console.WriteLine("Iniciando el juego...");
                        SetMenProgram(true);
                        succesMenu = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida, vuelve a intentarlo");
                        break;
                }
                Console.Clear();
            } while (!succesMenu);
            this.SetMenProgram(false);
        }

        //MÉTODO MENUPAUSA
        //El método es llamado desde el método jugador1 de la clase Jugadores
        public void MenuPausa()
        {
            bool accesoMenuPausa = this.getMenuProgram(); //PARÁMETRO CAMBIADO DESDE CELEBRACIÓN DE GOL
            ConsoleKeyInfo input;

            
            if (!accesoMenuPausa) //Evita poder acceder al menú de pausa durante la celebración, lo que causaría bug
            {
                Console.Clear();
                Console.Clear();
                Console.WriteLine("----MENU PAUSA----\nESC: Salir\nCUALQUIER BOTON: Renaudar partida");
                input = Console.ReadKey(intercept: true);
                switch (input.Key)
                {
                    case ConsoleKey.Escape:
                        Program.reseteaMarcador = true;
                        Program.reseteaJuego = true;
                        Console.Clear();
                        Console.WriteLine("Saliendo al menú principal...");
                        Thread.Sleep(1000);
                        Program.Reset();
                        break;
                    default:
                        Console.Clear();

                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Renaudando en:\n     3");
                        Thread.Sleep(1000);

                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Renaudando en:\n     2");
                        Thread.Sleep(1000);

                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Renaudando en:\n     1");
                        Thread.Sleep(1000);

                        Console.Clear();
                        Tablero.Imprime(Program.matriz, Program.puntos.getJugadorPts(), Program.puntos.getNpcPts(), this.limiteGoles, this.getDifPorcentaje());
                        Thread.Sleep(1000);
                        Program.menu = false;
                        Program.reloj.Change(0, 150);
                        break;
                }
            }
        }

        //Método llamado desde clase Menu, permite ajustar el número de goles para ganar
        private void LimiGol()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("AJUSTAR LÍMITE DE GOLES");
                Console.WriteLine("Escribe cuantos goles quieres que sea el límite (mínimo 1)");
                succes = int.TryParse(Console.ReadLine(), out limiteGoles);

            } while (!succes || limiteGoles < 1);
            this.SetLimGol(limiteGoles);
            Console.WriteLine($"El límite se ha establecido en {limiteGoles} gol/es");
            Thread.Sleep(1500);
        }

        //Método llamado desde clase Menu, permite ajustar la dificultad del enemigo
        private void Dificultad()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("AJUSTAR DIFICULTAD");
                Console.WriteLine("Elige la dificultad del enemigo\n1. 50%\n2. 60%\n3. 70%\n4. 80%\n5. 90%");
                succes = int.TryParse(Console.ReadLine(), out dificultad);

            } while (!succes || !(dificultad >= 1) || !(dificultad <= 5));

            switch (dificultad)
            {
                case 1:
                    this.SetDificultad(11);
                    Console.WriteLine($"Nivel de dificultad MUY FÁCIL (50%) establecido.\nTienes más miedo que doraimon en aduanas.");
                    difPorcentaje = 50;
                    break;
                case 2:
                    this.SetDificultad(10);
                    Console.WriteLine($"Nivel de dificultad FÁCIL (60%) establecido.");
                    difPorcentaje = 60;
                    break;
                case 3:
                    this.SetDificultad(9);
                    Console.WriteLine($"Nivel de dificultad NORMAL (70%) establecido.");
                    difPorcentaje = 70;
                    break;
                case 4:
                    this.SetDificultad(8);
                    Console.WriteLine($"Nivel de dificultad DIFÍCIL (80%) establecido. .");
                    difPorcentaje = 80;
                    break;
                case 5:
                    this.SetDificultad(7);
                    Console.WriteLine($"Nivel de dificultad MUY DIFÍCIL (90%) establecido.\nEres tan duro/a que puedes descargar hardware.");
                    difPorcentaje = 90;
                    break;
            }
            Thread.Sleep(1500);
        }

        //Método llamado desde clase Menu, permite ajustar tamaño de campo
        private void TamañoCampo()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("AJUSTAR TAMAÑO DEL CAMPO");
                Console.WriteLine("Elige tamaño del campo:\n1. MUY PEQUEÑO\n2. PEQUEÑO\n3. NORMAL\n4. GRANDE\n5. MUY GRANDE");
                succes = int.TryParse(Console.ReadLine(), out tamañoCampo);

            } while (!succes || !(tamañoCampo >= 1) || !(tamañoCampo <= 5));

            switch (tamañoCampo)
            {
                case 1:
                    this.SetSizeCampo(1);
                    textoTamCampo = "MUY PEQUEÑO";
                    Console.WriteLine($"Tamaño del campo MUY PEQUEÑO establecido.");
                    break;
                case 2:
                    this.SetSizeCampo(2);
                    textoTamCampo = "PEQUEÑO";
                    Console.WriteLine($"Tamaño del campo PEQUEÑO establecido.");
                    break;
                case 3:
                    this.SetSizeCampo(3);
                    textoTamCampo = "NORMAL";
                    Console.WriteLine($"Tamaño del campo NORMAL establecido.");
                    break;
                case 4:
                    this.SetSizeCampo(4);
                    textoTamCampo = "GRANDE";
                    Console.WriteLine($"Tamaño del campo GRANDE establecido.");
                    break;
                case 5:
                    this.SetSizeCampo(5);
                    textoTamCampo = "MUY GRANDE";
                    Console.WriteLine($"Tamaño del campo MUY GRANDE establecido.");
                    break;
            }

            Thread.Sleep(1500);
        }        
        //TODO
        private void VelocidadPelota()
        {

        }

        //GETTER
        public bool getMenuProgram() => this.menuProgram;
        public int getLimGol() => this.limiteGoles;
        public int getDificultad() => this.dificultad;
        public int getDifPorcentaje() => this.difPorcentaje;
        public int getSizeCampo() => this.tamañoCampo;
        public int getVelPelota() => this.velocidadPelota;


        //SETTER
        public void SetMenProgram(bool menuProgram)
        {
            this.menuProgram = menuProgram;
        }
        private void SetLimGol(int limiteGoles) {
            this.limiteGoles = limiteGoles;
        }
        private void SetDificultad(int dificultad)
        {
            this.dificultad = dificultad;
        }
        private void SetSizeCampo(int tamañoCampo)
        {
            this.tamañoCampo= tamañoCampo;
        }
        private void SetVelPelota(int velocidadPelota)
        {
            this.velocidadPelota= velocidadPelota;
        }

    }
}
