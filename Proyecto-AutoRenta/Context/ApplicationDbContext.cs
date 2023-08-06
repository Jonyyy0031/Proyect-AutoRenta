using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_AutoRenta.Context
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL("Server=localhost; database=Proyectoautorenta23bm; user=root; password=");
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Vehiculos> Vehiculo { get; set;}
        public DbSet<Reserve> Reservas { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData
                (
                    new Rol
                    {
                        PkRol = 1,
                        Nombre = "SuperAdmin"
                    }
                );
            modelBuilder.Entity<Rol>().HasData
                (
                    new Rol
                    {
                        PkRol = 2,
                        Nombre = "GestorReserva"
                    }
                );
            modelBuilder.Entity<Rol>().HasData
                (
                    new Rol
                    {
                        PkRol = 3,
                        Nombre = "GestorInventario"
                    }
                );
            modelBuilder.Entity<Usuario>().HasData
            (
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Jony",
                    UserName = "admin",
                    Password = "admin",
                    FkRol = 1,
                }

            );
            modelBuilder.Entity<Usuario>().HasData
            (
                new Usuario
                {
                    PkUsuario = 2,
                    Nombre = "Jero",
                    UserName = "123",
                    Password = "123",
                    FkRol = 2,
                }

            );
            modelBuilder.Entity<Usuario>().HasData
           (
               new Usuario
               {
                   PkUsuario = 3,
                   Nombre = "Jhordi",
                   UserName = "1234",
                   Password = "1234",
                   FkRol = 3,
               }

           );
            modelBuilder.Entity<Vehiculos>().HasData
            (
                new Vehiculos
                {
                    PkVehiculo = 1,
                    Modelo = "Nissan",
                    Tipo = "Deportivo",
                    Tarifa = 123,
                }
           );
            modelBuilder.Entity<Vehiculos>().HasData
           (
               new Vehiculos
               {
                   PkVehiculo = 2,
                   Modelo = "Chevrolet",
                   Tipo = "Super",
                   Tarifa = 300,
               }
          );
        }
        
     }
}