using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_AutoRenta.Entities
{
    public class Reserve
    {
        [Key]
        public int PkReserva { get; set; }
        public string  Nombre { get; set; }
        public string  Correo { get; set; }
        public string Telefono { get; set;}
        public int Tiempo { get; set; }
        public double Monto { get; set; }
        public string Pagado { get; set; }

        [ForeignKey("Vehiculos")]
        public int? FkVehiculos { get; set; }
        public Vehiculos Vehiculos { get; set; }
    }
}
