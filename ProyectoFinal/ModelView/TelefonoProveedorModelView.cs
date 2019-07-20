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
    class TelefonoProveedorModelView : INotifyPropertyChanged, ICommand
    {
        enum ACCION
        {
            NUEVO,
            GUARDAR,
            NINGUNO,
            ACTUALIZAR
        }
        #region variables
        private ProyectoDataContext db = new ProyectoDataContext();
        private ACCION accion = ACCION.NINGUNO;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledCancel = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledDelete = true;
        private bool _IsReadOnlyNumero = true;
        private bool _IsReadOnlyDescripcion = true;
        private bool _IsReadOnlyCodigoProveedor = true;
        private string _numero;
        private string _descripcion;
        private int _codigoProveedor;
        private TelefonoProveedorModelView _Instancia;
        private TelefonoProveedor _SelectTelefonoProveedor;
        #endregion
        private ObservableCollection<TelefonoProveedor> _TelefonoProveedor;
        private ObservableCollection<Proveedores> _Proveedores;
        public ObservableCollection<TelefonoProveedor> TelefonoProveedor
        {
            get
            {
                if(this._TelefonoProveedor == null)
                {
                    this._TelefonoProveedor = new ObservableCollection<TelefonoProveedor>();
                    foreach (TelefonoProveedor elemento in db.TelefonoProveedores.ToList())
                    {
                        this._TelefonoProveedor.Add(elemento);
                    }
                }
                return this._TelefonoProveedor;
            }
            set{ this._TelefonoProveedor = value; }
        }
        public ObservableCollection<Proveedores> Proveedores
        {
            get
            {
                if (this._Proveedores == null)
                {
                    this._Proveedores = new ObservableCollection<Proveedores>();
                    foreach (Proveedores elemento in db.Proveedores.ToList())
                    {
                        this._Proveedores.Add(elemento);
                    }
                }
                return this._Proveedores;
            }
            set { this._Proveedores = value; }
        }
        #region Propiedades
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
        public bool IsReadOnlyCodigoProveedor
        {
            get
            {
                return this._IsReadOnlyCodigoProveedor;
            }
            set
            {
                this._IsReadOnlyCodigoProveedor = value;
                NotificarCambio("IsReadOnlyCodigoProveedor");
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
        public int codigoProveedor
        {
            get
            {
                return this._codigoProveedor;
            }
            set
            {
                this._codigoProveedor = value;
                NotificarCambio("codigoProveedor");
            }
        }
        public TelefonoProveedorModelView Instancia
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
        public TelefonoProveedor SelectTelefonoProveedor
        {
            get
            {
                return this._SelectTelefonoProveedor;
            }
            set
            {
                if(value != null)
                {
                    this._SelectTelefonoProveedor = value;
                    this.numero = value.numero;
                    this.descripcion = value.descripcion;
                    this.codigoProveedor = value.codigoProveedor;
                    NotificarCambio("SelectTelefonoProveedor");
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
        public TelefonoProveedorModelView()
        {
            this.Titulo = "TelefonoProveedor";
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
                this.IsReadOnlyNumero = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = false;
                this.descripcion = "";
                this.numero = "";
            }
            else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        TelefonoProveedor nuevo = new TelefonoProveedor();
                        nuevo.numero = this.numero;
                        nuevo.descripcion = this.descripcion;
                        nuevo.codigoProveedor = this.codigoProveedor;
                        db.TelefonoProveedores.Add(nuevo);
                        db.SaveChanges();
                        this.TelefonoProveedor.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente");
                        this.IsReadOnlyNumero = true;
                        this.IsReadOnlyDescripcion = true;
                        this.IsEnabledAdd = true;
                        this.IsEnabledCancel = true;
                        this.IsEnabledDelete = true;
                        this.IsEnabledSave = true;
                        this.IsEnabledUpdate = true;
                        this.descripcion = "";
                        this.numero = "";
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.TelefonoProveedor.IndexOf(this.SelectTelefonoProveedor);
                            var updateTelefono = this.db.TelefonoProveedores.Find(this.SelectTelefonoProveedor.codigoTelefono);
                            updateTelefono.numero = this.numero;
                            updateTelefono.descripcion = this.descripcion;
                            updateTelefono.codigoProveedor = this.codigoProveedor;
                            this.db.Entry(updateTelefono).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.TelefonoProveedor.RemoveAt(posicion);
                            this.TelefonoProveedor.Insert(posicion, updateTelefono);
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
                if (this.SelectTelefonoProveedor != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.TelefonoProveedores.Remove(this.SelectTelefonoProveedor);
                            db.SaveChanges();
                            this.TelefonoProveedor.Remove(this.SelectTelefonoProveedor);
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
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyNumero = true;
                this.IsReadOnlyDescripcion = true;
                this.IsEnabledAdd = true;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = true;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = true;
                this.numero = "";
                this.descripcion = "";
            }
        }
    }
}
