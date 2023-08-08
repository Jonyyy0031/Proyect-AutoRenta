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
    /// Lógica de interacción para CRUDUsuario.xaml
    /// </summary>
    public partial class CRUDUsuario : Window
    {
        public CRUDUsuario()
        {
            InitializeComponent();
            GetUserTable();
            GetRoles();
        }
        Usuario usuario = new Usuario();
        UsuarioServices services = new UsuarioServices();
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //OTRA FORMA ES IF(txtpkuser.text == "")
            int id;
            if (int.TryParse(txtPkUser.Text, out id))
            {
                usuario.PkUsuario = id;
                usuario.Nombre = txtNombre.Text;
                usuario.UserName = txtUserName.Text;
                usuario.Password = txtPassword.Text;
                usuario.FkRol = int.Parse(SelectRol.SelectedValue.ToString());
                services.UpdateUser(usuario);
                txtPkUser.Clear();
                txtNombre.Clear();
                txtUserName.Clear();
                txtPassword.Clear();
                SelectRol.SelectedIndex = -1;
                MessageBox.Show("Usuario actualizado");
                GetUserTable();
            }
            else
            {


                usuario.Nombre = txtNombre.Text;
                usuario.UserName = txtUserName.Text;
                usuario.Password = txtPassword.Text;
                usuario.FkRol = int.Parse(SelectRol.SelectedValue.ToString());
                services.AddUser(usuario);

                txtNombre.Clear();
                txtUserName.Clear();
                txtPassword.Clear();
                SelectRol.SelectedIndex = -1;

                MessageBox.Show("Se agregó correctamente");
                GetUserTable();
            }

        }
        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (txtPkUser.Visibility == Visibility.Collapsed && ID.Visibility == Visibility.Collapsed)
            {

                txtPkUser.Visibility = Visibility.Visible;
                ID.Visibility = Visibility.Visible;
                Usuario usuario = new Usuario();
                usuario = (sender as FrameworkElement).DataContext as Usuario;

                txtPkUser.Text = usuario.PkUsuario.ToString();
                txtNombre.Text = usuario.Nombre.ToString();
                txtUserName.Text = usuario.UserName.ToString();
                txtPassword.Text = usuario.Password;
            }
            else
            {
                ID.Visibility = Visibility.Collapsed;
                txtPkUser.Visibility = Visibility.Collapsed;
            }



        }
        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario = (sender as FrameworkElement).DataContext as Usuario;
            int ID = int.Parse(usuario.PkUsuario.ToString());
            services.DeleteUser(ID);
            GetUserTable();
        }

        public void GetUserTable()
        {
            UserTable.ItemsSource = services.GetUsuarios();
        }

        public void GetRoles()
        {
            SelectRol.ItemsSource = services.GetRoles();
            SelectRol.DisplayMemberPath = "Nombre";
            SelectRol.SelectedValuePath = "PkRol";
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btngoback_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            StartViewSA.Show();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (SelectRol.SelectedValue != null && int.TryParse(SelectRol.SelectedValue.ToString(), out int rolId))
            {
                UsuarioServices usuarioServices = new UsuarioServices();
                List<Usuario> usuarios = usuarioServices.SearchUsuariosByRol(rolId);

                if (usuarios.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var usuario in usuarios)
                    {
                        sb.AppendLine($"Nombre: {usuario.Nombre}\nUsuario: {usuario.UserName}\nPassword: {usuario.Password}\nRol: {usuario.Roles.Nombre}\n");
                    }

                    MessageBox.Show(sb.ToString(), "Usuarios por Rol");
                }
                else
                {
                    MessageBox.Show("No se encontraron usuarios con el rol seleccionado.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un rol válido.");
            }
        }

    }
}
