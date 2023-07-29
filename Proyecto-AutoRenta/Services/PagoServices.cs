using Proyecto_AutoRenta.Context;
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
    }
}
