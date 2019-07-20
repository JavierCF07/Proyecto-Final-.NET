using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class DetalleFactura
    {
        public int codigoDetalle { get; set; }
        public int numeroFactura { get; set; }
        public int codigoProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
        public virtual Factura Factura { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
