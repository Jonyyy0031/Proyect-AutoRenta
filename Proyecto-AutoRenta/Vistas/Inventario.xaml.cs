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
    /// Lógica de interacción para Inventario.xaml
    /// </summary>
    public partial class Inventario : Window
    {
        public Inventario()
        {
            InitializeComponent();
            GetUserTable();
            if (App.UsuarioAutenticado != null)
            {
                Usuario usuarioAutenticado = App.UsuarioAutenticado;
                MostrarBotonSegunRol(usuarioAutenticado);
            }
        }
        private void MostrarBotonSegunRol(Usuario usuario)
        {
            // Verificar si el usuario es "SuperAdmin" y mostrar u ocultar el botón según el rol.
            if (usuario.Roles != null && usuario.Roles.Nombre == "SuperAdmin")
            {
                btnFlechaIzquierdaadmin.Visibility = Visibility.Visible;
            }
            else
            {
                btnFlechaIzquierdaadmin.Visibility = Visibility.Collapsed;
            }
        }
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
        public void BtnFlechaIzquierda_Click(object sender, RoutedEventArgs e)
        {
            Login iniciar = new Login();
            iniciar.Show();
            this.Close();
        }

        VehiculosServices services = new VehiculosServices();
        Vehiculos vehiculo = new Vehiculos();
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtModelo.Text) || string.IsNullOrEmpty(SelectTipo.Text) || string.IsNullOrEmpty(txtTarifa.Text))
            {
                MessageBox.Show("POR FAVOR, COMPLETE TODOS LOS CAMPOS");
            }
            else if (txtPkVehiculo.Text == "")
            {
                vehiculo.Modelo = txtModelo.Text;
                vehiculo.Tipo = SelectTipo.Text;

                //Validar la etrada del campo tarifa
                if (double.TryParse(txtTarifa.Text, out double tarifa))
                {
                    vehiculo.Tarifa = tarifa;
                    services.AddVehiculo(vehiculo);
                    //vehiculo.Tarifa = double.Parse(txtTarifa.Text);

                    txtModelo.Clear();
                    SelectTipo.SelectedIndex = -1;
                    txtTarifa.Clear();

                    MessageBox.Show("SE AGREGÓ EL VEHÍCULO CORRECTAMENTE");
                    GetUserTable();
                }
                else
                {
                    MessageBox.Show("INGRESE UNA TARIFA VALIDA");
                }
            }
            else
            {
                if (int.TryParse(txtPkVehiculo.Text, out int ID))
                {
                    //int id = int.Parse(txtPkVehiculo.Text);
                    vehiculo.PkVehiculo = ID;
                    vehiculo.Modelo = txtModelo.Text;
                    vehiculo.Tipo = SelectTipo.Text;
                    //vehiculo.Tarifa = double.Parse(txtTarifa.Text);

                    //Validar la entrada al capo tarifa
                    if (double.TryParse(txtTarifa.Text, out double tarifa))
                    {
                        vehiculo.Tarifa = tarifa;

                        services.UpdateUser(vehiculo);

                        MessageBox.Show("¡Datos del vehículos modificados correctamente!");
                        txtPkVehiculo.Clear();
                        txtModelo.Clear();
                        SelectTipo.SelectedIndex = -1;
                        txtTarifa.Clear();

                        GetUserTable();
                    }
                    else
                    {
                        MessageBox.Show("NGRESE UNA TARIFA VALIDA");
                    }
                }
                else
                {
                    MessageBox.Show("INGRESE UNA ID VALIDA");
                }
            }
        }
        private void EditItem(object sender, RoutedEventArgs e)
        {

            vehiculo = (sender as FrameworkElement).DataContext as Vehiculos;

            txtPkVehiculo.Text = vehiculo.PkVehiculo.ToString();
            txtModelo.Text = vehiculo.Modelo.ToString();
            SelectTipo.Text = vehiculo.Tipo.ToString();
            txtTarifa.Text = vehiculo.Tarifa.ToString();
        }
        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            vehiculo = (sender as FrameworkElement).DataContext as Vehiculos;
            int ID = int.Parse(vehiculo.PkVehiculo.ToString());
            services.DeleteUser(ID);
            GetUserTable();
        }
        public void GetUserTable()
        {
            UserTable.ItemsSource = services.GetUsuarios();
        }

        private void btnFlechaIzquierdaadmin_Click(object sender, RoutedEventArgs e)
        {
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            this.Close();
            StartViewSA.Show();
        }
    }
}
