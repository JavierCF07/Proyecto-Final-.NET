using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class Productos
    {
        public int codigoProducto { get; set; }
        public int codigoCategoria { get; set; }
        public int codigoEmpaque { get; set; }
        public string descripcion { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioPorDocena { get; set; }
        public decimal precioPorMayor { get; set; }
        public int existencia { get; set; }
        public string imagen { get; set; }


        public override string ToString()
        {
            return descripcion;
        }
    }
}
