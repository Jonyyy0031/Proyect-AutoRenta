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
    /// Lógica de interacción para Reserva.xaml
    /// </summary>
    public partial class Reserva : Window
    {
        public Reserva()
        {
            InitializeComponent();
            GetrenTable();
            GetVehiculos();
        }

        Reserve reserve = new Reserve();
        ReservaServices services = new ReservaServices();
        
        private void btnReserva_Click(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve();
            int ID;
            if (int.TryParse(txtPkReserva_.Text, out ID))
            {
                reserve.PkReserva = ID;
                reserve.Nombre = txtNombre.Text;
                reserve.Correo = txtCorreo.Text;
                reserve.Telefono = txtTelefono.Text;
                reserve.Tiempo = int.Parse(txtTDias.Text);
                reserve.FkVehiculos = int.Parse(SelectVehiculo.SelectedValue.ToString());
                services.Updatereser(reserve);

                txtPkReserva_.Clear();
                txtNombre.Clear();
                txtCorreo.Clear();
                txtTelefono.Clear();
                txtTDias.Clear();
                SelectVehiculo.SelectedValue = null;

                MessageBox.Show("Usuario actualizado");
                GetrenTable();
                GetVehiculos();
            }
            else
            {
                Pago pago = new Pago();
                reserve.Nombre = txtNombre.Text;
                reserve.Correo = txtCorreo.Text;
                reserve.Telefono = txtTelefono.Text;  
                reserve.Tiempo = int.Parse(txtTDias.Text);
                reserve.FkVehiculos= int.Parse(SelectVehiculo.SelectedValue.ToString());
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
                    txtTDias.Clear();
                    SelectVehiculo.SelectedValue = null;
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
            GetrenTable();
            GetVehiculos();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve();
            reserve = (sender as FrameworkElement).DataContext as Reserve;

            List<Vehiculos> vehiculos = services.GetVehiculo();
            Vehiculos vehiculo = vehiculos.FirstOrDefault(u => u.PkVehiculo == reserve.FkVehiculos);

            txtPkReserva_.Text = reserve.PkReserva.ToString();
            txtNombre.Text = reserve.Nombre.ToString();
            txtCorreo.Text = reserve.Correo.ToString();
            txtTelefono.Text = reserve.Telefono.ToString();
            txtTDias.Text = Convert.ToString(reserve.Tiempo);
            SelectVehiculo.SelectedValue = reserve.FkVehiculos;
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState= WindowState.Minimized;
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

        private void btnLimpia_Click(object sender, RoutedEventArgs e)
        {
            txtPkReserva_.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTDias.Clear();
            txtTelefono.Clear();
        }
    }
}
