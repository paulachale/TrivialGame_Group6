using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ProjectSO
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        string miUsuario;
        int numPartida;
        //public Tablero tablero;
        public List<Tablero> formularios = new List<Tablero>();
        Thread T;

        delegate void DelegadoParaRellenarTabla(string [] mensaje);
       



        public Form1()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
       
        public void RellenaDataGridConectados(string[] trozos)
        {
            if (Convert.ToInt32(trozos[1]) != 1)
            {

                conectados_dgv.RowCount = Convert.ToInt32(trozos[1]) - 1;
                conectados_dgv.ColumnCount = 1;
                conectados_dgv.Columns[0].HeaderText = "Nombre";
                int i = 1;//posicion en trozos
                int m = 1;//posicion en data grid
                while (i <= Convert.ToInt32(trozos[1]))
                {
                    if (trozos[i + 1] != this.miUsuario)
                    {
                        conectados_dgv.Rows[m - 1].Cells[0].Value = trozos[i + 1];
                        m++;
                    }

                    i++;
                }
            }
        }
        public void PonerEnMarchaFormulario(string primerturno)
        {
            Tablero formulario = new Tablero(this.miUsuario, this.numPartida,this.server);
            formularios.Add(formulario);
            this.formularios[0].TomaTurno(primerturno);
            formularios[0].ShowDialog();
        }
        
        private void AtenderServidor()
        {
            while (true)
            {
                string mensaje;
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string [] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                
                int codigo = Convert.ToInt32(trozos[0].Split('\0')[0]);
                
                switch (codigo)
                {
                    case 1: //Respuesta a log in
                            //Recibimos la respuesta del servidor

                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "SI")
                        {
                            MessageBox.Show("¡Ha iniciado sesión correctamente!");
                            
                        }
                        else if (mensaje=="NO")
                        {
                            MessageBox.Show("¡No se ha iniciado sesión correctamente!");

                        }
                        break;
                    case 2:
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "SI")
                        {
                            MessageBox.Show("¡Registrado correctamente!");
                        }
                        else if (mensaje == "NO")
                        {
                            MessageBox.Show("¡No se ha registrado correctamente!");

                        }
                        break;
                    case 3:
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("Los usuarios que han jugado en esta fecha son: " + mensaje);
                        break;
                    case 4:
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("Los mails de las personas con récord de puntos son: " + mensaje);
                        break;
                    case 5:
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("Los usuarios que han ganado con más de " + puntos_tb.Text + " son: " + mensaje);
                        break;
                    case 6:
                        
                        if (trozos[1] == "0")
                        {
                            mostrar_lb.Text = "No hay usuarios conectados";
                        }
                        else
                        {
                            DelegadoParaRellenarTabla delegado = new DelegadoParaRellenarTabla(RellenaDataGridConectados);
                            conectados_dgv.Invoke(delegado, new object[] { trozos });

                            
               
                        }
                        break;
                    case 8:
                        form_invitacion form1 = new form_invitacion();
                        string nombre = trozos[1];
                        form1.SetInvitador(nombre);
                        form1.ShowDialog();
                        int aceptar = form1.GetAceptar();
                        string respuesta;
                        if (aceptar == 0){
                            respuesta = "9/NO/" + trozos[2];
                        }
                        else 
                        {
                            respuesta = "9/SI/" + trozos[2];
                        }
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta);
                        server.Send(msg);
                        break;
                    case 10:
                        if (trozos[1] == "SI")
                        {
                            MessageBox.Show("Todos los usuarios han aceptado. ¡Empieza la partida!");
                            //se abre el form tablero
                            this.numPartida = Convert.ToInt32(trozos[2]);
                            ThreadStart ts = delegate { PonerEnMarchaFormulario(trozos[3]); };
                            this.T = new Thread(ts);
                            T.Start();
                        }
                        if(trozos[1] == "NO")
                        {
                            MessageBox.Show("No todos los usuarios han aceptado. Para crear una nueva partida, invita a nuevos usuarios.");
                        }
                        break;
                    case 11://recibe un cambio de turno y se lo asigna a todos los jugadores
                        string turno = trozos[1].Split('\0')[0];
                        
                        formularios[0].TomaTurno(turno);

                        break;

                }

        }
        }
        private void Conectar_but_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            //IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            //IPEndPoint ipep = new IPEndPoint(direc, 50069);
            IPEndPoint ipep = new IPEndPoint(direc, 9050);
            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            //Pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        private void Desconectar_but_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            conectados_dgv.Rows.Clear();
            conectados_dgv.Refresh();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void consulta_but_Click(object sender, EventArgs e)
        {
            if (fecha_rb.Checked)
            {
                string mensaje = "3/" + fecha_tb.Text;
                // Enviamos al servidor la fecha tecleada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

               
            }
            else if (mail_rb.Checked)
            {
                string mensaje = "4/" ;
                // Enviamos al servidor la petición
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

              
            }
            else if (ganadores_rb.Checked)
            {
                string mensaje = "5/" + puntos_tb.Text;
                // Enviamos al servidor la fecha tecleada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                
            }
        }

        private void Regis_but_Click(object sender, EventArgs e)
        {
            string usuario = usuario1_tb.Text;
            string contra = contra1_tb.Text;
            string mail = mail_tb.Text;
            string mensaje = "2/" + usuario + "/" + contra + "/" + mail;


           
            
            // Enviamos al servidor la información del registro
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
            //Recibimos la respuesta del servidor
            
        }

        private void inicio_but_Click(object sender, EventArgs e)
        {
            this.miUsuario = usuario_tb.Text; //Guardamos nuestro nombre de usuario
            string mensaje = "1/" + usuario_tb.Text + "/" + contra_tb.Text;
            // Enviamos al servidor la información del registro
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void conectados_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conectados_dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.White)
            {
                conectados_dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
            }
            else
            {
                conectados_dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            }
           
        }

        private void invitar_bt_Click(object sender, EventArgs e)
        {
            int i = 0;
            int contador = 0;
            string mensaje = "7/"+miUsuario;
            while (i < conectados_dgv.RowCount)
            {
                if ((conectados_dgv.Rows[i].Cells[0].Style.BackColor == Color.Red)&&( conectados_dgv.Rows[i].Cells[0].Value != miUsuario))
                {
                    mensaje = mensaje +"/"+ conectados_dgv.Rows[i].Cells[0].Value;
                    contador++;
                }
                i = i + 1;
            }
            if (contador != 0)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        

        
    }
}
