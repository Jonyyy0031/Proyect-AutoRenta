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
    public class RolesServices
    {
        public void AddRol(Rol request)
        {
            try
            {
                if (request != null)
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        Rol res = new Rol();
                        res.Nombre = request.Nombre;
                        _context.Roles.Add(res);
                        _context.SaveChanges();
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public void DeleteRol(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Rol rol = _context.Roles.Find(id);

                    if (rol != null)
                    {
                        _context.Entry(rol).State = EntityState.Deleted;
                        _context.SaveChanges();
                        MessageBox.Show("El rol ha sido eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún rol con el ID especificado.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error" + ex.Message);
            }
        }

        public List<Rol> GetRoles()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Rol> roles = _context.Roles.ToList();

                    if (roles.Count > 0)
                    {

                        return roles;

                    }

                    return roles;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public void UpdateRol(Rol request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Rol rol = new Rol();
                    rol = _context.Roles.Find(request.PkRol);
                    rol.Nombre = request.Nombre;
                    //_context.Update(usuario);
                    _context.Entry(rol).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        /*public List<Rol> GetRoles()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Rol> roles = _context.Roles.ToList();
                    return roles;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }

        }
        */
    }

}
