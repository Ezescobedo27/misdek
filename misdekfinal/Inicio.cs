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
    }
}
