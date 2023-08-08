using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_AutoRenta.Vistas
{
    /// <summary>
    /// Lógica de interacción para CRUDRoles.xaml
    /// </summary>
    public partial class CRUDRoles : Window
    {
        public CRUDRoles()
        {
            InitializeComponent();
            GetRolTable();
        }
        Rol roles = new Rol();
        RolesServices services = new RolesServices();
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //OTRA FORMA ES IF(txtpkuser.text == "")
            int id;
            if (int.TryParse(txtPkRol.Text, out id))
            {
                roles.PkRol = id;
                roles.Nombre = txtNombre.Text;
                services.UpdateRol(roles);
                txtNombre.Clear();
                txtPkRol.Clear();
                MessageBox.Show("Usuario actualizado");
                GetRolTable();
            }
            else
            {


                roles.Nombre = txtNombre.Text;
                services.AddRol(roles);

                txtNombre.Clear();
                MessageBox.Show("Se agregó correctamente");
                GetRolTable();
            }

        }
        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (txtPkRol.Visibility == Visibility.Collapsed && ID.Visibility == Visibility.Collapsed)
            {

                txtPkRol.Visibility = Visibility.Visible;
                ID.Visibility = Visibility.Visible;
                Rol rol = new Rol();
                rol = (sender as FrameworkElement).DataContext as Rol;

                txtPkRol.Text = rol.PkRol.ToString();
                txtNombre.Text = rol.Nombre.ToString();
            }
            else
            {
                ID.Visibility = Visibility.Collapsed;
                txtPkRol.Visibility = Visibility.Collapsed;
            }



        }
        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            Rol Roles = new Rol();
            Roles = (sender as FrameworkElement).DataContext as Rol;
            int ID = int.Parse(Roles.PkRol.ToString());
            services.DeleteRol(ID);
            GetRolTable();
        }

        public void GetRolTable()
        {
            RolTable.ItemsSource = services.GetRoles();
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
    }
}
