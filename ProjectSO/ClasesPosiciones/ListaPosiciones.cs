using System;
using System.Collections.Generic;
using System.Text;

namespace ClasesPosiciones
{
    public class ListaPosiciones
    {
        public List<PosicionJugador> lista = new List<PosicionJugador>();
        //Lista con varias clases de PosicionJugador.
        public void AñadirPosicion(PosicionJugador posicionJugador)
        {
            lista.Add(posicionJugador); //Hay una posición jugador por cada jugador.
        }
        public int GetNumeroPosiciones()
        {
            return this.lista.Count; //Devuelve el número de posiciones.
        }
        public int GetPosicionLista(string nombre)
        {
            //Devuelve a partir de un nombre la posición de este en la lista.
            int encontrado = 0;
            int i = 0;
            int posicion=0;
            while (encontrado == 0)
            {
                if (lista[i].GetNombre() == nombre)
                {
                    encontrado = 1;
                    posicion = i;
                }
                i++;
            }
            return (posicion);
        }
        public void ComprobarPosiciones(int posicionlista)
        {
            //Comprueba que las posiciones de los jugadores no se superpongan y dilata su localización en caso de que superpongan.
            int i = 0;
            int encontrados = 0;
            int idpos = this.lista[posicionlista].GetIDPosicion();
            while ((i < lista.Count) && (i < posicionlista))
            {
                if (i != posicionlista)
                {
                    if (idpos == lista[i].GetIDPosicion())
                    {
                        encontrados = encontrados + 1;
                    }
                }
                i++;
    
            }
            if (encontrados != 0)
            {
                double x=lista[posicionlista].posicion.GetX();
                lista[posicionlista].posicion.SetX(x+encontrados*20);
            }
        }

    }
}
