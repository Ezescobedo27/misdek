using misdekfinal.Clases;
using System;
using System.Windows.Forms;

namespace misdekfinal
{
    public partial class Tareas : Form
    {
        private Usuarios usuarios = new Usuarios();

        public Tareas()
        {
            InitializeComponent();
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            // Configurar las columnas del DataGridView
            dataGridViewTareas.AutoGenerateColumns = true;
            dataGridViewTareas.ReadOnly = true;
            dataGridViewTareas.AllowUserToAddRows = false;
        }

        
        private void Tareas_Load(object sender, EventArgs e)
        {
            ActualizarDataGridView();

            LlenarComboBoxUsuarios(); // Llenar el ComboBox al cargar el formulario

        }

        private void textBoxAutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewTareas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




        private void buttonCrear_Click(object sender, EventArgs e)
        {
            string nombreAutor = comboBox1.SelectedItem.ToString();
            string nombreTarea = textBoxNombre.Text;
            string descripcionTarea = richTextBoxDescripcion.Text;


            if (string.IsNullOrEmpty(nombreAutor) || string.IsNullOrEmpty(nombreTarea) || string.IsNullOrEmpty(descripcionTarea))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete la información.");
                return; 
            }
            usuarios.CrearTarea(nombreAutor, nombreTarea, descripcionTarea);

            textBoxNombre.Text = "";
            richTextBoxDescripcion.Text = "";
            comboBox1.SelectedItem = "";

            ActualizarDataGridView();


        }


        private void ActualizarDataGridView()
        {
            usuarios.ActualizarDataGridView(dataGridViewTareas);
        }

        private void linkInicio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Inicio inicio = new Inicio();

            // Mostrar el formulario de destino
            inicio.Show();

            this.Close(); // Cerrar el formulario actual

        }
        private void LlenarComboBoxUsuarios()
        {
            // Obtener la lista de nombres de usuarios desde la base de datos
            var nombresUsuarios = usuarios.ObtenerNombresUsuarios();

            // Asignar la lista al ComboBox
            comboBox1.DataSource = nombresUsuarios;
        }

        private void linkNotas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Notas notas = new Notas();

            // Mostrar el formulario de destino
            notas.Show();

            this.Close(); // Cerrar el formulario actual
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Manejar la selección cambiada
        }
    }
}
