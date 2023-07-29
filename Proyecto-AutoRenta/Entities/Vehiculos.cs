using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_AutoRenta.Entities
{
    public class Vehiculos
    {
        [Key]
        public int PkVehiculo { get; set; }
        public string Modelo { get; set; }
        public string Tipo { get; set; }
        public double Tarifa { get; set; }

    }
}
