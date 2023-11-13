using misdekfinal.Clases;
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
    public partial class Notas : Form
    {
        private Usuarios usuarios = new Usuarios();

        public Notas()
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

        private void Notas_Load(object sender, EventArgs e)

        {
            ActualizarDataGridView();
            LlenarComboBoxUsuarios();
            LlenarComboBoxSeccionesLocal();

        }
        private void LlenarComboBoxUsuarios()
        {
            // Obtener la lista de nombres de usuarios desde la base de datos
            var nombresUsuarios = usuarios.ObtenerNombresUsuarios();

            // Asignar la lista al ComboBox
            comboBoxAutor.DataSource = nombresUsuarios;
        }

     
        private void LlenarComboBoxSeccionesLocal()
        {
            List<string> nombresSecciones = new List<string>
    {
        "Personal",
        "Privado",
        "Grupal",
    };

            comboBoxSeccion.DataSource = nombresSecciones;
        }
        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxAutor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonCrearNota_Click(object sender, EventArgs e)
        {
            string nombreAutor = comboBoxAutor.SelectedItem.ToString();
            string nombreTarea = textBoxNombre.Text;
            string descripcionTarea = richTextBox1.Text;
            string nombreSeccion = comboBoxSeccion.SelectedItem.ToString();

            if (string.IsNullOrEmpty(nombreAutor) || string.IsNullOrEmpty(nombreTarea) || string.IsNullOrEmpty(descripcionTarea))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete la información.");
                return;
            }
            usuarios.CrearNota(nombreAutor, nombreTarea, descripcionTarea, nombreSeccion);

            textBoxNombre.Text = "";
            richTextBox1.Text = "";
            comboBoxAutor.SelectedItem = "";

            ActualizarDataGridView();

        }

        private void ActualizarDataGridView()
        {
            usuarios.ActualizarDataGridViewNotas(dataGridViewNotas);

        }

        private void dataGridViewNotas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
