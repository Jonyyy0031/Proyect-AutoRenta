using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_AutoRenta.Services
{
    public class PagoServices
    {
        public double Monto()
        {
            using (var _contex = new ApplicationDbContext())
            {
                double sumaMonto;
                sumaMonto = _contex.Reservas.Sum(v => v.Total);
                return sumaMonto;
            }
        }

        public double UpdatePrice(Vehiculos vehiculo, DateTime fechaS, DateTime FechaV)
        {
            TimeSpan diferencia = FechaV - fechaS;
            int dias = diferencia.Days;
            double total = vehiculo.Tarifa * dias;

            return total;
        }
    }
}
