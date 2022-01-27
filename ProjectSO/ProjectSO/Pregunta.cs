using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectSO
{
    public partial class Pregunta : Form
    {
        int tiempo;
        int haacertado;
        int correcta;
        public Pregunta()
        {
            InitializeComponent();
        }

        
        public void PonerIcono(int actualcat)
        {
            //En función de casilla en la que caiga el cliente, la pregunta le saldrá de una u otra categoría.
            string foto;
            if (actualcat == 1)
            {
                //Categoría 1: Plátano.
                foto = "cat1.png";
                
            }
            else if (actualcat == 2)
            {
                //Categoría 2: Arándano.
                foto = "cat2.png";
            }
            else if (actualcat == 3)
            {
                //Categoría 3: Frambuesa.
                foto = "cat3.png";
            }
            else if (actualcat == 4)
            {
                //Categoría 4: Kiwi.
                foto = "cat4.png";
            }
            else if (actualcat == 5)
            {
                //Categoría 5: Higo.
                foto = "cat5.png";
            }
            else if (actualcat == 6)
            {
                //Categoría 6: Naranja.
                foto = "cat6.png";
            }
            else
            {
                foto = "drama.png";
            }
            //PictureBox monigote = new PictureBox();
            //monigote.Width = 150;
            //monigote.Height = 150;
            //monigote.ClientSize = new Size(150, 150);

            //monigote.SizeMode = PictureBoxSizeMode.StretchImage;

            //Bitmap image = new Bitmap(foto);
            //monigote.Image = (System.Drawing.Image)image;
            //monigote.Location = new Point(200, 200);
            //PanelPregunta.Controls.Add(monigote);
            this.BackgroundImage = Image.FromFile(foto);


        }

        private void Pregunta_Load(object sender, EventArgs e)
        {
            //El jugador no puede estar más de 20 segundos respondiendo a la pregunta.
            //Si tarda más de 20 segundos en responder se cierra el form y falla la partida.
            timer1.Start();
            this.tiempo = 20;
            labelTimer.Text = Convert.ToString(tiempo);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Función del timer que indica que cuando el tiempo sea 0 se cierre el form.
            this.tiempo = this.tiempo-1;
            labelTimer.Text = Convert.ToString(tiempo);
            if (this.tiempo == 0)
            {
                timer1.Stop();
                this.Close();
            }
                
        }
        public int GetAcertado()
        {
            //Devuelve al form principal si el jugador ha acertado la pregunta o no.
            return (this.haacertado);
        }
        public void SetCorrecta(int correcta)
        {
            //Pone el número de la respuesta correcta que le viene de la base de datos.
            this.correcta = correcta;
        }
        public void PonerPreguntarRespuesta(string[] trozos)
        {
            //Display de la pregunta en un label y de las respuestas en distintos botones.
            labelPregunta.Text = trozos[0];
            button1.Text = trozos[1];
            button2.Text = trozos[2];
            button3.Text = trozos[3];
            button4.Text = trozos[4];


        }
        //En las funciones siguientes se compara si la respuesta introducida por el jugador
        //es la correcta.
        //Si es la correcta la varibale de haacertado se pone a 1.
        private void button1_Click(object sender, EventArgs e)
        {

            if (this.correcta == 1)
                this.haacertado = 1;
            this.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.correcta == 2)
                this.haacertado = 1;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.correcta == 3)
                this.haacertado = 1;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.correcta == 4)
                this.haacertado = 1;
            this.Close();
        }
    }
}
