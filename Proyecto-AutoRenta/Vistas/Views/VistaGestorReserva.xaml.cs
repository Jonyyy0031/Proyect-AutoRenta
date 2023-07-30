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

namespace Proyecto_AutoRenta.Vistas.Views
{
    /// <summary>
    /// Lógica de interacción para VistaGestorReserva.xaml
    /// </summary>
    public partial class VistaGestorReserva : Window
    {
        public VistaGestorReserva()
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
        private void BtnGoReservas_Click(object sender, RoutedEventArgs e)
        {
            Reserva StartRESERVAS = new Reserva();
            this.Close();
            StartRESERVAS.Show();
        }
        private void BtnGoPagos_Click(object sender, RoutedEventArgs e)
        {
            Reporte StartREPORTE = new Reporte();
            this.Close();
            StartREPORTE.Show();
        }

        private void btngoback_Click(object sender, RoutedEventArgs e)
        {
            Login StartLogin = new Login();
            this.Close();
            StartLogin.Show();
        }
    }
}
