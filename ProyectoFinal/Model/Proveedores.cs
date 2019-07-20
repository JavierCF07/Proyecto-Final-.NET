using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    public class Proveedores
    {
        public int codigoProveedor { get; set; }
        public string nit { get; set; }
        public string razonSocial { get; set; }
        public string direccion { get; set; }
        public string paginaWeb { get; set; }
        public string contactoPrincipal { get; set; }
        //public virtual TelefonoProveedor TelefonoProveedor { get; set; }
        //public virtual EmailProveedor EmailProveedor { get; set; }
        //public virtual Compras Compras { get; set; }
        public override string ToString()
        {
            return Convert.ToString(codigoProveedor);
        }
    }
}
