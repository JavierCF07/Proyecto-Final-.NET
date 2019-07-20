using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class Inventario
    {
        public int codigoInventario { get; set; }
        public int codigoProducto { get; set; }
        public DateTime fecha { get; set; }
        public string tipoRegistro { get; set; }
        public decimal precio { get; set; }
        public int entradas { get; set; }
        public int salidas { get; set; }       
    }
}
