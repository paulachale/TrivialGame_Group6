using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ProjectSO
{
    public partial class Tablero : Form
    {
        public int Turno;
        int miPosicion;
        int partida;
        string miUsuario;
        Socket server;
        //delegate void DelegadoParaCambiarTurno(string mensaje);

        public Tablero(string miUsuario, int partida, Socket server)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.miUsuario = miUsuario;
            this.server = server;
            this.partida = partida;
            this.Turno = 0;
        }
        public void CambiarLabelTurno(string mensaje)
        {
            turno_lb.Text = mensaje;
        }

        public void TomaTurno (string turno)
        {
            //DelegadoParaCambiarTurno delegado1 = new DelegadoParaCambiarTurno(CambiarLabelTurno);

            string mensaje;
            if (turno == miUsuario)
            {
                this.Turno = 1;
                //mensaje = "Es tu turno. ¡Lanza el dado!";
                this.turno_lb.Text = "Es tu turno. ¡Lanza el dado!";
            }
            else
            {
                this.Turno = 0;
                //mensaje = "Es el turno de " + turno + ".";
                this.turno_lb.Text = "Es el turno de " + turno + ".";

            }
            //turno_lb.Invoke(delegado1, new object[] { mensaje });

        }
        private void lanzarDado_btn_Click(object sender, EventArgs e)
        {
            if (Turno == 1)
            {
                int dado;
                Random aleatorio = new Random();
                dado = aleatorio.Next(1, 7);
                if (dado == 1)
                {
                    picDado.Image = Image.FromFile("cara1.png");
                }
                else if (dado == 2)
                {
                    picDado.Image = Image.FromFile("cara2.png");
                }
                else if (dado == 3)
                {
                    picDado.Image = Image.FromFile("cara3.png");
                }
                else if (dado == 4)
                {
                    picDado.Image = Image.FromFile("cara4.png");
                }
                else if (dado == 5)
                {
                    picDado.Image = Image.FromFile("cara5.png");
                }
                else
                {
                    picDado.Image = Image.FromFile("cara6.png");
                }
                miPosicion = miPosicion + dado;
                string mensaje = "12/" + Convert.ToString(this.partida) + "/" + miUsuario +"/"  + Convert.ToString(miPosicion);
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("No es tu turno!");
            }
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fallo_bt_Click(object sender, EventArgs e)
        {
            string mensaje = "13/" + Convert.ToString(this.partida) + "/" + miUsuario ;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

      
    }
}
