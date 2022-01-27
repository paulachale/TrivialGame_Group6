using System;
using System.Collections.Generic;
using System.Text;

namespace ClasesPosiciones
{
    public class PosicionJugador
    {
        public Posicion posicion;
        //número de casilla donde nos encontramos.
        public int idposicion;
        public string miUsuario;
        int[] categorias;
 
        int actualcat;
        int frutas;
        int premio;
       

       
        public PosicionJugador( Posicion pos, int idpos, string usuario)
        {
            //Constructor de la posición del jugador.
            this.posicion = pos; //posición que accede a la clase Posición
            this.idposicion = idpos; //identificador de la posición que tendrá unas respectivas coordenadas.
            this.miUsuario = usuario; //usuario con el que tratamos.
            this.categorias = new int[6]; //vector de integers con las distintas categorías puestas a 0.
            int i = 0;
            while (i < 6)
            {
                this.categorias[i] = 0;
                i++;
            }
            this.actualcat = 0;
            this.premio = 0;
            this.frutas = 0;

        }
        public void AvanzaPosicion(int avance)
        { 
            //Calculamos la nueva posición cuando tenemos que avanzar.
            this.idposicion = this.idposicion + avance;
            if (this.idposicion >= 27)
            {
                this.idposicion = this.idposicion-26;
            }
        }
        public void CambiaPosicion()
        {
            //Cambiamos las coordenadas en función de en qué id posición nos encontremos.
            if (this.idposicion == 0)
            {
                this.posicion.SetX(40);
                this.posicion.SetY(542);
                this.actualcat = 0;
                this.premio = 0;
            }
            if (this.idposicion == 1)
            {
                this.posicion.SetX(40);
                this.posicion.SetY(469);
                this.actualcat = 6;
                this.premio = 1;
            }
            if (this.idposicion == 2)
            {
                this.posicion.SetX(40);
                this.posicion.SetY(388);
                this.actualcat = 3;
                this.premio = 0;
            }
            if (this.idposicion == 3)
            {
                this.posicion.SetX(40);
                this.posicion.SetY(308);
                this.actualcat = 2;
                this.premio = 0;
            }
            if (this.idposicion == 4)
            {
                this.posicion.SetX(40);
                this.posicion.SetY(222);
                this.actualcat = 7;
                this.premio = 0;
            }
            if (this.idposicion == 5)
            {
                this.posicion.SetX(40);
                this.posicion.SetY(138);
                this.actualcat = 5;
                this.premio = 0;
            }
            if (this.idposicion == 6)
            {
                this.posicion.SetX(40);
                this.posicion.SetY(50);
                this.actualcat = 2;
                this.premio = 1;
            }
            if (this.idposicion == 7)
            {
                this.posicion.SetX(164);
                this.posicion.SetY(50);
                this.actualcat = 6;
                this.premio = 0;
            }
            if (this.idposicion == 8)
            {
                this.posicion.SetX(273);
                this.posicion.SetY(50);
                this.actualcat = 1;
                this.premio = 0;
            }
            if (this.idposicion == 9)
            {
                this.posicion.SetX(390);
                this.posicion.SetY(50);
                this.actualcat = 4;
                this.premio = 0;
            }
            if (this.idposicion == 10)
            {
                this.posicion.SetX(500);
                this.posicion.SetY(50);
                this.actualcat = 3;
                this.premio = 1;
            }
            if (this.idposicion == 11)
            {
                this.posicion.SetX(623);
                this.posicion.SetY(50);
                this.actualcat = 5;
                this.premio = 0;
            }
            if (this.idposicion == 12)
            {
                this.posicion.SetX(740);
                this.posicion.SetY(50);
                this.actualcat = 2;
                this.premio = 0;
            }
            if (this.idposicion == 13)
            {
                this.posicion.SetX(854);
                this.posicion.SetY(50);
                this.actualcat = 3;
                this.premio = 0;
            }
            if (this.idposicion == 14)
            {
                this.posicion.SetX(975);
                this.posicion.SetY(50);
                this.actualcat = 4;
                this.premio = 1;
            }
            if (this.idposicion == 15)
            {
                this.posicion.SetX(975);
                this.posicion.SetY(138);
                this.actualcat = 1;
                this.premio = 0;
            }
            if (this.idposicion == 16)
            {
                this.posicion.SetX(975);
                this.posicion.SetY(222);
                this.actualcat = 6;
                this.premio = 0;
            }
            if (this.idposicion == 17)
            {
                this.posicion.SetX(975);
                this.posicion.SetY(308);
                this.actualcat = 7;
                this.premio = 0;
            }
            if (this.idposicion == 18)
            {
                this.posicion.SetX(975);
                this.posicion.SetY(388);
                this.actualcat = 4;
                this.premio = 0;
            }
            if (this.idposicion == 19)
            {
                this.posicion.SetX(975);
                this.posicion.SetY(469);
                this.actualcat = 1;
                this.premio = 1;
            }
            if (this.idposicion == 20)
            {
                this.posicion.SetX(854);
                this.posicion.SetY(469);
                this.actualcat = 5;
                this.premio = 0;
            }
            if (this.idposicion == 21)
            {
                this.posicion.SetX(740);
                this.posicion.SetY(469);
                this.actualcat = 6;
                this.premio = 0;
            }
            if (this.idposicion == 22)
            {
                this.posicion.SetX(623);
                this.posicion.SetY(469);
                this.actualcat = 2;
                this.premio = 0;
            }
            if (this.idposicion == 23)
            {
                this.posicion.SetX(500);
                this.posicion.SetY(469);
                this.actualcat = 5;
                this.premio = 1;
            }
            if (this.idposicion == 24)
            {
                this.posicion.SetX(390);
                this.posicion.SetY(469);
                this.actualcat = 1;
                this.premio = 0;
            }
            if (this.idposicion == 25)
            {
                this.posicion.SetX(273);
                this.posicion.SetY(469);
                this.actualcat = 3;
                this.premio = 0;
            }
            if (this.idposicion == 26)
            {
                this.posicion.SetX(164);
                this.posicion.SetY(469);
                this.actualcat = 4;
                this.premio = 0;
            }

        }
        public int GetIDPosicion()
        {
            return (this.idposicion); //Devuelve el id posición.
        }
        public string GetNombre()
        {
            return (this.miUsuario); //Devuelve el nombre del usuario.
        }
        public int GetActualCat()
        {
            return (this.actualcat); //Devuelve la categoría en la que se encuentra el jugador.
        }
        public int GetPremio()
        {
            return (this.premio); //Devuelve la variable premio que informa si el jugador ha caído en una casilla que permite ganar fruta o no.
        }
        public int SetCategoria(int actualcat)
        {
            //Establece la categoría del jugador.
            this.categorias[actualcat - 1] = 1;
            this.frutas++;
            int ganado = 0;

            if (frutas == 6)
                ganado = 1;

            return (ganado);
        }
        public int ComprobarCategoria(int actualcat)
        {
            //Comprueba la categoría en la que se encuentra el jugador.
            int nueva = 0;
            if (this.categorias[actualcat - 1] == 0)
            {
                nueva = 1;
            }
            return (nueva);
        }
    }
}
