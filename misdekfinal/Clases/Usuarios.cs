using Npgsql;
using System;
using System.Windows.Forms;

namespace misdekfinal.Clases
{
    internal class Usuarios
    {
        NpgsqlConnection conex = new NpgsqlConnection();
        static String servidor = "localhost";
        static String bd = "misdek";
        static String usuario = "postgres";
        static String password = "eduar2006";
        static String puerto = "5432";

        public class Usuario
        {
            public string Correo { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Escuela { get; set; }
            public string Telefono { get; set; }
        }


        String cadenaConexion = "Server=" + servidor + ";Port=" + puerto + ";User Id=" + usuario + ";Password=" + password + ";Database=" + bd + ";";

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
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Error de Conexión: " + ex.Message);
                return false;
            }
            
        }

        public Usuario ObtenerInformacionUsuario(string correo)
        {
            Usuario usuario = new Usuario();

            // Realiza una consulta a la base de datos para obtener la información del usuario
            string consulta = "SELECT nombre, apellido_paterno, apellido_materno, escuela, telefono FROM usuarios WHERE correo = @correo";

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
                    }
                }
            }

            return usuario;
        }

    }
}
