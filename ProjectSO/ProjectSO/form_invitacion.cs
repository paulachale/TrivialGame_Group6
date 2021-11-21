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
    public partial class form_invitacion : Form
    {
        public int aceptar;
        public string Invitador;
        public form_invitacion()
        {
            InitializeComponent();
        }

        public void form_invitacion_Load(object sender, EventArgs e)
        {
            invitacion_lb.Text = Invitador + " te ha invitado a jugar. ¿Aceptas?";
        }

        private void rechazar_bt_Click(object sender, EventArgs e)
        {
            this.aceptar = 0;
            this.Close();        
        }

        private void Aceptar_bt_Click(object sender, EventArgs e)
        {
            this.aceptar = 1;
            this.Close();
        }
        public int GetAceptar()
        {
            return this.aceptar;
        }
        public void SetInvitador(string nombre)
        {
            this.Invitador = nombre;
        }
    }
}
