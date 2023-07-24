using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using Stripe;
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
    /// Lógica de interacción para Pago.xaml
    /// </summary>
    public partial class Pago : Window
    {
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
                return Text; // Esto es lo que se mostrará en el ComboBox
            }
        }

        public Pago()
        {
            InitializeComponent();
            //comboBoxPaymentMetodo.Items.Add("card");
            comboBoxPaymentMetodo.Items.Add(new ComboBoxItem("Tarjeta Debito", "card"));
            comboBoxPaymentMetodo.Items.Add(new ComboBoxItem("Tarjeta Credito", "card"));
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

        private async void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_51L1nJ6KJ39qpYxSVdNU2LImvvprzU0KjW68MEaRzaZHMFJM8jTrm7hqPQCaI1EgQssdXzdHaDpmEIvKNcJAIpkbt00Uqy4aQ6A");
                
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
                    Amount = Convert.ToInt32(lblAmount.Content)*100,
                    Currency = "mxn",
                    PaymentMethod = paymentMethod.Id,
                    ConfirmationMethod = "manual",
                    Confirm = true
                };

                var paymentIntent = paymentIntentService.Create(createOptions);

                MessageBox.Show("Pago realizado con éxito. \nEstado: " + paymentIntent.Status);

                if(paymentIntent.Status == "succeeded")
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        //agregar base de datos
                    }
                }

                txtCardNumberBox.Clear();
                txtYearBox.Clear();
                txtMonthBox.Clear();
                txtCvcBox.Clear();
            }
            catch (StripeException ex)
            {
                MessageBox.Show("Error de pago: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se genero un error: " + ex.Message);
            }
        }
    }
}
