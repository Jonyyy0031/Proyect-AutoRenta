using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_AutoRenta.Vistas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        CRUDUsuario iniciar = new CRUDUsuario();
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) 
            {
                DragMove();
            }
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            Usuario usuario = ObtenerUsuario(username, password);

            if (usuario != null)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Por favor, ingresa un nombre de usuario y contraseña válidos.");
                    return;
                }
                string rol = usuario.Roles.Nombre;
                switch (rol)
                {
                    case "SuperAdmin":
                        MessageBox.Show("Acceso correcto", "Inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                        iniciar.Show();
                        break;
                    case "Usuario":
                        MessageBox.Show("Acceso correcto", "Inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                MessageBox.Show("Credenciales invalidas. Por favor intentelo nuevamente", "Inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        UsuarioServices usuarioservices = new UsuarioServices();
        private Usuario ObtenerUsuario(string username, string password)
        {

            List<Usuario> usuarios = usuarioservices.GetUsuarios();
            List<Rol> roles = usuarioservices.GetRoles();

            Usuario usuario = usuarios.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (usuario != null)
            {
                Rol rolUsuario = roles.FirstOrDefault(r => r.PkRol == usuario.FkRol);
                usuario.Roles = rolUsuario;
            }

            return usuario;
        }
    }
}
