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
            options.UseMySQL("Server= localhost; database= ProyectoAutoRenta23BM; user=root");
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Vehiculos> Vehiculo { get; set;}
        public DbSet<Reserve> Reservas { get; set; }
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
            modelBuilder.Entity<Vehiculos>().HasData
            (
                new Vehiculos
                {
                    PkVehiculo = 1,
                    Modelo = "2023223",
                    Tipo = "Deportivo",
                    Tarifa = 123,
                }
           );
        }
        
     }
}