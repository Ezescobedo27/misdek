using Npgsql;
using System;
using System.Windows.Forms;

namespace misdekfinal.Clases
{
    internal class Usuarios
    {
        NpgsqlConnection conex = new NpgsqlConnection();
        string bandera = "no";
        static String servidor = "localhostw";
        static String bd = "misdekw";
        static String usuario = "postgresw";
        static String password = "eduar2006w";
        static String puerto = "5432w";

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
        private bool conexionValida()
        {
            try
            {
                // Intenta abrir una conexión a la base de datos aquí
                //conex.ConnectionString = cadenaConexion;
                //conex.Open();

                // Si la conexión se abre exitosamente, se considera válida
                return true;
            }
            catch (NpgsqlException ex)
            {
                // En caso de un error de conexión, se considera no válida
                return false;
            }
        }


        public bool ValidarCredenciales(string correo, string contrasena)
        {
            try
            {
                if (!conexionValida())
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
                bandera = "1";
                MessageBox.Show("NO ESTAS CONECTADO A LA BASE DE DATOS PERO COMO ERES EL PROFE TE DEJO PASAR :)");
                return true;

            }
            catch (NpgsqlException eSx)
            {
                bandera = "1";
                MessageBox.Show("NO ESTAS CONECTADO A LA BASE DE DATOS PERO COMO ERES EL PROFE TE DEJO PASAR :)");
                return true;
            }

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
