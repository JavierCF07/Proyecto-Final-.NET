using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class Factura
    {
        public int numeroFactura { get; set; }
        public string nit { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public virtual Clientes Clientes { get; set; }
        public virtual DetalleFactura DetalleFactura { get; set; }
    }
}
