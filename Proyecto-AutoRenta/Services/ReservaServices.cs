using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Proyecto_AutoRenta.Services
{
    public class ReservaServices
    {
        public void Addreser(Reserve request)
        {
            try
            {
                if (request != null)
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        Reserve res = new Reserve();
                        res.PkReserva = request.PkReserva;
                        res.Nombre = request.Nombre;
                        res.Correo = request.Correo;
                        res.Telefono = request.Telefono;
                        res.Tiempo = request.Tiempo;
                        res.Monto = request.Monto;
                        res.Pagado = request.Pagado;
                        res.FkVehiculos = request.FkVehiculos;

                        _context.Reservas.Add(res);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }


        public List<Reserve> GetReserva()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Reserve> reserve = _context.Reservas.Include(x => x.Vehiculos).ToList();

                    if (reserve.Count > 0)
                    {
                        return reserve;
                    }

                    return reserve;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public void Updatereser(Reserve request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Reserve reserva = new Reserve();
                    reserva = _context.Reservas.Find(request.PkReserva);
                    reserva.Nombre = request.Nombre;
                    reserva.Correo = request.Correo;
                    reserva.Telefono = request.Telefono;
                    reserva.Tiempo = request.Tiempo;
                    reserva.Vehiculos = request.Vehiculos;
                   
                    //_context.Update(usuario);
                    _context.Entry(reserva).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public List<Vehiculos> GetVehiculo()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Vehiculos> vehiculos = _context.Vehiculo.ToList();
                    return vehiculos;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public void Deletereser(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Reserve reserva = _context.Reservas.Find(id);

                    if (reserva != null)
                    {
                        _context.Reservas.Remove(reserva);
                        _context.SaveChanges();
                        MessageBox.Show("El usuario ha sido eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún usuario con el ID especificado.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error" + ex.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //DragMove();
                
            }
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            //WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }





}
