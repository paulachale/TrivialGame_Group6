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
    public partial class Registrarse : Form
    {
        string usuario;
        string contra;
        string mail;
        public Registrarse()
        {
            InitializeComponent();
        }

        private void Registrarse_Load(object sender, EventArgs e)
        {

        }
        public string GetUsuario()
        {
            //Devuelve al form principal el usuario que desea registrarse.
            return (this.usuario);
        }
        public string GetContra()
        {
            //Devuelve al form principal la contraseña del usuario que desea registrarse.
            return (this.contra);
        }
       public string GetMail()
        {
            //Devuelve al form principal el email del usuario que desea registrarse.
            return (this.mail);
        }

        private void Regis_but_Click(object sender, EventArgs e)
        {
            //Pone en las variables la información que introduce el usuario.
            this.usuario = usuario1_tb.Text;
            this.contra = contra1_tb.Text;
            this.mail = mail_tb.Text;
            this.Close();
        }
    }
}
