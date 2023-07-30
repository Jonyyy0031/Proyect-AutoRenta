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
                sumaMonto = _contex.Pagos.Sum(v => v.Total);
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

        public void AddPay(Pagos request)
        {
            try
            {
                if (request != null)
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        Pagos res = new Pagos();

                        res.PkPago = request.PkPago;
                        res.Total = request.Total;
                        res.Fecha = request.Fecha;
                        _context.Pagos.Add(res);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error " + ex.Message, ex);
            }
        }

        public void UpdatePay(Pagos request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Pagos pagos = _context.Pagos.Find(request.PkPago);
                    pagos.Total = request.Total;
                    pagos.Fecha = request.Fecha;

                    _context.Update(pagos);

                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public List<Pagos> GetPagos()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {

                    List<Pagos> pagos = _context.Pagos.ToList();

                    return pagos;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }
    }
}
