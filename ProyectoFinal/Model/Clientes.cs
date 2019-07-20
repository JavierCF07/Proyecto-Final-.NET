using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class Clientes
    {
        public string nit { get; set; }
        public string DPI { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public override string ToString()
        {
            return nit;
        }

        //public virtual TelefonoCliente TelefonoCliente { get; set; }
        //public virtual EmailCliente EmailCliente { get; set; }
        //public virtual Factura Factura { get; set; }
    }
}
