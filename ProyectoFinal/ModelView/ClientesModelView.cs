using ProyectoFinal.Model;
using ProyectoFinal.View;
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
    public class ClientesModelView : INotifyPropertyChanged, ICommand
    {
        enum ACCION
        {
            NINGUNO,
            NUEVO,
            GUARDAR,
            ACTUALIZAR
        }

        #region variables
        private ProyectoDataContext db = new ProyectoDataContext();
        private ACCION accion = ACCION.NINGUNO;
        private bool _IsReadOnlyNit = true;
        private bool _IsReadOnlyDPI = true;
        private bool _IsReadOnlyNombre = true;
        private bool _IsReadOnlyDireccion = true;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledCancel = true;
        private string _nit;
        private string _dpi;
        private string _nombre;
        private string _direccion;
        private ClientesModelView _Instancia;
        private Clientes _SelectCliente;
        #endregion

        private ObservableCollection<Clientes> _Clientes;

        public ObservableCollection<Clientes> Clientes
        {
            get
            {
                if(this._Clientes == null)
                {
                    this._Clientes = new ObservableCollection<Clientes>();
                    foreach (Clientes elemento in db.Clientes.ToList())
                    {
                        this._Clientes.Add(elemento);
                    }
                }
                return this._Clientes;
            }
            set{ this._Clientes = value; }
        }

        #region Propiedades
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
        public string dpi
        {
            get
            {
                return this._dpi;
            }
            set
            {
                this._dpi = value;
                NotificarCambio("dpi");
            }
        }
        public string nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                this._nombre = value;
                NotificarCambio("nombre");
            }
        }
        public string direccion
        {
            get
            {
                return this._direccion;
            }
            set
            {
                this._direccion = value;
                NotificarCambio("direccion");
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
        public bool IsReadOnlyDPI
        {
            get
            {
                return this._IsReadOnlyDPI;
            }
            set
            {
                this._IsReadOnlyDPI = value;
                NotificarCambio("IsReadOnlyDPI");
            }
        }
        public bool IsReadOnlyNombre
        {
            get
            {
                return this._IsReadOnlyNombre;
            }
            set
            {
                this._IsReadOnlyNombre = value;
                NotificarCambio("IsReadOnlyNombre");
            }
        }
        public bool IsReadOnlyDireccion
        {
            get
            {
                return this._IsReadOnlyDireccion;
            }
            set
            {
                this._IsReadOnlyDireccion = value;
                NotificarCambio("IsReadOnlyDireccion");
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
        public ClientesModelView Instancia
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
        public Clientes SelectCliente
        {
            get
            {
                return this._SelectCliente;
            }
            set
            {
                if(value != null)
                {
                    this._SelectCliente = value;
                    this.nit = value.nit;
                    this.dpi = value.DPI;
                    this.nombre = value.nombre;
                    this.direccion = value.direccion;
                    NotificarCambio("SelectCliente");
                }
            }
        }

        #endregion

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public ClientesModelView()
        {
            this.Titulo = "Clientes";
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
            if (parameter.Equals("EmailClientes"))
            {
                EmailClientesView email = new EmailClientesView();
                email.Show();
            }
            else if (parameter.Equals("TelefonoClientes"))
            {
                TelefonoClientesView telefono = new TelefonoClientesView();
                telefono.Show();
            }
            else if (parameter.Equals("Add"))
            {
                this.IsReadOnlyNit = false;
                this.IsReadOnlyDPI = false;
                this.IsReadOnlyNombre = false;
                this.IsReadOnlyDireccion = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel= true;
                this.IsEnabledDelete= false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate= false;
                this.nit = "";
                this.dpi = "";
                this.nombre = "";
                this.direccion = "";
            }else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        Clientes nuevo = new Clientes();
                        nuevo.nit = this.nit;
                        nuevo.DPI = this.dpi;
                        nuevo.nombre = this.nombre;
                        nuevo.direccion = this.direccion;
                        db.Clientes.Add(nuevo);
                        db.SaveChanges();
                        this.Clientes.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente");
                        this.IsReadOnlyNit = true;
                        this.IsReadOnlyDPI = true;
                        this.IsReadOnlyNombre = true;
                        this.IsReadOnlyDireccion = true;
                        this.IsEnabledAdd = true;
                        this.IsEnabledCancel = true;
                        this.IsEnabledDelete = true;
                        this.IsEnabledSave = true;
                        this.IsEnabledUpdate = true;
                        this.nit = "";
                        this.dpi = "";
                        this.nombre = "";
                        this.direccion = "";
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            if(SelectCliente != null) { 
                            int posicion = this.Clientes.IndexOf(this.SelectCliente);
                            var updateClientes = this.db.Clientes.Find(this.SelectCliente.nit);
                            updateClientes.nit = this.nit;
                            updateClientes.DPI = this.dpi;
                            updateClientes.nombre = this.nombre;
                            updateClientes.direccion = this.direccion;
                            this.db.Entry(updateClientes).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.Clientes.RemoveAt(posicion);
                            this.Clientes.Insert(posicion, updateClientes);
                            MessageBox.Show("Registro actualizado");
                            }
                            else
                            {
                                MessageBox.Show("Debe seleccionar un dato para poder actualizar");
                            }
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
                if (this.SelectCliente != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Clientes.Remove(this.SelectCliente);
                            db.SaveChanges();
                            this.Clientes.Remove(this.SelectCliente);
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
                this.IsReadOnlyNit = false;
                this.IsReadOnlyDPI = false;
                this.IsReadOnlyNombre = false;
                this.IsReadOnlyDireccion = false;
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyNit = true;
                this.IsReadOnlyDPI = true;
                this.IsReadOnlyNombre = true;
                this.IsReadOnlyDireccion = true;
                this.IsEnabledAdd = true;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = true;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = true;
                this.nit = "";
                this.dpi = "";
                this.nombre = "";
                this.direccion = "";
            }
        }
    }
}


