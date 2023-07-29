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
        private void BtnGoUsuarios_Click(object sender, RoutedEventArgs e)
        {
            CRUDUsuario StartUSERS = new CRUDUsuario();
            this.Close();
            StartUSERS.Show();

        }

        private void BtnGoRoles_Click(object sender, RoutedEventArgs e)
        {
            CRUDRoles StartCRUDROLES = new CRUDRoles();
            this.Close();
            StartCRUDROLES.Show();
        }

        private void BtnGoReservas_Click(object sender, RoutedEventArgs e)
        {
            Reserva StartRESERVAS = new Reserva();
            this.Close();
            StartRESERVAS.Show();
        }

        private void BtnGoInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario StartINVENTARIO = new Inventario();
            this.Close();
            StartINVENTARIO.Show();
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
