using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace misdekfinal
{
    public partial class Autenticacion : Form
    {
        private Clases.Usuarios.Usuario usuarioAutenticado;


        public Autenticacion()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Clases.Usuarios usuarios = new Clases.Usuarios();

            // Obtener el correo y la contraseña ingresados por el usuario
            string correo = inputCorreo.Text;
            string contrasena = inputContrasena.Text;

            // Validar las credenciales
            bool inicioSesionExitoso = usuarios.ValidarCredenciales(correo, contrasena);

            // Realizar acciones adicionales en función del resultado
            if (inicioSesionExitoso)
            {
                labelError.Text = "";
                labelExitoso.Text = "Inicio de Sesión Exitoso, Redirigiendo...";

                // Configurar un Timer para redirigir después de 2 segundos
                Timer timer = new Timer();
                timer.Interval = 2000; // 2000 milisegundos = 2 segundos
                timer.Tick += (s, args) =>
                {
                    timer.Stop(); // Detener el Timer
                    timer.Dispose(); // Liberar recursos
                    Clases.Usuarios.Usuario usuario = usuarios.ObtenerInformacionUsuario(correo);
                    usuarioAutenticado = usuarios.ObtenerInformacionUsuario(correo);

                    // Abrir el formulario "Perfil" y pasar los datos del usuario
                    Perfil perfil = new Perfil();
                    perfil.Nombre = usuario.Nombre;
                    perfil.Escuela = usuario.Escuela;
                    perfil.Telefono = usuario.Telefono;
                    perfil.Puesto = usuario.Puesto;
                    perfil.Show();
                    this.Hide(); // Ocultar el formulario de inicio de sesión
                };
                timer.Start(); // Iniciar el Timer
            }
            else
            {
                // Inicio de sesión fallido, muestra un mensaje de error al usuario.
                labelError.Text = "El Correo o la Contraseña son Incorrectos";
                labelExitoso.Text = "";
            }
        }

        private void Autenticacion_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnLogin;

        }

        private void inputCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string correo = "ehernandez71@ucol.mx";

            string mailtoUrl = $"mailto:{correo}";

            Process.Start(new ProcessStartInfo(mailtoUrl));

            e.Link.Visited = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (inputContrasena.UseSystemPasswordChar == true)
            {
                // Mostrar la contraseña
                inputContrasena.UseSystemPasswordChar = false;   
             }
            else
            {
                // Ocultar la contraseña
                inputContrasena.UseSystemPasswordChar = true;

            }
        }

        private void inputContrasena_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
