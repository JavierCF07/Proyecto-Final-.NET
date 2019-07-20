using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class TelefonoProveedor
    {
        public int codigoTelefono { get; set; }
        public string numero { get; set; }
        public string descripcion { get; set; }
        public int codigoProveedor { get; set; }
        public virtual Proveedores Proveedores { get; set; }
    }
}
