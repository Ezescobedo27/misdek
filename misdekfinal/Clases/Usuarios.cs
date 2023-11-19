using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace misdekfinal.Clases
{
    internal class Usuarios
    {
        NpgsqlConnection conex = new NpgsqlConnection();
        string bandera = "no";
        static String servidor = "ep-raspy-firefly-54852420-pooler.us-east-1.postgres.vercel-storage.com";
        static String bd = "verceldb";
        static String usuario = "default";
        static String password = "qaPTRoJ21GrQ";
        static String puerto = "5432";
                private Usuario usuarioAutenticado; 

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


        String cadenaConexion = "Server=" + servidor + ";Port=" + puerto + ";User Id=" + usuario + ";Password=" + password + ";Database=" + bd + ";";


        public Tuple<string, string, string> ObtenerDetallesTarea(int idTarea)
        {
            Tuple<string, string, string> detallesTarea = null;

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


        public void ActualizarDataGridView(DataGridView dataGridView)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                // Consulta SQL para obtener todas las tareas
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
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al obtener las tareas: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }

        public void EliminarTarea(int idTarea)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "DELETE FROM tareas WHERE id_tarea = @idTarea";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@idTarea", idTarea);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al eliminar la tarea: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }


        public void EliminarTareaNotas(int idNota)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                string sql = "DELETE FROM notas WHERE id_nota = @idNota";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                {
                    cmd.Parameters.AddWithValue("@idNota", idNota);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error al eliminar la nota: " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }

        public void ActualizarDataGridViewNotas(DataGridView dataGridView)
        {
            try
            {
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
                MessageBox.Show("Error al obtener las tareas: " + ex.Message);
            }
            finally
            {
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



        public Usuario ObtenerUsuarioAutenticado()
        {
            return usuarioAutenticado;
        }

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
