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
        string menu;

        public Menu()
        {
            menuProgram = false;
            limiteGoles = 3;
            dificultad = 9;
            difPorcentaje = 70;
            tamañoCampo = 3;
            tamañoJugadores = 1;
            velocidadJugador = 1;
            velocidadPelota = 1;
        }
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
                Console.WriteLine($"3. Tamaño del campo ({this.tamañoCampo})");
                Console.WriteLine($"4. Tamaño jugadores ({this.tamañoJugadores})");
                Console.WriteLine($"5. Velocidad jugador ({this.velocidadJugador})");
                Console.WriteLine($"6. Velocidad pelota ({this.velocidadPelota})");
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

                    case "5":
                        Console.WriteLine("Opción 5 seleccionada");
                        break;

                    case "6":
                        Console.WriteLine("Opción 6 seleccionada");
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
            } while (!succesMenu);
            
        }
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

        
        private void TamañoCampo()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("AJUSTAR TAMAÑO DEL CAMPO");
                Console.WriteLine("Elige tamaño del campo:\n1. MUY PEQUEÑO\n2. PEQUEÑO\n3. NORMAL\n4. GRANDE\n5. MUY GRANDE");
                succes = int.TryParse(Console.ReadLine(), out dificultad);

            } while (!succes || !(dificultad >= 1) || !(dificultad <= 5));

            //TODO
            switch (dificultad)
            {
                case 1:
                    this.SetSizeCampo(1);
                    Console.WriteLine($"Tamaño del campo MUY PEQUEÑO establecido.");
                    break;
                case 2:
                    this.SetSizeCampo(2);
                    Console.WriteLine($"Tamaño del campo PEQUEÑO establecido.");
                    break;
                case 3:
                    this.SetSizeCampo(3);
                    Console.WriteLine($"Tamaño del campo NORMAL establecido.");
                    break;
                case 4:
                    this.SetSizeCampo(4);
                    Console.WriteLine($"Tamaño del campo GRANDE establecido.");
                    break;
                case 5:
                    this.SetSizeCampo(5);
                    Console.WriteLine($"Tamaño del campo MUY GRANDE establecido.");
                    break;
            }

            Thread.Sleep(1500);
        }        

        private void TamañoJugador()
        {

        }

        private void VelocidadJugador()
        {

        }

        private void VelocidadPelota()
        {

        }

        //GETTER
        public bool getMenuProgram() => this.menuProgram;
        public int getLimGol() => this.limiteGoles;
        public int getDificultad() => this.dificultad;
        public int getDifPorcentaje() => this.difPorcentaje;
        public int getSizeCampo() => this.tamañoCampo;
        public int getSizeJugador() => this.tamañoJugadores;
        public int getVelJugador() => this.velocidadJugador;
        public int getVelPelota() => this.velocidadPelota;


        //SETTER
        private void SetMenProgram(bool menuProgram)
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
        private void SettSizeJugador(int tamañoJugadores)
        {
            this.tamañoJugadores = tamañoJugadores;
        }
        private void SetVelJugador(int velocidadJugador)
        {
            this.velocidadJugador= velocidadJugador;
        }
        private void SetVelPelota(int velocidadPelota)
        {
            this.velocidadPelota= velocidadPelota;
        }

    }
}
