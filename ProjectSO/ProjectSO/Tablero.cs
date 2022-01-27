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
using System.Threading;
using System.Net.Sockets;
using System.Media;
using ClasesPosiciones;
using System.IO;



namespace ProjectSO
{
    public partial class Tablero : Form
    {
        public int Turno;
        int miPosicion;
        int partida;
        string miUsuario;
        ListaPosiciones lista;
        Socket server;
        public PictureBox[] misFichas;
        public delegate void DelegadoParaCambiarTurno(string mensaje);
        public delegate void DelegadoParaMoverFichas();


        public Tablero(string miUsuario, int partida, Socket server, ListaPosiciones miLista)
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
            this.miUsuario = miUsuario;
            this.server = server;
            this.partida = partida;
            this.Turno = 0;
            this.lista = miLista;
           
           
        }
        public void CambiarLabelTurno(string mensaje)
        {
            //En función de a qué jugador le toca jugar el label muesta uno u otro mensaje.
            turno_lb.Text = mensaje;
        }
        public void MoverFichas()
        {
            
            //Función que mueve las fichas en el tablero.
            int i = 0;
            while (i < lista.GetNumeroPosiciones())
            {
                //Cada vez que se requiere el movimiento se cambia la localización de la picture box con la ficha.
                //Primero de todo se comprueban las posiciones para que las fichas no salgan superpuestas la una con la otra.
                lista.ComprobarPosiciones(i);
                misFichas[i].Location = new Point(Convert.ToInt32(lista.lista[i].posicion.GetX()), Convert.ToInt32(lista.lista[i].posicion.GetY()));
                i++;
            }
            //En este punto se requiere llamar al delegado para refrescar y actualizar el panel.
            this.Invoke(new Action(() =>
            {
                Panel.Refresh();
            }));


        }

        public void TomaTurno (string turno)
        {
            
            //Esta función permite el cambio de turno.
            string mensaje;
            if (turno == miUsuario)
            {
                //En caso de que sí sea el turno del usuario se permitirá lanzar el dado.
                this.Turno = 1;
                this.turno_lb.Text = "Es tu turno. ¡Lanza el dado!";


            }
            else
            {
                //En caso de que no sea el turno del usuario se informa de quién es el turno.
                this.Turno = 0;
                this.turno_lb.Text = "Es el turno de " + turno + ".";


            }
            
        }
      
        private void lanzarDado_btn_Click(object sender, EventArgs e)
        {
            //Función que, en caso de que sea el turno del usuario saca un número random.
            //En función del número random que salga sale una u otra imagen.
            if (Turno == 1)
            {
                int dado;
                Random aleatorio = new Random();
                dado = aleatorio.Next(1, 7);
                if (dado == 1)
                {
                    picDado.Image = Image.FromFile("cara1.jpg");
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
                //Se envía el mensaje:
                //12/número de partida/usuario/número resultante del da
                string mensaje = "12/" + Convert.ToString(this.partida) + "/" + miUsuario +"/"  + Convert.ToString(dado);
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

        private void Tablero_Load(object sender, EventArgs e)
        {
            //Cuando se cargue el tablero se introduce el data grid view con el recuento de frutas
            //que llevan los jugadores.
            resultados_dgv.RowCount = 4;
            resultados_dgv.RowHeadersVisible = false;
            int num = this.lista.GetNumeroPosiciones();
            resultados_dgv.ColumnCount = num * 3 - 1;
            int i = 0;
            int pos = 0;
            int m = 1;
            while (m < num)
            {
                //Ponemos una separación entre las tablas de cada jugador.
                resultados_dgv.Columns[m * 3 - 1].DefaultCellStyle.BackColor = Color.Black;
                m++;
            }
            while (pos < num)
            {
                //Ponemos los nombres de los jugadores y una foto de su respectiva ficha.
                resultados_dgv.Rows[0].Cells[i].Value = this.lista.lista[pos].GetNombre();
                string image2 = "ficha" + Convert.ToString(pos + 1) + ".png";
                Bitmap bmp = new Bitmap(image2);
                Bitmap resized = new Bitmap(bmp, new Size(bmp.Width / 4, bmp.Height / 4));

                DataGridViewImageCell iCell = new DataGridViewImageCell();
                iCell.Value = resized;

                resultados_dgv.Rows[0].Cells[i+1] = iCell;
                i = i + 3; 
                pos++;
            }
            
            
            i = 0;
            this.Controls.Add(Panel);
            misFichas = new PictureBox[lista.GetNumeroPosiciones()];
            while (i < lista.GetNumeroPosiciones())
            {
                //Creación de las picture boxes de las fichas.
                //Las pondremos en la posición 0 y comprobaremos que no nos quedan superpuestas.
                lista.ComprobarPosiciones(i);
                PictureBox ficha = new PictureBox();
                ficha.Width = 20;
                ficha.Height = 20;
                ficha.ClientSize = new Size(20, 20);
                

                ficha.SizeMode = PictureBoxSizeMode.StretchImage;
                string foto = "ficha" + Convert.ToString(i+1)+ ".png";
                Bitmap image = new Bitmap(foto);
                this.DoubleBuffered = false;
                ficha.Image = (System.Drawing.Image)image;
                ficha.Location = new Point(Convert.ToInt32(lista.lista[i].posicion.GetX()),Convert.ToInt32(lista.lista[i].posicion.GetY()));
                misFichas[i] = ficha;
                Panel.Controls.Add(ficha);
                ficha.Tag = i;
             
                i++;
            }
            //Ponemos a nula la text box del chat.
            ChatTextBox.Text = null;

        }
        public void CambiarFicha(string nombre, int dado)
        {
            //Cada vez que se lanza un dado la ficha tiene que avanzar:
            int posicion = this.lista.GetPosicionLista(nombre);
            this.lista.lista[posicion].AvanzaPosicion(dado);
            this.lista.lista[posicion].CambiaPosicion();
            this.lista.ComprobarPosiciones(posicion);
            

            //DelegadoParaMoverFichas delegado2 = new DelegadoParaMoverFichas(MoverFichas);
            //Panel.Invoke(delegado2, new object[] { });

        }
        public void HacerPregunta(int actualcat, string [] trozos,int correcta)
        {
            
            //Función que llama al form de la pregunta y le introduce los parámetros necesarios para que pueda salir correctamente.
          
            Pregunta form = new Pregunta();
            form.PonerIcono(actualcat); //Pondrá la foto de fondo del form en función de la categoría.
            form.SetCorrecta(correcta); //Informará de cuál es la respuesta correcta.
            form.PonerPreguntarRespuesta(trozos);
            form.ShowDialog();
            int acertado = form.GetAcertado();
            int posicion= lista.GetPosicionLista(miUsuario);
            int premio = lista.lista[posicion].GetPremio();
            //En caso de que haya caído en una casilla con posibilidad de premio, de conseguir fruta y ha acertado:
            //Se monta el mensaje que informa de ello al servidor.
            if ((acertado == 1)&&(premio==1))
            {
                int nueva=lista.lista[posicion].ComprobarCategoria(actualcat);
                if (nueva==1)
                {
                    //ponemos fruta.
                    string mensaje = "15/" + Convert.ToString(this.partida) + "/" + miUsuario + "/" + Convert.ToString(actualcat);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                }


            }
            //En caso de que haya acertado pero no se encuentre en una casilla de premio debe seguir jugando.
            if ((acertado == 1) && (premio == 0))
            {
                DelegadoParaCambiarTurno delegado1 = new DelegadoParaCambiarTurno(CambiarLabelTurno);
                turno_lb.Invoke(delegado1, new object[] { "¡Vuelve a tirar!" });
            }
            //Si no ha acertado la pregunta se produce cambio de turno y se envía un mensaje al servidor informando de ello.
            if (acertado == 0)
            {
                string mensaje = "13/" + Convert.ToString(this.partida) + "/" + miUsuario;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }



        }
        public void AñadirFruta(string nombre, int cat)
        {
            //Esto sucede cuando se ha caído en una casilla de premio y se ha acertado la pregunta.
            int posicion = lista.GetPosicionLista(nombre);
            int ganado=lista.lista[posicion].SetCategoria(cat);
            //Actualizamos el data grid:
            if (cat == 1)
            {
                //Si ha ganado un plátano.
                resultados_dgv.Rows[1].Cells[posicion * 3].Style.BackColor = Color.Yellow;
            }
            if (cat == 2)
            {
                //Si ha ganado un arándano.
                resultados_dgv.Rows[2].Cells[posicion * 3].Style.BackColor = Color.Blue;
            }

            if (cat == 3)
            {
                //Si ha ganado una frambuesa.
                resultados_dgv.Rows[3].Cells[posicion * 3].Style.BackColor = Color.Red;
            }
            if (cat == 4)
            {
                //Si ha ganado un kiwi.
                resultados_dgv.Rows[1].Cells[posicion * 3+1].Style.BackColor = Color.Green;
            }
            if (cat == 5)
            {
                //Si ha ganado una frambuesa.
                resultados_dgv.Rows[2].Cells[posicion * 3+1].Style.BackColor = Color.Magenta;
            }
            if (cat == 6)
            {
                //Si ha ganado una naranja.
                resultados_dgv.Rows[3].Cells[posicion * 3+1].Style.BackColor = Color.Orange;
            }
            if (ganado == 1)
            {
                //Miramos si con la fruta ganada llega o no a ganar la partida.
                if (nombre == miUsuario)
                {
                    string mensaje = "16/" + Convert.ToString(this.partida) + "/" + miUsuario;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    MessageBox.Show("Has ganado la partida. ¡Felicidades!");   
                }
                else
                {
                    MessageBox.Show("La partida ha finalizado. ¡El ganador@ es"+ miUsuario+ "!");
                }
                this.Close();
            }
         
        }
        public int GetActualCatTablero(string nombre)
        {
            //Devuelve al form principal la categoría actual de la casilla en la que se encuentra el jugador.
            int pos = lista.GetPosicionLista(nombre);
            return (lista.lista[pos].GetActualCat());
        }

        private void enviar_btn_Click(object sender, EventArgs e)
        {
            //Esto es el mensaje que se monta para enviar al servidor el mensaje que el jugador quiere enviar a los demás jugadores mediante el chat.
            string mensaje ="17/"+ Convert.ToString(this.partida)+"/"+ miUsuario+"/"+ MensajeBox.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MensajeBox.Text = null;
        }

   
        public void AñadirMensajeEnElChat(string nombre, string mensaje)
        {
            //Mostramos el mensaje enviado en el chat.
            if (ChatTextBox.Text == " ")
                ChatTextBox.Text = nombre + ": " + mensaje;
            else
                ChatTextBox.Text = ChatTextBox.Text+ "\r\n"  + nombre+ ": "+ mensaje;
            


        }


      

        private void resultados_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
