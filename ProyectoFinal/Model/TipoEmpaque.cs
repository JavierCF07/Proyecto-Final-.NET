using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class TipoEmpaque
    {
        public int codigoEmpaque { get; set; }
        public string descripcion { get; set; }
        //public virtual Productos Productos { get; set; }
        public override string ToString()
        {
            return Convert.ToString(codigoEmpaque);
        }
    }
}
