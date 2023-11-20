using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace misdekfinal
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void linkNotas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Notas notas = new Notas();

            // Mostrar el formulario de destino
            notas.Show();

            this.Close(); // Cerrar el formulario actual
        }

        private void linkTareas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tareas tareas = new Tareas();

            // Mostrar el formulario de destino
            tareas.Show();

            this.Close(); // Cerrar el formulario actual
        }

        private void linkPerfil_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Perfil perfil = new Perfil();

            // Mostrar el formulario de destino
            perfil.Show();

            this.Close(); // Cerrar el formulario actual
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }
        // Depediendo de la red social a la que le de click, le aparecera como aparecemos
        private void iconButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Facebook: @misdek");
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Youtube: @misdek");

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Github: @misdek");
        }
    }
}
