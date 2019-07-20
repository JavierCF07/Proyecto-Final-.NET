using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class DetalleCompra
    {
        public int idDetalle { get; set; }
        public int idCompra { get; set; }
        public int codigoProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public virtual Compras Compras { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
