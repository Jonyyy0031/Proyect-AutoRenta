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
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRegreso { get; set; }

        [ForeignKey("Vehiculos")]
        public int FkVehiculos { get; set; }
        public Vehiculos Vehiculos { get; set; }

        [ForeignKey("Usuario")]
        public int FkUsuario { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Pago")]
        public int FkPago { get; set; }
        public Pagos Pago { get; set; }
    }
}
