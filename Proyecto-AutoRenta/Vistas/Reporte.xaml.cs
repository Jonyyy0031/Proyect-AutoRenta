using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Services;
using Proyecto_AutoRenta.Vistas.Views;
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
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {
        ReservaServices services = new ReservaServices();
        PagoServices pagoservices = new PagoServices();

        double Gtotal;

        public Reporte()
        {
            InitializeComponent();
            GetrenTable();
            GTotal();
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
                btngobackadmin.Visibility = Visibility.Visible;
            }
            else
            {
                btngobackadmin.Visibility = Visibility.Collapsed;
            }
        }
        public void GTotal()
        {
            Gtotal = pagoservices.Monto();
            lblTotal.Content = Gtotal;
        }

        public void GetrenTable()
        {
            UserTable.ItemsSource = services.GetReserva();
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

            VistaGestorReserva StartViewGR = new VistaGestorReserva();
            this.Close();
            StartViewGR.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btngobackadmin_Click(object sender, RoutedEventArgs e)
        {
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            this.Close();
            StartViewSA.Show();
        }
    }
}
