using ProyectoFinal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoFinal.ModelView
{
    public class InventarioModelView : INotifyPropertyChanged, ICommand
    {
        enum ACCION
        {
            NINGUNO,
            NUEVO,
            GUARDAR,
            ACTUALIZAR
        }
        #region variables
        private ProyectoDBContext db = new ProyectoDBContext();
        private ACCION nuevo = ACCION.NINGUNO;
        private bool _IsReadOnlyCodigoProducto = false;
        private bool _IsReadOnlyFecha = false;
        private bool _IsReadOnlyTipoRegistro = false;
        private bool _IsReadOnlyPrecio = false;
        private bool _IsReadOnlyEntradas = false;
        private bool _IsReadOnlySalidas = false;
        private int _codigoProducto;
        private DateTime _fecha;
        private string _tipoRegistro;
        private decimal _precio;
        private int _entradas;
        private int _salidas;
        private bool _IsEnabledAdd = false;
        private bool _IsEnabledSave = false;
        private bool _IsEnabledUpdate = false;
        private bool _IsEnabledDelete = false;
        private bool _IsEnabledCancel = false;
        private InventarioModelView _Instancia;
        private Inventario _SelectInventario;
        #endregion
        #region Propiedades
        public bool IsReadOnlyCodigoProducto
        {
            get
            {
                return this._IsReadOnlyCodigoProducto;
            }
            set
            {
                this._IsReadOnlyCodigoProducto = value;
                NotificarCambio("IsReadOnlyCodigoProducto");
            }
        }
        public bool IsReadOnlyFecha
        {
            get
            {
                return this._IsReadOnlyFecha;
            }
            set
            {
                this._IsReadOnlyFecha = value;
                NotificarCambio("IsReadOnlyFecha");
            }
        }
        public bool IsReadOnlyTipoRegistro
        {
            get
            {
                return this._IsReadOnlyTipoRegistro;
            }
            set
            {
                this._IsReadOnlyTipoRegistro = value;
                NotificarCambio("IsReadOnlyTipoRegistro");
            }
        }
        public bool IsReadOnlyPrecio
        {
            get
            {
                return this._IsReadOnlyPrecio;
            }
            set
            {
                this._IsReadOnlyPrecio = value;
                NotificarCambio("IsReadOnlyPrecio");
            }
        }
        public bool IsReadOnlyEntradas
        {
            get
            {
                return this._IsReadOnlyEntradas;
            }
            set
            {
                this._IsReadOnlyEntradas = value;
                NotificarCambio("IsReadOnlyEntradas");
            }
        }
        public bool IsReadOnlySalidas
        {
            get
            {
                return this._IsReadOnlySalidas;
            }
            set
            {
                this._IsReadOnlySalidas = value;
                NotificarCambio("IsReadOnlySalidas");
            }
        }
        public int codigoProducto
        {
            get
            {
                return this._codigoProducto;
            }
            set
            {
                this._codigoProducto = value;
                NotificarCambio("codigoProducto");
            }
        }
        public DateTime fecha
        {
            get
            {
                return this._fecha;
            }
            set
            {
                this._fecha = value;
                NotificarCambio("fecha");
            }
        }
        public string tipoRegistro
        {
            get
            {
                return this._tipoRegistro;
            }
            set
            {
                this._tipoRegistro = value;
                NotificarCambio("tipoRegistro");
            }
        }
        public decimal precio
        {
            get
            {
                return this._precio;
            }
            set
            {
                this._precio = value;
                NotificarCambio("precio");
            }
        }
        public int entradas
        {
            get
            {
                return this._entradas;
            }
            set
            {
                this._entradas = value;
                NotificarCambio("entradas");
            }
        }
        public int salidas
        {
            get
            {
                return this._salidas;
            }
            set
            {
                this._salidas = value;
                NotificarCambio("salidas");
            }
        }
        public bool IsEnabledAdd
        {
            get
            {
                return this._IsEnabledAdd;
            }
            set
            {
                this._IsEnabledAdd = value;
                NotificarCambio("IsEnabledAdd");
            }
        }
        public bool IsEnabledSave
        {
            get
            {
                return this._IsEnabledSave;
            }
            set
            {
                this._IsEnabledSave = false;
                NotificarCambio("IsEnabledSave");
            }
        }
        public bool IsEnabledDelete
        {
            get
            {
                return this._IsEnabledDelete = false;
            }
            set
            {
                this._IsEnabledDelete = false;
                NotificarCambio("IsEnabledDelete");
            }
        }
        public bool IsEnabledCancel
        {
            get
            {
                return this._IsEnabledCancel;
            }
            set
            {
                this._IsEnabledCancel = false;
                NotificarCambio("IsEnabledCancel");
            }
        }
        public bool IsEnabledUpdate
        {
            get
            {
                return this._IsEnabledUpdate;
            }
            set
            {
                this._IsEnabledUpdate = value;
                NotificarCambio("IsEnabledUpdate");
            }
        }
        public InventarioModelView Instancia
        {
            get
            {
                return this._Instancia;
            }
            set
            {
                this._Instancia = value;
                NotificarCambio("Instancia");
            }
        }
        public Inventario SelectInventario
        {
            get
            {
                return this._SelectInventario;
            }
            set
            {
                if(value != null)
                {
                    this._SelectInventario = value;
                    this.tipoRegistro = value.tipoRegistro;
                    this.precio = value.precio;
                    this.entradas = value.entradas;
                    this.salidas = value.salidas;
                    NotificarCambio("SelectInventario");
                }
                
            }
        }
        #endregion

        public InventarioModelView()
        {
            this.Titulo = "Inventario";
            this.Instancia = this;
        }
        public string Titulo { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        
        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Add"))
            {
                this.IsReadOnlyCodigoProducto = false;
                //this.IsReadOnlyEntradas
            }
        }

    }
}
