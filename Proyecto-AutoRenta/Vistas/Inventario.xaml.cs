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

        VehiculosServices services = new VehiculosServices();
        Vehiculos vehiculo = new Vehiculos();
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            

            if (txtPkVehiculo.Text == "")
            {
                vehiculo.Modelo = txtModelo.Text;
                vehiculo.Tipo = txtTipo.Text;
                vehiculo.Tarifa = double.Parse(txtTarifa.Text);

                services.AddVehiculo(vehiculo);

                txtModelo.Clear();
                txtTipo.Clear();
                txtTarifa.Clear();

                MessageBox.Show("SE AGREGÓ EL VEHÍCULO CORRECTAMENTE");
                //GetUserTable();
            }

            else if (txtPkVehiculo.Text != "")
            {
                int id = int.Parse(txtPkVehiculo.Text);
                vehiculo.PkVehiculo = id;
                vehiculo.Modelo = txtModelo.Text;
                vehiculo.Tipo = txtTipo.Text;
                vehiculo.Tarifa = double.Parse(txtTarifa.Text);

                services.UpdateUser(vehiculo);

                MessageBox.Show("¡Datos del vehículos modificados correctamente!");
                txtPkVehiculo.Clear();
                txtModelo.Clear();
                txtTipo.Clear();
                txtTarifa.Clear();

                //GetUserTable();
            }

        }
    }
}
