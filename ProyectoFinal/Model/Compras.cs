using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class Compras
    {
        public int idCompra { get; set; }
        public int numeroDocumento { get; set; }
        public int codigoProveedor { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public virtual Proveedores Proveedores { get; set; }
        public virtual DetalleCompra DetalleCompra { get; set; }
    }
}
