using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class Categoria
    {
        public int codigoCategoria { get; set; }
        public string descripcion { get; set; }
        //public virtual Productos Productos { get; set; }
        public override string ToString()
        {
            return Convert.ToString(codigoCategoria);
        }
    }
}
