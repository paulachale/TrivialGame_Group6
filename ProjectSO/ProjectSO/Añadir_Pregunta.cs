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
    public partial class Añadir_Pregunta : Form
    {
        string mensaje;
        string correcta;
        string categoria;
        public Añadir_Pregunta()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Añadir_but_Click(object sender, EventArgs e)
        {
            //Función que forma el mensaje que se enviará al servidor con la pregunta que el cliente desea añadir
            //a la base de datos.

            //Primero se comprueba que todos los campos contengan información.
            if (pregunta_tb.Text==null || resp1_tb.Text==null || resp2_tb.Text==null || resp3_tb.Text==null|| resp4_tb.Text==null)
            {
                MessageBox.Show("Rellena todos los campos.");
            }
            //La respuesta correcta la escogerá el cliente seleccionando uno de los radio buttons.
            else {
                //En función del radio button se incluirá la pregunta en una categoría u en otra.
                if (r1.Checked)
                {
                    correcta = "1";
                }
                if (r2.Checked)
                {
                    correcta = "2";
                }
                if (r3.Checked)
                {
                    correcta = "3";
                }
                if (r4.Checked)
                {
                    correcta = "4";
                }
                if (rb1.Checked)
                {
                    categoria = "1";
                }
                if (rb2.Checked)
                {
                    categoria = "2";
                }
                if (rb3.Checked)
                {
                    categoria = "3";
                }
                if (rb4.Checked)
                {
                    categoria = "4";
                }
                if (rb5.Checked)
                {
                    categoria = "5";
                }
                if (rb6.Checked)
                {
                    categoria = "6";
                }
                //Se constituye el mensaje y se cierra el form.
                //18/categoría/pregunta/respuesta1/respuesta2/respuesta3/respuesta4/correcta.
                this.mensaje = "18/" + categoria + ",'" + pregunta_tb.Text + "','1." + resp1_tb.Text + "','2." + resp2_tb.Text + "','3." + resp3_tb.Text + "','4." + resp4_tb.Text + "',"
                    + correcta + "/";
                this.Close();
            }




        }
        public string GetMensaje()
        {
            //Devuelve el mensaje de añadir la pregunta.
            return this.mensaje;
        }

        private void r3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
