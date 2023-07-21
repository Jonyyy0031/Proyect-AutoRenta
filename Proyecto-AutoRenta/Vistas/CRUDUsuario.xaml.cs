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
                services.UpdateUser(usuario);
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

                MessageBox.Show("SE AGREGÓ CORRECTAMENTE");
                GetUserTable();
            }

        }
        private void EditItem(object sender, RoutedEventArgs e)
        {

            Usuario usuario = new Usuario();
            usuario = (sender as FrameworkElement).DataContext as Usuario;

            txtPkUser.Text = usuario.PkUsuario.ToString();
            txtNombre.Text = usuario.Nombre.ToString();
            txtUserName.Text = usuario.UserName.ToString();
            txtPassword.Text = usuario.Password;

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

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
