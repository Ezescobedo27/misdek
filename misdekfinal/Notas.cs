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
        private int idNotaSelccionada; // Declaración de idNotaSelccionada
        private bool editandoTarea = false;
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


        // En cuanto cargue, actualizamos el daraGrid, llenamos las secciones y los usuarios que pueden crear registros
        private void Notas_Load(object sender, EventArgs e)

        {
            ActualizarDataGridView();
            ConfigurarDataGridView();
            LlenarComboBoxUsuarios();
            LlenarComboBoxSeccionesLocal();
            


        }

        private void ConfigurarDataGridView()
        {
            dataGridViewNotas.AutoGenerateColumns = true;
            dataGridViewNotas.ReadOnly = true;
            dataGridViewNotas.AllowUserToAddRows = false;

            // Aca agregamos nuevas columnas para eliminar y actualizar, su logica la manejaremos mas abajo
            DataGridViewButtonColumn eliminarColumn = new DataGridViewButtonColumn();
            eliminarColumn.HeaderText = "Eliminar";
            eliminarColumn.Text = "Eliminar";
            eliminarColumn.UseColumnTextForButtonValue = true;
            dataGridViewNotas.Columns.Insert(0, eliminarColumn); // Insertar la columna 'Eliminar' en el índice 0

            DataGridViewButtonColumn editarColumn = new DataGridViewButtonColumn();
            editarColumn.HeaderText = "Editar";
            editarColumn.Text = "Editar";
            editarColumn.UseColumnTextForButtonValue = true;
            dataGridViewNotas.Columns.Insert(1, editarColumn); // Insertar la columna 'Editar' en el índice 1
        }

        private void LlenarComboBoxUsuarios()
        {
            // Obtener la lista de nombres de usuarios desde la base de datos
            var nombresUsuarios = usuarios.ObtenerNombresUsuarios();

            // Asignar la lista al ComboBox
            comboBoxAutor.DataSource = nombresUsuarios;
        }

     
        // Aca creamos las secciones locales para el comboBox
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
        private void dataGridViewNotas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aca agregamos nuevas columnas para eliminar y actualizar, su logica la manejaremos mas abajo

            if (e.RowIndex >= 0 && dataGridViewNotas.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridViewNotas.Columns[e.ColumnIndex].HeaderText.Equals("Eliminar"))
            {
                int idNota = Convert.ToInt32(dataGridViewNotas.Rows[e.RowIndex].Cells["id_nota"].Value);

                // Lógica para eliminar la tarea con el ID obtenido
                usuarios.EliminarTareaNotas(idNota);

                MessageBox.Show("Tarea eliminada correctamente");

                ActualizarDataGridView();
            }
            // Si elige editar, con una tupla y la clase obtenemos detalles 
            if (e.RowIndex >= 0 && dataGridViewNotas.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridViewNotas.Columns[e.ColumnIndex].HeaderText.Equals("Editar"))
            {
                int idNota = Convert.ToInt32(dataGridViewNotas.Rows[e.RowIndex].Cells["id_nota"].Value);
                Tuple<string, string, string, string> detallesNota = usuarios.ObtenerDetallesNotas(idNota);

                if (detallesNota != null)
                {
                    comboBoxAutor.Text = detallesNota.Item1;
                    textBoxNombre.Text = detallesNota.Item2;
                    richTextBox1.Text = detallesNota.Item3;
                    comboBoxSeccion.Text = detallesNota.Item4;

                    idNotaSelccionada = idNota;
                    editandoTarea = true;

                    MessageBox.Show("ID:" + idNota + " Seleccionado, Modifica los Campos de tu registro");
                }
                // Por si no encuentra
                else
                {
                    MessageBox.Show("No se encontraron detalles para la nota seleccionada.");
                }
            }
        }


        private void buttonCrearNota_Click(object sender, EventArgs e)
        {
            // Si esta editando tarea, usamos la clase de editar con los datos ya rellenados para nomas modificar
            
            if (editandoTarea)
            {
                string nombreAutor = comboBoxAutor.SelectedItem.ToString();
                string nombreNota = textBoxNombre.Text;
                string descripcionNota = richTextBox1.Text;
                string seccionNota = comboBoxSeccion.SelectedItem.ToString();

                if (string.IsNullOrEmpty(nombreAutor) || string.IsNullOrEmpty(nombreNota) || string.IsNullOrEmpty(descripcionNota))
                {
                    MessageBox.Show("Todos los campos son obligatorios. Por favor, complete la información.");
                    return;
                }

                usuarios.ActualizarNota(idNotaSelccionada, nombreAutor, nombreNota, descripcionNota, seccionNota);

                textBoxNombre.Text = "";
                richTextBox1.Text = "";
                comboBoxAutor.SelectedItem = "";

                ActualizarDataGridView();

            } else
            {
                // Si no la esta editando, nomas creamos una tarea nueva
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
           
           

        }


        // Aca estaremos actualizando el dataGrid para ver los nuevos registros
        private void ActualizarDataGridView()
        {
            usuarios.ActualizarDataGridViewNotas(dataGridViewNotas);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
