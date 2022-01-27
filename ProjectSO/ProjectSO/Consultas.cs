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
    public partial class Consultas : Form
    {
        string mensaje;
        string miUsuario;
        public Consultas()
        {
            InitializeComponent();
        }

        private void consulta_but_Click(object sender, EventArgs e)
        {
            //Cuando se hace click en el botón consulta se constituye el mensaje que se enviará al servidor para hacer la consulta.
            //El mensaje será uno u otro en función del radio button que se seleccione.
            if (top_rb.Checked)
            {
                //Mensaje de consulta del jugador que ha ganado más puntos.
                //3/
                this.mensaje = "3/";

            }
            else if (mail_rb.Checked)
            {
                //Mensaje de consulta del email de un jugador en concreto.
                //4/nombre.
                this.mensaje = "4/" + nombre_tb.Text;

            }
            else if (tuspartidas_rb.Checked)
            {
                //Mensaje de consulta de cuántas partidas ha ganado el usuario.
                //5/miUsuario.
                this.mensaje = "5/"+miUsuario;

            }
            this.Close();
           

        }
       
        public string GetMensaje()
        {
            //Función que retorna al form principal el mensaje de la consulta.
            return (this.mensaje);
        }
        public void SetUsuario(string nombre)
        {
            //Función que pasa al form el nombre del usuario.
            this.miUsuario = nombre;
        }

        private void top_rb_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
