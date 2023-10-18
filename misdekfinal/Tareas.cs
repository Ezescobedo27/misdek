using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace misdekfinal
{
    public partial class Tareas : Form
    {
        public Tareas()
        {
            InitializeComponent();
        }

        private void linkInicio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Inicio inicio = new Inicio();

            // Mostrar el formulario de destino
            inicio.Show();

            this.Close(); // Cerrar el formulario actual
        }

        private void linkNotas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Notas notas = new Notas();

            // Mostrar el formulario de destino
            notas.Show();

            this.Close(); // Cerrar el formulario actual
        }

        private void linkPerfil_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Perfil perfil = new Perfil();

            // Mostrar el formulario de destino
            perfil.Show();

            this.Close(); // Cerrar el formulario actual
        }
    }
}
