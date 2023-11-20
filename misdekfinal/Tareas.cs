using misdekfinal.Clases;
using Npgsql;
using System;
using System.Windows.Forms;

namespace misdekfinal
{
    public partial class Tareas : Form
    {
        private Usuarios usuarios = new Usuarios();
        NpgsqlConnection conex = new NpgsqlConnection(); // Definición de conex como NpgsqlConnection
        private int idTareaSeleccionada; // Declaración de idTareaSeleccionada
        private bool editandoTarea = false;

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

            // Aca agregamos nuevas columnas para eliminar y actualizar, su logica la manejaremos mas abajo
            DataGridViewButtonColumn eliminarColumn = new DataGridViewButtonColumn();
            eliminarColumn.HeaderText = "Eliminar";
            eliminarColumn.Text = "Eliminar";
            eliminarColumn.UseColumnTextForButtonValue = true;
            dataGridViewTareas.Columns.Add(eliminarColumn);

            DataGridViewButtonColumn editarColumn = new DataGridViewButtonColumn();
            editarColumn.HeaderText = "Editar";
            editarColumn.Text = "Editar";
            editarColumn.UseColumnTextForButtonValue = true;
            dataGridViewTareas.Columns.Add(editarColumn);
        }

        
       // Cuando cargue la secciion, llenamos los usuarios en el comboBox y ponemos los registros de dataGrid
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
            // Si le da click a la columna de eliminar, obtenemos el id del que le dio click y traem la funcion ElimnarTarea de la clase usuarios
            if (e.RowIndex >= 0 && dataGridViewTareas.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridViewTareas.Columns[e.ColumnIndex].HeaderText.Equals("Eliminar"))
            {
                int idTarea = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells["id_tarea"].Value);


           
                // Lógica para eliminar la tarea con el ID obtenido
                usuarios.EliminarTarea(idTarea); 

                MessageBox.Show("Tarea eliminada correctamente");

                ActualizarDataGridView();
            }
            // Si le da click a la columna de editar, obtenemos el id del que le dio click y traem la funcion eDITAR de la clase usuarios
            if (e.RowIndex >= 0 && dataGridViewTareas.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridViewTareas.Columns[e.ColumnIndex].HeaderText.Equals("Editar"))
            {
                int idTarea = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells["id_tarea"].Value);
                Tuple<string, string, string> detallesTarea = usuarios.ObtenerDetallesTarea(idTarea);
                comboBox1.Text = detallesTarea.Item1;
                textBoxNombre.Text = detallesTarea.Item2;
                richTextBoxDescripcion.Text = detallesTarea.Item3;

                //  CargarDetallesTareaParaEdicion(idTarea);

                idTareaSeleccionada = idTarea;
                editandoTarea = true;
                MessageBox.Show("ID:"+idTarea+" Seleccionado, Modifica los Campos de tú registro");  
            }
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            if (editandoTarea)
            {
                // Obtener los valores de los campos
                string nombreAutor = comboBox1.SelectedItem.ToString();
                string nombreTarea = textBoxNombre.Text;
                string descripcionTarea = richTextBoxDescripcion.Text;

                if (string.IsNullOrEmpty(nombreAutor) || string.IsNullOrEmpty(nombreTarea) || string.IsNullOrEmpty(descripcionTarea))
                {
                    MessageBox.Show("Todos los campos son obligatorios. Por favor, complete la información.");
                    return;
                }

                // Actualizar la tarea con los nuevos valores
                usuarios.ActualizarTarea(idTareaSeleccionada, nombreAutor, nombreTarea, descripcionTarea);
                ActualizarDataGridView();

                // Restaurar el formulario para permitir la creación de una nueva tarea
                textBoxNombre.Text = "";
                richTextBoxDescripcion.Text = "";
                comboBox1.SelectedItem = "";
                editandoTarea = false; // Cambiar el estado de edición
            }
            else // Crear una nueva tarea
            {
                string nombreAutor = comboBox1.SelectedItem.ToString();
                string nombreTarea = textBoxNombre.Text;
                string descripcionTarea = richTextBoxDescripcion.Text;

                if (string.IsNullOrEmpty(nombreAutor) || string.IsNullOrEmpty(nombreTarea) || string.IsNullOrEmpty(descripcionTarea))
                {
                    MessageBox.Show("Todos los campos son obligatorios. Por favor, complete la información.");
                    return;
                }

                // Crear una nueva tarea
                usuarios.CrearTarea(nombreAutor, nombreTarea, descripcionTarea);
                ActualizarDataGridView();

                // Limpiar los campos después de crear una nueva tarea
                textBoxNombre.Text = "";
                richTextBoxDescripcion.Text = "";
                comboBox1.SelectedItem = "";
            }


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

        private void labelTarea_Click(object sender, EventArgs e)
        {

        }
    }
}
