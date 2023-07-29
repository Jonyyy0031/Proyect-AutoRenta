using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Lógica de interacción para Pago.xaml
    /// </summary>
    public partial class Pago : Window
    {
        Reserve ress = new Reserve();
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(string text, string value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        public void Reserva(Reserve reserva)
        {
            ReservaServices services = new ReservaServices();
            List<Vehiculos> vehiculos = services.GetVehiculo();
            Vehiculos vehiculo = vehiculos.FirstOrDefault(u => u.PkVehiculo == reserva.FkVehiculos);
            ress.FkVehiculos = reserva.FkVehiculos;
            txtRecibeNombre.Text = reserva.Nombre;
            txtRecibeCorreo.Text = reserva.Correo;
            txtRecibeTelefono.Text = reserva.Telefono;
            txtRecibeVehiculo.Text = vehiculo.Modelo;
            txtRecibeDias.Text = Convert.ToString(reserva.Tiempo);
            lblRecibeAmount.Content = vehiculo.Tarifa*reserva.Tiempo;
            ress.Monto = vehiculo.Tarifa * reserva.Tiempo;
            ress.PkReserva = reserva.PkReserva;
        }

        public Pago()
        {
            InitializeComponent();
            //comboBoxPaymentMetodo.Items.Add("card");
            comboBoxPaymentMetodo.Items.Add(new ComboBoxItem("Tarjeta Debito", "card"));
            comboBoxPaymentMetodo.Items.Add(new ComboBoxItem("Tarjeta Credito", "card"));
            comboBoxPaymentMetodo.Items.Add(new ComboBoxItem("Efectivo", "cash"));
        }
        public string ObtenerValorSeleccionado()
        {
            if (comboBoxPaymentMetodo.SelectedItem is ComboBoxItem selectedItem)
            {
                string valorSeleccionado = selectedItem.Value;
                return valorSeleccionado;
            }
            return null;
        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //StripeConfiguration.SetApiKey("pk_test_51L1nJ6KJ39qpYxSV5ioSMb4uBS7cX61nkyNWlE36EhGaM0Ns72eum376gRNl0hn2Pmywjdi2lFFz97oA6dTqCmOB0019F4eQpa");
                StripeConfiguration.SetApiKey("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");

                var options = new PaymentMethodCreateOptions
                {
                    //Type = comboBoxPaymentMetodo.SelectedValue.ToString(),
                    Type = ObtenerValorSeleccionado(),
                    Card = new PaymentMethodCardOptions
                    {
                        Number = txtCardNumberBox.Text,
                        ExpMonth = Convert.ToInt32(txtMonthBox.Text),
                        ExpYear = Convert.ToInt32(txtYearBox.Text),
                        Cvc = txtCvcBox.Password
                    }
                };

                var service = new PaymentMethodService();
                var paymentMethod = service.Create(options);

                var paymentIntentService = new PaymentIntentService();
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = Convert.ToInt32(lblRecibeAmount.Content) * 100,
                    Currency = "mxn",
                    PaymentMethod = paymentMethod.Id,
                    ConfirmationMethod = "manual",
                    Confirm = true
                };

                var paymentIntent = paymentIntentService.Create(createOptions);

                MessageBox.Show("Pago realizado con éxito. \nEstado: " + paymentIntent.Status + "\nReserva agregada!");

                Reserve reserve = new Reserve();
                ReservaServices rservices = new ReservaServices();

                if (paymentIntent.Status == "succeeded")
                {
                    reserve.PkReserva = ress.PkReserva;
                    reserve.Nombre = txtRecibeNombre.Text;
                    reserve.Correo = txtRecibeCorreo.Text;
                    reserve.Telefono = txtRecibeTelefono.Text;
                    reserve.Tiempo = int.Parse(txtRecibeDias.Text);
                    reserve.FkVehiculos = ress.FkVehiculos;
                    reserve.Monto = ress.Monto;
                    reserve.Pagado = "Pagado";
                    rservices.Addreser(reserve);
                }

                txtCardNumberBox.Clear();
                txtYearBox.Clear();
                txtMonthBox.Clear();
                txtCvcBox.Clear();

                DialogResult = true;

                Close();
            }
            catch (StripeException ex)
            {
                MessageBox.Show("Error de pago: " + ex.Message);
            }
        }

        private void ComboBoxPaymentMetodo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;

            switch (comboBox.SelectedIndex)
            {
                case 0:
                    Tarjetas.Visibility = Visibility.Visible;
                    Efectivo.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    Tarjetas.Visibility = Visibility.Visible;
                    Efectivo.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    Tarjetas.Visibility = Visibility.Collapsed;
                    Efectivo.Visibility = Visibility.Visible;
                    break;
                default:
                    Tarjetas.Visibility = Visibility.Collapsed;
                    Efectivo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Efectivo_Click(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve();
            ReservaServices rservices = new ReservaServices();

            reserve.PkReserva = ress.PkReserva;
            reserve.Nombre = txtRecibeNombre.Text;
            reserve.Correo = txtRecibeCorreo.Text;
            reserve.Telefono = txtRecibeTelefono.Text;
            reserve.Tiempo = int.Parse(txtRecibeDias.Text);
            reserve.FkVehiculos = ress.FkVehiculos;
            reserve.Monto = ress.Monto;
            reserve.Pagado = "Pagado";
            rservices.Addreser(reserve);

            

            txtCardNumberBox.Clear();
            txtYearBox.Clear();
            txtMonthBox.Clear();
            txtCvcBox.Clear();

            DialogResult = true;

            Close();
        }
    }
}
