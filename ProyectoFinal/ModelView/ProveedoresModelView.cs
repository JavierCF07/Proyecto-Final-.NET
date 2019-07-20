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
    class ProveedoresModelView : INotifyPropertyChanged, ICommand
    {
        enum ACCION
        {
            NINGUNO,
            NUEVO,
            ACTUALIZAR,
            GUARDAR
        }
        #region variables
        private ProyectoDataContext db = new ProyectoDataContext();
        private ACCION accion = ACCION.NUEVO;
        private bool _IsReadOnlyNit = true;
        private bool _IsReadOnlyRazonSocial = true;
        private bool _IsReadOnlyDireccion = true;
        private bool _IsReadOnlyPaginaWeb = true;
        private bool _IsReadOnlyContactoPrincipal = true;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledCancel = true;
        private string _nit;
        private string _razonSocial;
        private string _direccion;
        private string _paginaWeb;
        private string _contactoPrincipal;
        private ProveedoresModelView _Instancia;
        private Proveedores _SelectProveedores;
        #endregion
        private ObservableCollection<Proveedores> _Proveedores;

        public ObservableCollection<Proveedores> Proveedores
        {
            get
            {
                if(this._Proveedores == null)
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
        public string razonSocial
        {
            get
            {
                return this._razonSocial;
            }
            set
            {
                this._razonSocial = value;
                NotificarCambio("razonSocial");
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
        public string paginaWeb
        {
            get
            {
                return this._paginaWeb;
            }
            set
            {
                this._paginaWeb = value;
                NotificarCambio("paginaWeb");
            }
        }
        public string contactoPrincipal
        {
            get
            {
                return this._contactoPrincipal;
            }
            set
            {
                this._contactoPrincipal = value;
                NotificarCambio("contactoPrincipal");
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
        public bool IsReadOnlyRazonSocial
        {
            get
            {
                return this._IsReadOnlyRazonSocial;
            }
            set
            {
                this._IsReadOnlyRazonSocial = value;
                NotificarCambio("IsReadOnlyRazonSocial");
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
        public bool IsReadOnlyPaginaWeb
        {
            get
            {
                return this._IsReadOnlyPaginaWeb;
            }
            set
            {
                this._IsReadOnlyPaginaWeb = value;
                NotificarCambio("IsReadOnlyPaginaWeb");
            }
        }
        public bool IsReadOnlyContactoPrincipal
        {
            get
            {
                return this._IsReadOnlyContactoPrincipal;
            }
            set
            {
                this._IsReadOnlyContactoPrincipal = value;
                NotificarCambio("IsReadOnlyContactoPrincipal");
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
        public ProveedoresModelView Instancia
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
        public Proveedores SelectProveedores
        {
            get
            {
                return this._SelectProveedores;
            }
            set
            {
                if(value != null)
                {
                    this._SelectProveedores = value;
                    this.nit = value.nit;
                    this.razonSocial = value.razonSocial;
                    this.direccion = value.direccion;
                    this.paginaWeb = value.paginaWeb;
                    this.contactoPrincipal = value.contactoPrincipal;
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
        public ProveedoresModelView()
        {
            this.Titulo = "Proveedores";
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
            if (parameter.Equals("EmailProveedor"))
            {
                EmailProveedorView emailProveedor = new EmailProveedorView();
                emailProveedor.Show();
            }else if (parameter.Equals("TelefonoProveedor"))
            {
                TelefonoProveedorView telefonoProveedor = new TelefonoProveedorView();
                telefonoProveedor.Show();
            }
            else if (parameter.Equals("Add"))
            {
                this.IsReadOnlyNit = false;
                this.IsReadOnlyRazonSocial = false;
                this.IsReadOnlyDireccion = false;
                this.IsReadOnlyPaginaWeb = false;
                this.IsReadOnlyContactoPrincipal = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = false;
                this.nit = "";
                this.razonSocial = "";
                this.direccion = "";
                this.paginaWeb = "";
                this.contactoPrincipal = "";
            }
            else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        Proveedores nuevo = new Proveedores();
                        nuevo.nit = this.nit;
                        nuevo.razonSocial = this.razonSocial;
                        nuevo.direccion = this.direccion;
                        nuevo.paginaWeb = this.paginaWeb;
                        nuevo.contactoPrincipal = this.contactoPrincipal;
                        db.Proveedores.Add(nuevo);
                        db.SaveChanges();
                        this.Proveedores.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente");
                        this.IsReadOnlyNit = true;
                        this.IsReadOnlyRazonSocial = true;
                        this.IsReadOnlyDireccion = true;
                        this.IsReadOnlyPaginaWeb = true;
                        this.IsReadOnlyContactoPrincipal = true;
                        this.IsEnabledAdd = true;
                        this.IsEnabledCancel = true;
                        this.IsEnabledDelete = true;
                        this.IsEnabledSave = true;
                        this.IsEnabledUpdate = true;
                        this.nit = "";
                        this.razonSocial = "";
                        this.direccion = "";
                        this.paginaWeb = "";
                        this.contactoPrincipal = "";
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.Proveedores.IndexOf(this.SelectProveedores);
                            var updateProveedores = this.db.Proveedores.Find(this.SelectProveedores.codigoProveedor);
                            updateProveedores.nit = this.nit;
                            updateProveedores.razonSocial = this.razonSocial;
                            updateProveedores.direccion = this.direccion;
                            updateProveedores.paginaWeb = this.paginaWeb;
                            updateProveedores.contactoPrincipal = this.contactoPrincipal;
                            this.db.Entry(updateProveedores).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.Proveedores.RemoveAt(posicion);
                            this.Proveedores.Insert(posicion, updateProveedores);
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
                if (this.SelectProveedores != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Proveedores.Remove(this.SelectProveedores);
                            db.SaveChanges();
                            this.Proveedores.Remove(this.SelectProveedores);
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
                this.IsReadOnlyRazonSocial = false;
                this.IsReadOnlyDireccion = false;
                this.IsReadOnlyPaginaWeb = false;
                this.IsReadOnlyContactoPrincipal = false;
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyNit = true;
                this.IsReadOnlyRazonSocial = true;
                this.IsReadOnlyDireccion = true;
                this.IsReadOnlyPaginaWeb = true;
                this.IsReadOnlyContactoPrincipal = true;
                this.IsEnabledAdd = true;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = true;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = true;
                this.nit = "";
                this.razonSocial = "";
                this.direccion = "";
                this.paginaWeb = "";
                this.contactoPrincipal = "";
            }
        }
    }
}
