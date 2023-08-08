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
    public class UsuarioServices
    {
        public void AddUser(Usuario request)
        {
            try
            {
                if (request != null)
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        Usuario res = new Usuario();
                        res.Nombre = request.Nombre;
                        res.UserName = request.UserName;
                        res.Password = request.Password;
                        res.FkRol = request.FkRol;

                        _context.Usuarios.Add(res);
                        _context.SaveChanges();
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Usuario usuario = _context.Usuarios.Find(id);

                    if (usuario != null)
                    {
                        _context.Entry(usuario).State = EntityState.Deleted;
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

        public List<Usuario> GetUsuarios()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Usuario> usuarios = _context.Usuarios.Include(x => x.Roles).ToList();

                    if (usuarios.Count > 0)
                    {

                        return usuarios;

                    }

                    return usuarios;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public void UpdateUser(Usuario request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Usuario usuario = new Usuario();
                    usuario = _context.Usuarios.Find(request.PkUsuario);
                    usuario.Nombre = request.Nombre;
                    usuario.UserName = request.UserName;
                    usuario.Password = request.Password;
                    usuario.FkRol = request.FkRol;
                    //_context.Update(usuario);
                    _context.Entry(usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public List<Rol> GetRoles()
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

        public List<Usuario> SearchUsuariosByRol(int rolId)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Usuario> usuarios = _context.Usuarios
                        .Where(u => u.FkRol == rolId)
                        .Include(x => x.Roles)
                        .ToList();

                    return usuarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error " + ex.Message);
            }
        }

    }
}
