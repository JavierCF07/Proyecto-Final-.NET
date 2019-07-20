using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class TelefonoCliente
    {
        public int codigoTelefono { get; set; }
        public string numero { get; set; }
        public string descripcion { get; set; }
        public string nit { get; set; }
        public virtual Clientes Clientes { get; set; }
    }
}
