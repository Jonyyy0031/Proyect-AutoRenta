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
            this.Close();
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            StartViewSA.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
