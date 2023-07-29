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
    /// Lógica de interacción para VistaSuperAdmin.xaml
    /// </summary>
    public partial class VistaSuperAdmin : Window
    {
        public VistaSuperAdmin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hola");
        }

        private void BtnGoUsuarios_Click(object sender, RoutedEventArgs e)
        {
            CRUDUsuario usuario = new CRUDUsuario();
            usuario.Show();
            Close();
        }

        private void BtnGoRoles_Click(object sender, RoutedEventArgs e)
        {
            CRUDRoles roles = new CRUDRoles();
            roles.Show();
            Close();
        }

        private void BtnGoReservas_Click(object sender, RoutedEventArgs e)
        {
            Reserva reserva = new Reserva();
            reserva.Show();
            Close();
        }

        private void BtnGoInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario inventario = new Inventario();
            inventario.Show();
            Close();
        }

        private void BtnGoPagos_Click(object sender, RoutedEventArgs e)
        {
            PagosReporte pagosReporte = new PagosReporte();
            pagosReporte.Show();
            Close();
        }
    }
}
