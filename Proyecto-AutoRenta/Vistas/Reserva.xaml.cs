
using Google.Protobuf.WellKnownTypes;
using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para Reserva.xaml
    /// </summary>
    public partial class Reserva : Window
    {
        public Reserva()
        {
            InitializeComponent();
            GetrenTable();
            GetVehiculos();
            GetUser();

        }

        Reserve reserve = new Reserve();
        ReservaServices services = new ReservaServices();

        private void btnReserva_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaSalida = datePickerSalida.SelectedDate.HasValue ? datePickerSalida.SelectedDate.Value : DateTime.MinValue;
            DateTime fechaRegreso = datePickerRegreso.SelectedDate.HasValue ? datePickerRegreso.SelectedDate.Value : DateTime.MinValue;
            int ID;
            if (int.TryParse(txtPkReserva_.Text, out ID))
            {

                Reserve reserve = new Reserve();


                reserve.PkReserva = ID;
                reserve.Nombre = txtNombre.Text;
                reserve.Correo = txtCorreo.Text;
                reserve.Telefono = txtTelefono.Text;
                reserve.FechaSalida = fechaSalida;
                reserve.FechaRegreso = fechaRegreso;

                Vehiculos seleccionado = SelectVehiculo.SelectedItem as Vehiculos;
                reserve.FkVehiculos = seleccionado.PkVehiculo;

                Usuario seleccionadoo = SelectUser.SelectedItem as Usuario;
                reserve.FkUsuario = seleccionadoo.PkUsuario;


                services.Updatereser(reserve);

                txtPkReserva_.Clear();
                txtCorreo.Clear();
                txtTelefono.Clear();
                datePickerSalida.SelectedDate = null;
                datePickerRegreso.SelectedDate = null;

                MessageBox.Show("Reserva Actualizado");
                GetrenTable();
                GetVehiculos();
                GetUser();

            }
            else
            {
                Pago pago = new Pago();
                reserve.Nombre = txtNombre.Text;
                reserve.Correo = txtCorreo.Text;
                reserve.Telefono = txtTelefono.Text;
                reserve.FkVehiculos = int.Parse(SelectVehiculo.SelectedValue.ToString());
                reserve.FkUsuario = int.Parse(SelectUser.SelectedValue.ToString());
                reserve.FechaSalida = fechaSalida;
                reserve.FechaRegreso = fechaRegreso;
                pago.Reserva(reserve);

                bool? dialogResult = pago.ShowDialog();
                if (dialogResult == true)
                {
                    MessageBox.Show("Reserva agregada");
                    GetrenTable();
                    GetVehiculos();

                    txtNombre.Clear();
                    txtCorreo.Clear();
                    txtTelefono.Clear();
                    SelectVehiculo.SelectedValue = null;
                    SelectUser.SelectedValue = null;
                }
                else
                {
                    MessageBox.Show("Fallo al agregar la reserva");
                }

            }
        }


        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve();
            reserve = (sender as FrameworkElement).DataContext as Reserve;
            int ID = int.Parse(reserve.PkReserva.ToString());
            services.Deletereser(ID);

            txtPkReserva_.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            datePickerSalida.SelectedDate = null;
            datePickerRegreso.SelectedDate = null;
            GetrenTable();
            GetVehiculos();
            GetUser();

        }

        private void EditItem(object sender, RoutedEventArgs e)
        {


            Reserve reserve = new Reserve();
            reserve = (sender as FrameworkElement).DataContext as Reserve;

            txtPkReserva_.Text = reserve.PkReserva.ToString();
            txtNombre.Text = reserve.Nombre.ToString();
            txtCorreo.Text = reserve.Correo.ToString();
            txtTelefono.Text = reserve.Telefono.ToString();
            datePickerSalida.SelectedDate = reserve.FechaSalida;
            datePickerRegreso.SelectedDate = reserve.FechaRegreso;


        }

        public void GetrenTable()
        {
            UserTable.ItemsSource = services.GetReserva();
        }


        public void GetVehiculos()
        {
            SelectVehiculo.ItemsSource = services.GetVehiculo();
            SelectVehiculo.DisplayMemberPath = "Modelo";
            SelectVehiculo.SelectedValuePath = "PkVehiculo";
        }

        public void GetUser()
        {
            SelectUser.ItemsSource = services.GetUsuario();
            SelectUser.DisplayMemberPath = "Nombre";
            SelectUser.SelectedValuePath = "PkUsuario";
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
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void UserTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}