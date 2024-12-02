using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPongC_
{
    internal class Puntos
    {
        int npcPuntos, jugadorPuntos;
        public Puntos()
        {
            npcPuntos = 0;
            jugadorPuntos = 0;
        }
        //SETTERS
        public void setNpcPts()
        {
            npcPuntos++;
        }
        public void setJugadorPts()
        {
            jugadorPuntos++;
        }
        //GETTERS
        public int getNpcPts() => npcPuntos;
        public int getJugadorPts () => jugadorPuntos;
    }
}
