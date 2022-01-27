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
    public partial class IniciarSesion : Form
    {
        string usuario;
        string contra;
        public IniciarSesion()
        {
            InitializeComponent();
        }

        public string GetUsuario()
        {
            //Devuelve al form principal el usuario que inicia sesión.
            return (this.usuario);
        }
        public string GetContra()
        {
            //Devuelve al form principal la contraseña.
            return (this.contra);
        }


        private void Regis_but_Click_1(object sender, EventArgs e)
        {
            //Pone las variables. 
            this.usuario = usuario1_tb.Text;
            this.contra = contra1_tb.Text;
            this.Close();

        }
    }
}
