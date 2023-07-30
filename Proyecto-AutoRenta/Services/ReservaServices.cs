using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                        res.Nombre = request.Nombre;
                        res.Correo = request.Correo;
                        res.Telefono = request.Telefono;
                        res.FkVehiculos = request.FkVehiculos;
                        res.FkUsuario = request.FkUsuario;
                        res.FechaSalida = request.FechaSalida;
                        res.FechaRegreso = request.FechaRegreso;
                        res.FkPago = request.FkPago;
                        _context.Reservas.Add(res);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error " + ex.Message, ex);
            }
        }

        public List<Reserve> GetReserva()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    
                    List<Reserve> reserve = _context.Reservas
                        .Include(x => x.Vehiculos)
                        .Include(x => x.Pago)
                        .Include(x => x.Usuario)
                        .ThenInclude(usuario => usuario.Roles)
                        .ToList();

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
                    Reserve reserva = _context.Reservas.Find(request.PkReserva);
                    reserva.Nombre = request.Nombre;
                    reserva.Correo = request.Correo;
                    reserva.Telefono = request.Telefono;
                    reserva.FkPago = request.FkPago;
                    reserva.FkVehiculos = request.FkVehiculos;
                    reserva.FkUsuario = request.FkUsuario;
                    reserva.FechaSalida = request.FechaSalida;
                    reserva.FechaRegreso = request.FechaRegreso;

                    _context.Update(reserva);

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
                        MessageBox.Show("la reserva ha sido eliminada correctamente.");
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

        public List<Usuario> GetUsuario()
        {
                try
                {

                    using (var _context = new ApplicationDbContext())
                    {
                        List<Usuario> usuarios = _context.Usuarios.ToList();
                        return usuarios;
                    }
                }
   
                catch (Exception ex)
                {
                    throw new Exception("Ocurrio un error " + ex.Message);
                }
        }

    }
}