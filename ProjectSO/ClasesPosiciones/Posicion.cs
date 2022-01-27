using System;

namespace ClasesPosiciones
{
    public class Posicion
    {
        //Clase que nos permitirá trabajar con las posiciones en las que se encuentre el jugador.
        public double x;
        public double y;
        public Posicion()
        {
            //Contructor de la posición inicial. 
            this.x = 40;
            this.y = 542;
        }
        public void SetX(double a)
        {
            this.x = a; //Pondremos la coordenada X.
        }
        public void SetY(double a)
        {
            this.y = a; //Coordenada Y deseada.
        }
        public double GetX()
        { return x; } //Devuelve la coordenada X de la posición.

        public double GetY()
        { return y; } //Devuelve la coordenada Y de la posición.

        

    }
}
