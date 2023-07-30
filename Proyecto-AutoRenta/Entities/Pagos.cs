using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_AutoRenta.Entities
{
    public class Pagos
    {
        [Key]
        public int PkPago { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
