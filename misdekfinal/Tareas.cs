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
            DataGridViewButtonColumn editarColumn = new DataGridViewButtonColumn();
            editarColumn.HeaderText = "Editar";
            editarColumn.Text = "Editar";
            editarColumn.UseColumnTextForButtonValue = true;
            dataGridViewTareas.Columns.Add(editarColumn);

            // Manejar el evento CellContentClick para el DataGridView
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
            if (e.ColumnIndex == dataGridViewTareas.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                // Obtener el ID de la tarea desde la fila seleccionada
                int idTarea = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells["id_tarea"].Value);

                // Lógica para abrir el formulario de edición con el ID de la tarea seleccionada
                // Puedes implementar esta lógica para abrir un nuevo formulario de edición con los detalles de la tarea seleccionada
                // y permitir al usuario realizar cambios.
                // Por ejemplo:
                // Crear un nuevo formulario de edición pasándole el ID de la tarea seleccionada
                FormularioEdicionTarea formularioEdicion = new FormularioEdicionTarea(idTarea);
                formularioEdicion.ShowDialog();
            }
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
