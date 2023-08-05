using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_AutoRenta.Services
{
    public class VehiculosServices
    {
        public void AddVehiculo(Vehiculos request)
        {
            try
            {
                if (request != null)
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        Vehiculos res = new Vehiculos();
                        res.Modelo = request.Modelo;
                        res.Tipo = request.Tipo;
                        res.Tarifa = request.Tarifa;

                        _context.Vehiculo.Add(res);
                        _context.SaveChanges();
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }
        public void DeleteVehiculo(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Vehiculos vehiculo = _context.Vehiculo.Find(id);

                    if (vehiculo != null)
                    {
                        // Elimina el vehiculo del contexto
                        _context.Vehiculo.Remove(vehiculo);
                        _context.SaveChanges();
                        MessageBox.Show("El vehículo se removió del inventario");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún vehículo con el ID especificado.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error" + ex.Message);
            }
        }
        public void UpdateVehiculo(Vehiculos request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {

                    Vehiculos vehiculo = _context.Vehiculo.Find(request.PkVehiculo);
                    if (vehiculo != null)
                    {
                        vehiculo.Modelo = request.Modelo;
                        vehiculo.Tipo = request.Tipo;
                        vehiculo.Tarifa = request.Tarifa;

                        //_context.Update(vehiculo);
                        _context.Entry(vehiculo).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Sucedio un error: " + ex.Message);
            }
        }
        public List<Vehiculos> GetVehiculos()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Vehiculos> vehiculo = _context.Vehiculo.ToList();

                    if (vehiculo.Count > 0) //verificar lista vacia
                    {

                        return vehiculo;

                    }

                    return vehiculo;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public List<Vehiculos> BuscarVehiculosPorModelo(string modelo)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Vehiculos> vehiculos = _context.Vehiculo
                        .Where(v => v.Modelo.ToLower() == modelo.ToLower())
                        .ToList();

                    return vehiculos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error: " + ex.Message);
            }
        }


    }
}
