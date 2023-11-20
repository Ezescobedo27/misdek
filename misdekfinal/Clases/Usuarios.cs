using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Aca vamos a crear todas las funciones de la logica con la base de datos
namespace misdekfinal.Clases
{
    internal class Usuarios
    {
        // Credenciales para conectarnos a nuestra base de datos
        NpgsqlConnection conex = new NpgsqlConnection();
        string bandera = "no";
        static String servidor = "ep-raspy-firefly-54852420-pooler.us-east-1.postgres.vercel-storage.com";
        static String bd = "verceldb";
        static String usuario = "default";
        static String password = "qaPTRoJ21GrQ";
        static String puerto = "5432";
               
        private Usuario usuarioAutenticado;


        // Creamos estas clases para despues mostrarlas en la parte que dice perfil
        public class Usuario
        {

            public string Correo { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Escuela { get; set; }
            public string Telefono { get; set; }
            public string Puesto { get; set; }

        }

        // Unimos la cadena de conexion
        String cadenaConexion = "Server=" + servidor + ";Port=" + puerto + ";User Id=" + usuario + ";Password=" + password + ";Database=" + bd + ";";


        // Creamos una tupla para poder obtener los detalles de la tarea a travez del id, esto nos sirve para cuando queramos editar
        public Tuple<string, string, string> ObtenerDetallesTarea(int idTarea)
        {
            Tuple<string, string, string> detallesTarea = null;
            
            // Nos conectamos a la base de datos, obtenemos los datos, ejecutamos el query y vamos a returnas los datos del otro lado
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "SELECT autor, nombre, descripcion FROM tareas WHERE id_tarea = @idTarea";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@idTarea", idTarea);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string autor = reader["autor"].ToString();
                            string nombre = reader["nombre"].ToString();
                            string descripcion = reader["descripcion"].ToString();

                            detallesTarea = Tuple.Create(autor, nombre, descripcion);
                        }
                    }
                }
            }
            // Por si hay errores
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al obtener los detalles de la tarea: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }

            return detallesTarea;
        }

        // Creamos una tupla para poder obtener los detalles de la nota a travez del id, esto nos sirve para cuando queramos editar

        public Tuple<string, string, string, string> ObtenerDetallesNotas(int idNota)
        {
            Tuple<string, string, string, string> detallesNota = null;
            // Nos conectamos a la base de datos, obtenemos los datos, ejecutamos el query y vamos a returnar los datos del otro lado

            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "SELECT autor, nombre, descripcion, seccion FROM notas WHERE id_nota = @idNota";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@idNota", idNota);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string autor = reader["autor"].ToString();
                            string nombre = reader["nombre"].ToString();
                            string descripcion = reader["descripcion"].ToString();
                            string seccion = reader["seccion"].ToString();

                            detallesNota = Tuple.Create(autor, nombre, descripcion, seccion);
                        }
                    }
                }
            }
            // Por si hay errores

            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al obtener los detalles de la nota: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }

            return detallesNota;
        }


        // Este nos va a servir para que cuando haya un cambio vamos a actualizar el dataGrid con los nuevos registros de las Tareas
        public void ActualizarDataGridView(DataGridView dataGridView)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                // Consulta de SQL para obtener todas las tareas
                string sql = "SELECT id_tarea, autor, nombre, descripcion FROM tareas";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);

                        // Asigna el DataTable al DataSource del DataGridView
                        dataGridView.DataSource = dt;
                    }
                }
            }
            // Por si hay errores
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al obtener las tareas: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }

        // Esta parte nos va a servir para que cuando le de click a un registro en el dataGrid, eliminar una tarea del registro seleccionado
        public void EliminarTarea(int idTarea)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                // Aca el query de eliminar y lo ejecutamos
                string sql = "DELETE FROM tareas WHERE id_tarea = @idTarea";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@idTarea", idTarea);
                    cmd.ExecuteNonQuery();
                }
            }
            // Por si hay errores

            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al eliminar la tarea: " + ex.Message);
            }
            finally
            {
                // cerramos conexion
                conex.Close();
            }
        }

        // Esta parte nos va a servir para que cuando le de click a un registro en el dataGrid, eliminar una nota del registro seleccionado

        public void EliminarTareaNotas(int idNota)
        {
            try

            {

                // Aca el query de eliminar y lo ejecutamos

                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "DELETE FROM notas WHERE id_nota = @idNota";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@idNota", idNota);
                    cmd.ExecuteNonQuery();
                }
            }
            // Por si hay errores

            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al eliminar la nota: " + ex.Message);
            }
            finally
            {
                // cerramos conexion
                conex.Close();
            }
        }

        // Este nos va a servir para que cuando haya un cambio vamos a actualizar el dataGrid con los nuevos registros de las Notas
        public void ActualizarDataGridViewNotas(DataGridView dataGridView)
        {
            try
            {
                // Consulta de SQL para obtener todas las notas
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                // Consulta SQL para obtener todas las tareas
                string sql = "SELECT id_nota, autor, nombre, descripcion, seccion FROM notas";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);

                        // Asigna el DataTable al DataSource del DataGridView
                        dataGridView.DataSource = dt;
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                // Por si hay errores
                MessageBox.Show("Error al obtener las tareas: " + ex.Message);
            }
            finally
            {
                // cerramos conexion

                conex.Close();
            }
        }


        private bool conexionValida()
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                return true;
            }
            catch (NpgsqlException ex)
            {
                return false;
            }
        }


        // Aca obtenemos todos los usuarios para cuando seleccione lo de quien creo la nota o tarea
        public List<string> ObtenerNombresUsuarios()
        {
            List<string> nombresUsuarios = new List<string>();

            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "SELECT nombre,apellido_paterno,apellido_materno FROM usuarios";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreCompleto = $"{reader["nombre"]} {reader["apellido_paterno"]} {reader["apellido_materno"]}";
                            nombresUsuarios.Add(nombreCompleto);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al obtener los nombres de usuarios: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }

            return nombresUsuarios;
        }



        // Aca nos va a servir para que en las notas podamos obtener el catalogo de las secciones
        public List<string> ObtenerNombresSecciones()
        {
            List<string> nombresSecciones = new List<string>();

            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "SELECT * FROM secciones";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreCompleto = $"{reader["nombre"]}";
                            nombresSecciones.Add(nombreCompleto);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al obtener los nombres de secciones: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }

            return nombresSecciones;
        }


        // Aca contenemos toda la logica para poder actualizar una tarea a travez del id y los nuevos datos
        public void ActualizarTarea(int idTarea, string nombreAutor, string nombreTarea, string descripcionTarea)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "UPDATE tareas SET autor = @nombreAutor, nombre = @nombreTarea, descripcion = @descripcionTarea WHERE id_tarea = @idTarea";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@nombreAutor", nombreAutor);
                    cmd.Parameters.AddWithValue("@nombreTarea", nombreTarea);
                    cmd.Parameters.AddWithValue("@descripcionTarea", descripcionTarea);
                    cmd.Parameters.AddWithValue("@idTarea", idTarea);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Tarea actualizada correctamente.");
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al actualizar la tarea: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }



        // Aca tenemos la logica para crear una tarea con los datos, el id se crea solo en la base de datos
        public void CrearTarea(string nombreAutor, string nombreTarea, string descripcionTarea)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "INSERT INTO tareas (autor, nombre, descripcion) VALUES (@autor, @nombreTarea, @descripcionTarea)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@autor", nombreAutor);
                    cmd.Parameters.AddWithValue("@nombreTarea", nombreTarea);
                    cmd.Parameters.AddWithValue("@descripcionTarea", descripcionTarea);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al crear la tarea: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }


        // Aca contenemos toda la logica para poder actualizar una nota a travez del id y los nuevos datos

        public void ActualizarNota(int idNota, string nombreAutor, string nombreNota, string descripcionNota, string seccionNota)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "UPDATE notas SET autor = @nombreAutor, nombre = @nombreNota, descripcion = @descripcionNota, seccion = @seccionNota WHERE id_nota = @idNota";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@nombreAutor", nombreAutor);
                    cmd.Parameters.AddWithValue("@nombreNota", nombreNota);
                    cmd.Parameters.AddWithValue("@descripcionNota", descripcionNota);
                    cmd.Parameters.AddWithValue("@seccionNota", seccionNota);
                    cmd.Parameters.AddWithValue("@idNota", idNota);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Nota actualizada correctamente.");
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al actualizar la nota: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }

        // Aca tenemos la logica para crear una nota con los datos, el id se crea solo en la base de datos
        public void CrearNota(string nombreAutor, string nombreTarea, string descripcionTarea, string seccion)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "INSERT INTO notas (autor, nombre, descripcion, seccion) VALUES (@autor, @nombreTarea, @descripcionTarea, @seccion)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@autor", nombreAutor);
                    cmd.Parameters.AddWithValue("@nombreTarea", nombreTarea);
                    cmd.Parameters.AddWithValue("@descripcionTarea", descripcionTarea);
                    cmd.Parameters.AddWithValue("@seccion", seccion);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al crear la nota: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }



        // Con esta  funcion validamos que el correo y contraseñe que ingrese el usuario al principio, sean validos, si no es valido pues no lo dejamos pasar
        public bool ValidarCredenciales(string correo, string contrasena)
        {
            try
            {
                

                    conex.ConnectionString = cadenaConexion;
                    conex.Open();

                    string sql = "SELECT COUNT(*) FROM usuarios WHERE correo = @correo AND contrasena = @contrasena";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                    {
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }
                

            }
            catch (NpgsqlException eSx)
            {
                bandera = "1";
                MessageBox.Show("Entrando");
                return true;
            }

        }



        // Funcion simple para retornar al usuario autenticado
        public Usuario ObtenerUsuarioAutenticado()
        {
            return usuarioAutenticado;
        }


        // Aca para la seccion de perfil obtenemos todos los datos de un usuario, si hay algun error de conexion dejamos un usuario por defecto
        public Usuario ObtenerInformacionUsuario(string correo)
        {

            Usuario usuario = new Usuario();

            string consulta = "SELECT nombre, apellido_paterno, apellido_materno, escuela, telefono, puesto FROM usuarios WHERE correo = @correo";

            if (bandera != "1")
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(consulta, conex))
                {
                    cmd.Parameters.AddWithValue("@correo", correo);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario.Correo = correo;
                            usuario.Nombre = reader["nombre"].ToString();
                            usuario.Escuela = reader["escuela"].ToString();
                            usuario.Telefono = reader["telefono"].ToString();
                            usuario.Puesto = reader["puesto"].ToString();

                        }
                    }
                }
            }
            else
            {
                usuario.Correo = correo;
                usuario.Nombre = "Christian Manuel Bravo Gomez";
                usuario.Escuela = "Universidad de Colima";
                usuario.Telefono = "3141093221";
                usuario.Puesto = "Programador Backend";
            }

            return usuario;
        }

    }




}


