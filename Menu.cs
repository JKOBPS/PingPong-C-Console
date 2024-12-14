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
        int limiteGoles, dificultad, tamañoCampo, tamañoJugadores, velocidadJugador, velocidadPelota;
        string menu;

        public Menu()
        {
            menuProgram = false;
            limiteGoles = 3;
            dificultad = 1;
            tamañoCampo = 1;
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
                Console.WriteLine($"2. Dificultad ({this.dificultad})");
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
                Console.WriteLine("Elige la dificultad del partido\n1. FÁCIL\n2. NORMAL\n3. DIFÍCIL");
                succes = int.TryParse(Console.ReadLine(), out dificultad);

            } while (!succes || !(dificultad >= 1) || !(dificultad <= 3));

            this.SetDificultad(dificultad);
            Console.WriteLine($"Nivel de dificultad {dificultad} establecido");
            Thread.Sleep(1500);
        }

        //TODO
        private void TamañoCampo()
        {

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
