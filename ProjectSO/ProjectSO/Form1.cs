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

namespace ProjectSO
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Conectar_but_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9200);
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
        }

        private void Desconectar_but_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
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

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("Los usuarios que han jugado en esta fecha son: " + mensaje);
            }
            else if (mail_rb.Checked)
            {
                string mensaje = "4/" ;
                // Enviamos al servidor la petición
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("Los mails de las personas con récord de puntos son: " + mensaje);
            }
            else if (ganadores_rb.Checked)
            {
                string mensaje = "5/" + puntos_tb.Text;
                // Enviamos al servidor la fecha tecleada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
              
                MessageBox.Show("Los usuarios que han ganado con más de " + puntos_tb.Text+" son: " + mensaje);
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
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if(mensaje == "SI"){
                MessageBox.Show("¡Registrado correctamente!");
            }
            else if (mensaje == "NO")
            {
                MessageBox.Show("¡No se ha registrado correctamente!");

            }
        }

        private void inicio_but_Click(object sender, EventArgs e)
        {
            string mensaje = "1/" + usuario_tb.Text + "/" + contra_tb.Text;
            // Enviamos al servidor la información del registro
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if (mensaje == "SI")
            {
                MessageBox.Show("¡Ha iniciado sesión correctamente!");
            }
            else if (mensaje=="NO")
            {
                MessageBox.Show("¡No se ha iniciado sesión correctamente!");

            }
        }

        private void mostrar_btn_Click(object sender, EventArgs e)
        {
            string mensaje = "6/" ;
            // Enviamos al servidor la información de la petición
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if (mensaje == "NO")
            {
                mostrar_lb.Text="No hay usuarios conectados";
            }
            else 
            {
                mostrar_lb.Text = String.Empty;
                string[] conectados = mensaje.Split('/');
                string label="";
                foreach (var conectado in conectados)
                {
                    label= $"{label}"+conectado + "\n";
                }
                mostrar_lb.Text = label;

            }
        }
    }
}
