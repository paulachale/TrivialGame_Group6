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
using ClasesPosiciones;

namespace ProjectSO
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        string miUsuario;
        int numPartida;
        int iniciado;
        //public Tablero tablero;
        public List<Tablero> formularios = new List<Tablero>();
        Thread T;

        delegate void DelegadoParaRellenarTabla(string [] mensaje); //Necesitamos este delegado para poder modificar la tabla de conectados
        
       



        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.iniciado = 0; //Esta variable esta puesta para saber si el usuario puede hacer consultas o no (si ha iniciado sesión o no)
        }
       
        public void RellenaDataGridConectados(string[] trozos) 
            //Esta función rellena el datagridview de los usuarios conectados. Le pasamos una vector de carácteres en los que le pasamos el
            //número de conectados que hay en ese momento y los nombres de cada uno. Los pondrá en el orden que le lleguen, a no ser que sea él mismo.
        {
            conectados_dgv.RowHeadersVisible = false;
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
        public void PonerEnMarchaFormulario(string primerturno, ListaPosiciones miLista)
            //Función que inicializa el formulario, llegándole el nombre de la primera persona que jugará (que siempre será el que 
            //invite a ugar la partida) e inicializa también la lista de posiciones.
        {
            Tablero formulario = new Tablero(this.miUsuario, this.numPartida,this.server, miLista);
            // Está explicado en el respectivo código la manera de funcionar del formulario Tablero.
            formularios.Add(formulario);
            this.formularios[0].TomaTurno(primerturno);
            formularios[0].ShowDialog();
        }
        
        private void AtenderServidor()
        {
            while (true)
            {

                string mensaje;
                byte[] msg2 = new byte[200];
                server.Receive(msg2);
                string mensaje3= Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string [] trozos = mensaje3.Split('/');
                
                
                int codigo = Convert.ToInt32(trozos[0]);
                
                switch (codigo)
                {
                    case 1: // Caso en el que se recibe si se ha podido iniciar sesión correctamente o no. Llevará un SI o un NO,
                        //dependiendo de si se ha realizado con éxito o no, respectivamente. Independientemente de lo que haya pasado
                        //el servidor, se abrirá un messagebox indicando la resolución de la consulta.

                        mensaje = trozos[1];
                        if (mensaje == "SI")
                        {
                            MessageBox.Show("¡Ha iniciado sesión correctamente!");
                            iniciado = 1;
                            
                        }
                        else if (mensaje=="NO")
                        {
                            MessageBox.Show("¡No se ha iniciado sesión correctamente!");

                        }
                        break;
                    case 2: //Cuando recibe si se ha reistrado un usuario correctamente o no. En el servidor se comprueba que no exista 
                        //ningún usuario con ese mismo nombre. Igual que en el caso anterior, aparecerá un messagebox indicando qué ha pasado.
                        //Igual que en el caso 1, lleva un sí o un no para saberlo.
                        mensaje = trozos[1];
                        if (mensaje == "SI")
                        {
                            MessageBox.Show("¡Registrado correctamente!");
                        }
                        else if (mensaje == "NO")
                        {
                            MessageBox.Show("¡No se ha registrado correctamente!");

                        }
                        break;
                    case 3: //Consulta que indica qué usuario ha ganado más partidas. El formato del mensaje es 3/n, donde n es el nombre del usuario
                        //que ha ganado más partidas.
                        mensaje = trozos[1];
                        MessageBox.Show("El usuario que ha ganado más partidas es: " + mensaje);
                        break;
                    case 4://Consulta que indica el mail del usuario que se ha pedido.. El formato del mensaje es 4/n, donde n es el mail del usuario
                        //que se ha solicitado.
                        mensaje = trozos[1];
                        MessageBox.Show("El e-mail solicitado: " + mensaje);
                        break;
                    case 5://Consulta que indica cuantas partidas ha ganado el usuario que ha iniciado sesión.
                           //El formato del mensaje es 5/n, donde n es el numero de partidas ganadas.

                        mensaje = trozos[1];
                        MessageBox.Show("Has ganado " + mensaje + " partidas." );
                        break;
                    case 6: //Este caso es cuando se recibe un mensaje del formato siguiente: 6/n/nombre1/nombre2... donde n es el numero de usuarios
                        //conectados, y seguidamente aparecen los nombres de los usuarios conectados.
                        
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
                    case 8: //Este caso lo que hace es gestionar el momento en el que se invita a una partida. El cliente recibe un mensaje
                        //con el formato 8/nombreusuarioqueinvita/nombresdelosinvitados. Envia al servidor un mensaje con el formato
                        //9/SI/nombre1... si ha aceptado para poder informarles de la respuesta o un mensaje 9/NO... si has sido el caso 
                        //contrario.
                        form_invitacion form1 = new form_invitacion();
                        string nombre = trozos[1];
                        form1.SetInvitador(nombre);
                        form1.ShowDialog();
                        int aceptar = form1.GetAceptar();
                        string respuesta;
                        if (aceptar == 0){
                            respuesta = "9/NO/" + trozos[2]; //Si ha aceptado
                        }
                        else 
                        {
                            respuesta = "9/SI/" + trozos[2]; //Si no han aceptado
                        }
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta); //Lo envia al servidor.
                        server.Send(msg);
                        break;
                    case 10: //El cliente recibe este mensaje cuando todos los usuarios invitados han aceptado. Después del código de la 
                        //petición llevará un si o un no dependiendo de si han aceptado o no. Si lleva un SI, también llevará el id de 
                        //la partida.
                        if (trozos[1] == "SI")
                        {
                            MessageBox.Show("Todos los usuarios han aceptado. ¡Empieza la partida!");
                            //se abre el form tablero
                            this.numPartida = Convert.ToInt32(trozos[2]);
                            ListaPosiciones miLista = new ListaPosiciones();
      
                            int i = 3;
                            while (i < trozos.Length)
                            {

                                Posicion pos = new Posicion();
                                PosicionJugador posicionnueva = new PosicionJugador(pos, 0, trozos[i]);
                                miLista.AñadirPosicion(posicionnueva);
                                i++;
                       
                            }
                            ThreadStart ts = delegate { PonerEnMarchaFormulario(trozos[3], miLista); };
                            this.T = new Thread(ts);
                            T.Start();
                        }
                        
                        if(trozos[1] == "NO") //En el caso de que reciban un 10/NO significará que no han aceptado y aparecerá un messagebox
                            //indicándolo.
                        {
                            MessageBox.Show("No todos los usuarios han aceptado. Para crear una nueva partida, invita a nuevos usuarios.");
                        }
                        break;
                    case 11://Recibe un cambio de turno y se lo asigna a todos los jugadores, ya que la ficha ha de cambiar para todos, no 
                        //solo el que está respondiendo preguntas. Lleva el nombre del usuario que tiene el turno.
                        string turno = trozos[1];
                        
                        formularios[0].TomaTurno(turno);

                        break;
                    case 14://Mueve una ficha de posicion. Lleva un nombre de la persona que ha movido ficha (si es el mismo que el que recibe el 
                        //mensaje, enviará un mensaje pidiendo una pregunta de la categoria correspondiente), y el número de fichas que tiene que avanzar.
                        //Con ese número usa la función MoverFichas para hacerlo.
                        string nombre1 = trozos[1];
                        int dado = Convert.ToInt32(trozos[2]);
                        formularios[0].CambiarFicha(nombre1, dado);
                        formularios[0].MoverFichas();
                        string mens;

                        if (nombre1 == miUsuario)//En caso de que el movimiento sea nuestro, enviamos un mensaje para que nos envie una pregunta. El mensaje
                            //lleva un id de la categoria de la que queremos la pregunta, la que ha tocado en función de la casilla.
                        {
                            int actualcat= formularios[0].GetActualCatTablero(nombre1);
                            
                            
                            if (actualcat != 7) //Si actualcat=7 significa que vuelve a tirar, así qeu no requiere pregunta.
                            {
                                mens = "19/" + this.miUsuario + "/" + Convert.ToString(actualcat); //19/nombredelusuario/idcategoria
                                byte[] ms = System.Text.Encoding.ASCII.GetBytes(mens);
                                server.Send(ms);

                            }
                        }                        
                        break;
                    case 15://Si alguien cae en la casilla de ganar fruta, se envia un mensaje desde el servidor para añadirla en la data grid
                        //view que hay creada para esto. El formato del mensaje es 15/nombredelapersonaquelahaganado/categoriaganada
                        string nombre2 = trozos[2];
                        int cat = Convert.ToInt32(trozos[3]);
                        formularios[0].AñadirFruta(nombre2, cat);
               
                        break;
                    case 17://Recibe un mensaje con el formato 17/personaqueloenvia/mensaje, para así añadirlo en el textbox que hay creado aora el chat.
                        string nombre3 = trozos[2];
                        string mensaje4= trozos[3];
                        formularios[0].AñadirMensajeEnElChat(nombre3,mensaje4);
                        break;
                    case 20://Lee la pregunta que se le envia para abrir el formulario (el servidor se lo envia solo a la persona que se le tiene
                        //que abrir este formulario. El formato es 20/categoriadelapregunta/pregunta,respuesta1,respuesta2,respuesta3,respuesta4/idcorrecta
                        int cate = Convert.ToInt32(trozos[2]);
                        string info1 = Convert.ToString(trozos[3]);
                        string[] info = info1.Split(','); //Dividimos la información en preguntas y respuestas para así enviarselo a la función que lo analizará.
                        int correcta = Convert.ToInt32(trozos[4]);
                        wait(2000);
                        formularios[0].HacerPregunta(cate, info, correcta);
                        
                        break;
                    case 21://Comunica respuesta de si se ha eliminado el usuario correctamente. Lo que hacemos es enviar un mensaje con el formato:
                        //21/mensajequeapareceráenelmessagebox. Directamente le pasa la info de la que se le tiene que informar al ususario.
                        MessageBox.Show(trozos[1]);
                        break;
                }

        }
        }
        private void Conectar_but_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            //IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 50069);
            //IPEndPoint ipep = new IPEndPoint(direc, 9070);
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
        //Mensaje de desconexión: solamente lleva el código con un 0/, sin ninguna inforación más.
        {
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos, ya que sabemos que se realizará el envio del mensaje pero ya no recibiremos nada más del servidor.
            atender.Abort();
            conectados_dgv.Rows.Clear();
            conectados_dgv.Refresh();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
        private void conectados_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Esta función sirve para programar las personas seleciionadas en la datagriedview de conectados a los que queremos 
            //invitar. El fondo de la celda que apretemos, aparecerá en fondo rojo. Si se vuelve a apretar, se deseleccionará y aparecerá
            //el fondo de color blaco otra vez.
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
            //En cuanto apretamos al botón de empezar partida, el cliente enviará un mensaje al servidor con el formato:
            //7/invitador/invitado1/invitado2..., para así que el propio servidor avise a todos los clientes de que invitador les ha pedido 
            //emoezar una partida.
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
            if (contador != 0) //Si realmente hay invitados o no.
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje); //Enviamos mensaje al servidor.
                server.Send(msg);
            }
        }

        private void IniciarSesion_btn_Click(object sender, EventArgs e)
        {
            IniciarSesion form = new IniciarSesion();
            form.ShowDialog();
            this.miUsuario = form.GetUsuario();
            string contra = form.GetContra();
            string mensaje = "1/" + this.miUsuario + "/" + contra; //Enviamos este mensaje al servidor con el nombre de usuario
            //y la contraseña para que este compruebe si la info es correcta y coincide.
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje); //Envio al servidor.
            server.Send(msg);
        }

        private void Registro_btn_Click(object sender, EventArgs e)
        {
            Registrarse form = new Registrarse();
            form.ShowDialog();
            string usuario = form.GetUsuario();
            string contra = form.GetContra();
            string mail = form.GetMail();
            string mensaje = "2/" + usuario + "/" + contra + "/" + mail;
            // Hacemos lo mismo que anteriormente pero ahora le enviamos un mail también ya que se tiene que registrar. El servidor comprueba
            //que no haya más usuarios con ese nombre.
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg); //Enviamos mensaje al servidor
        }
        private void consultas_btn_Click(object sender, EventArgs e)
        {
            //Sólo se puede consultar si la persona ha iniciado sesión por lo tanto usaremos la variable nombrada anteriormente
            //para comprobar si se puede realizar la acción.
            if (this.iniciado == 1)
            {
                //Si ha iniciado sesión, se abre el form con las diferentes consultas que podemos hacer.
                Consultas form = new Consultas();
                form.SetUsuario(this.miUsuario);
                form.ShowDialog();
                string mensaje = form.GetMensaje();
                //El formulario Consultas, trae aquí un mensaje según la consulta que se haya hecho, y se envia al servidor desde aquí.
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Inicia sesión para realizar consultas."); //Si no lo ha haecho, no puede conusltar nada.
            }
        }

        private void conectados_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Add_bt_Click(object sender, EventArgs e)
        {
            //Formulario para añadir una pregunta a la base de datos. Para esta consulta no hace falta que se haya inciado sesión.
            Añadir_Pregunta form = new Añadir_Pregunta();
            form.ShowDialog();
            string mensaje = form.GetMensaje();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Eliminar_bt_Click(object sender, EventArgs e)
        {
            //Se abre el formulario para eliminar el usuario que se desee. Está explicado más detalladamente en el form, pero funciona
            //como si fuese un iniciar sesión, ya que el nombre de usuario y contraseña deben coincidir.
            Eliminar form = new Eliminar();
            form.ShowDialog();
            string mensaje = form.GetMensaje();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }


        public void wait(int milliseconds)
        {
            //Esta función está creada para que cuando se haya movido la ficha, que espere unos segundos antes de abrir la 
            //pregunta y así poder ver en qué color se ha caido.
            var timer1 = new System.Windows.Forms.Timer(); 
            if (milliseconds == 0 || milliseconds < 0) 
                return; 
            // Console.WriteLine("start wait timer"); 
            timer1.Interval = milliseconds; 
            timer1.Enabled = true; 
            timer1.Start();
            timer1.Tick += (s, e) => { timer1.Enabled = false;
                timer1.Stop(); 
            }; 
            while (timer1.Enabled)
            { 
                Application.DoEvents(); 
            } 
        }   
    }
}
