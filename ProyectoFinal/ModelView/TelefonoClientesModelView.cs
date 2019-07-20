using ProyectoFinal.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProyectoFinal.ModelView
{
    class TelefonoClientesModelView : INotifyPropertyChanged, ICommand
    {
        enum ACCION
        {
            NUEVO,
            NINGUNO,
            GUARDAR,
            ACTUALIZAR
        }
        #region variables
        private ProyectoDataContext db = new ProyectoDataContext();
        private ACCION accion = ACCION.NINGUNO;
        private bool _IsReadOnlyNumero = true;
        private bool _IsReadOnlyDescripcion = true;
        private bool _IsReadOnlyNit = true;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledCancel = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledSave = true;
        private string _numero;
        private string _descripcion;
        private string _nit;
        private TelefonoClientesModelView _Instancia;
        private TelefonoCliente _SelectTelefonoCliente;
        #endregion

        private ObservableCollection<TelefonoCliente> _TelefonoClientes;
        private ObservableCollection<Clientes> _Clientes;

        public ObservableCollection<Clientes> Clientes
        {
            get
            {
                if (this._Clientes == null)
                {
                    this._Clientes = new ObservableCollection<Clientes>();
                    foreach (Clientes elemento in db.Clientes.ToList())
                    {
                        this._Clientes.Add(elemento);
                    }
                }
                return this._Clientes;
            }
            set { this._Clientes = value; }
        }
        public ObservableCollection<TelefonoCliente> TelefonoClientes
        {
            get
            {
                if(this._TelefonoClientes == null)
                {
                    this._TelefonoClientes = new ObservableCollection<TelefonoCliente>();
                    foreach (TelefonoCliente elemento in db.TelefonoClientes.ToList())
                    {
                        this._TelefonoClientes.Add(elemento);
                    }
                }
                return this._TelefonoClientes;
            }
            set { this._TelefonoClientes = value; }
        }
        #region Propiedades
        public bool IsReadOnlyNumero
        {
            get
            {
                return this._IsReadOnlyNumero;
            }
            set
            {
                this._IsReadOnlyNumero = value;
                NotificarCambio("IsReadOnlyNumero");
            }
        }
        public bool IsReadOnlyDescripcion
        {
            get
            {
                return this._IsReadOnlyDescripcion;
            }
            set
            {
                this._IsReadOnlyDescripcion = value;
                NotificarCambio("IsReadOnlyDescripcion");
            }
        }
        public bool IsReadOnlyNit
        {
            get
            {
                return this._IsReadOnlyNit;
            }
            set
            {
                this._IsReadOnlyNit = value;
                NotificarCambio("IsReadOnlyNit");
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
        public bool IsEnabledDelete
        {
            get
            {
                return this._IsEnabledDelete;
            }
            set
            {
                this._IsEnabledDelete = value;
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
                this._IsEnabledCancel = value;
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
        public bool IsEnabledSave
        {
            get
            {
                return this._IsEnabledSave;
            }
            set
            {
                this._IsEnabledSave = value;
                NotificarCambio("IsEnabledSave");
            }
        }
        public string numero
        {
            get
            {
                return this._numero;
            }
            set
            {
                this._numero = value;
                NotificarCambio("numero");
            }
        }
        public string descripcion
        {
            get
            {
                return this._descripcion;
            }
            set
            {
                this._descripcion = value;
                NotificarCambio("descripcion");
            }
        }
        public string nit
        {
            get
            {
                return this._nit;
            }
            set
            {
                this._nit = value;
                NotificarCambio("nit");
            }
        }
        public TelefonoClientesModelView Instancia
        {
            get
            {
                return this._Instancia;
            }
            set
            {
                this._Instancia = value;
            }
        }
        public TelefonoCliente SelectTelefonoClientes
        {
            get
            {
                return this._SelectTelefonoCliente;
            }
            set
            {
                if(value != null)
                {
                    this._SelectTelefonoCliente = value;
                    this.numero = value.numero;
                    this.descripcion = value.descripcion;
                    this.nit = value.nit;
                    NotificarCambio("SelectTelefonoCliente");
                }
            }
        }
        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion
        public TelefonoClientesModelView()
        {
            this.Titulo = "Telefono Clientes";
            this.Instancia = this;
        }
        public string Titulo { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Add"))
            {
                this.IsReadOnlyDescripcion = false;
                this.IsReadOnlyNit = false;
                this.IsReadOnlyNumero = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = false;
                this.descripcion = "";
                this.nit = "";
                this.numero = "";
            }
            else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        TelefonoCliente nuevo = new TelefonoCliente();
                        nuevo.numero = this.numero;
                        nuevo.descripcion = this.descripcion;
                        nuevo.nit = this.nit;
                        db.TelefonoClientes.Add(nuevo);
                        db.SaveChanges();
                        this.TelefonoClientes.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente");
                        this.IsReadOnlyDescripcion = true;
                        this.IsReadOnlyNit = true;
                        this.IsReadOnlyNumero = true;
                        this.IsEnabledAdd = true;
                        this.IsEnabledCancel = true;
                        this.IsEnabledDelete = true;
                        this.IsEnabledSave = true;
                        this.IsEnabledUpdate = true;
                        this.descripcion = "";
                        this.numero = "";
                        this.nit = "";
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.TelefonoClientes.IndexOf(this.SelectTelefonoClientes);
                            var updateTelefono = this.db.TelefonoClientes.Find(this.SelectTelefonoClientes.codigoTelefono);
                            updateTelefono.numero = this.numero;
                            updateTelefono.descripcion = this.descripcion;
                            updateTelefono.nit = this.nit;
                            this.db.Entry(updateTelefono).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.TelefonoClientes.RemoveAt(posicion);
                            this.TelefonoClientes.Insert(posicion, updateTelefono);
                            MessageBox.Show("Registro actualizado");

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        break;
                }
            }
            else if (parameter.Equals("Delete"))
            {
                if (this.SelectTelefonoClientes != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.TelefonoClientes.Remove(this.SelectTelefonoClientes);
                            db.SaveChanges();
                            this.TelefonoClientes.Remove(this.SelectTelefonoClientes);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        MessageBox.Show("Registro eliminado correctamente");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (parameter.Equals("Update"))
            {
                this.accion = ACCION.ACTUALIZAR;
                this.IsReadOnlyNumero = false;
                this.IsReadOnlyDescripcion = false;
                this.IsReadOnlyNit = false;
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyNumero = true;
                this.IsReadOnlyDescripcion = true;
                this.IsReadOnlyNit = true;
                this.IsEnabledAdd = true;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = true;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = true;
                this.descripcion = "";
                this.numero = "";
                this.nit = "";
            }
        }
    }
}
