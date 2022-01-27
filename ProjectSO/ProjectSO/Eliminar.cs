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
    
    public partial class Eliminar : Form
    {
        string mensaje;
        public Eliminar()
        {
            InitializeComponent();
        }

        private void Regis_but_Click(object sender, EventArgs e)
        {
            //Función del botón de eliminar usuario del registo.
            //Primero miramos que todos los campos estén rellenos.
            if (usuario1_tb.Text!=null && contra1_tb.Text != null)
            {
                //20/usuario/contraseña
                this.mensaje = "20/" + usuario1_tb.Text + "/" + contra1_tb.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Rellena todos los campos");
            }

           
        }

        public string GetMensaje()
        {
            //Función que devuelve el mensaje creado para eliminar el usuario.
            return this.mensaje;
        }

    }
}
