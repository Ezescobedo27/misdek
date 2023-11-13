﻿using Npgsql;
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
                private Usuario usuarioAutenticado;  // Variable para almacenar el usuario autenticado

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

        // Función para determinar si la conexión es válida o no

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


        private bool conexionValida()
        {
            try
            {
                // Intenta abrir una conexión a la base de datos aquí
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                // Si la conexión se abre exitosamente, se considera válida
                return true;
            }
            catch (NpgsqlException ex)
            {
                // En caso de un error de conexión, se considera no válida
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

        public bool ValidarCredenciales(string correo, string contrasena)
        {
            try
            {
                

                    conex.ConnectionString = cadenaConexion;
                    conex.Open();

                    // Consulta SQL para verificar las credenciales
                    string sql = "SELECT COUNT(*) FROM usuarios WHERE correo = @correo AND contrasena = @contrasena";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conex))
                    {
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            // Las credenciales son válidas
                            return true;
                        }
                        else
                        {
                            // Las credenciales son inválidas
                            return false;
                        }
                }
                

            }
            catch (NpgsqlException eSx)
            {
                bandera = "1";
                MessageBox.Show("NO ESTAS CONECTADO A LA BASE DE DATOS");
                return false;
            }

        }

        public Usuario ObtenerUsuarioAutenticado()
        {
            return usuarioAutenticado;
        }

        public Usuario ObtenerInformacionUsuario(string correo)
        {

            Usuario usuario = new Usuario();

            // Realiza una consulta a la base de datos para obtener la información del usuario
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
