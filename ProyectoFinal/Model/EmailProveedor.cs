using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class EmailProveedor
    {
        public int codigoEmail { get; set; }
        public string email { get; set; }
        public int codigoProveedor { get; set; }
        public virtual Proveedores Proveedores { get; set; }
        public override string ToString()
        {
            return Convert.ToString(codigoProveedor);
        }
    }
}
