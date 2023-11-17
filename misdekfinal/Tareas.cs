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
            if (e.RowIndex >= 0 && dataGridViewTareas.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridViewTareas.Columns[e.ColumnIndex].HeaderText.Equals("Eliminar"))
            {
                int idTarea = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells["id_tarea"].Value);

                // Lógica para eliminar la tarea con el ID obtenido
                usuarios.EliminarTarea(idTarea); // Reemplaza esto con tu lógica de eliminación

                MessageBox.Show("Tarea eliminada correctamente");

                ActualizarDataGridView();
            }
            if (e.RowIndex >= 0 && dataGridViewTareas.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridViewTareas.Columns[e.ColumnIndex].HeaderText.Equals("Editar"))
            {
                int idTarea = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells["id_tarea"].Value);
                CargarDetallesTareaParaEdicion(idTarea);

                MessageBox.Show("Editar Tareas con id " + idTarea);  
            }
        }
        static String servidor = "ep-raspy-firefly-54852420-pooler.us-east-1.postgres.vercel-storage.com";
        static String bd = "verceldb";
        static String usuario = "default";
        static String password = "qaPTRoJ21GrQ";
        static String puerto = "5432";
        private void CargarDetallesTareaParaEdicion(int idTarea)
        {

            try
            {
                String cadenaConexion = "Server=" + servidor + ";Port=" + puerto + ";User Id=" + usuario + ";Password=" + password + ";Database=" + bd + ";";
                conex.Open();

                string sql = "SELECT nombre, descripcion FROM tareas WHERE id_tarea = @idTarea";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@idTarea", idTarea);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Mostrar los detalles de la tarea seleccionada en los controles del formulario actual
                            textBoxNombre.Text = reader["nombre"].ToString();
                            richTextBoxDescripcion.Text = reader["descripcion"].ToString();

                            // Guardar el ID de la tarea seleccionada para la actualización de la tarea más adelante
                            this.idTareaSeleccionada = idTarea;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al cargar los detalles de la tarea para edición: " + ex.Message);
            }
            finally
            {
                conex.Close();
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
