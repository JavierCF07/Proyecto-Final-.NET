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
    class EmailProveedorModelView : INotifyPropertyChanged, ICommand
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
        private ACCION accion = ACCION.NINGUNO;
        private bool _IsReadOnlyEmail = true;
        private bool _IsReadOnlyCodigoProveedor = true;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledCancel = true;
        private string _email;
        private int _codigoProveedor;
        private EmailProveedorModelView _Instancia;
        private EmailProveedor _SelectEmailProveedor;
        #endregion
        private ObservableCollection<EmailProveedor> _EmailProveedor;
        private ObservableCollection<Proveedores> _Proveedores;
        public ObservableCollection<EmailProveedor> EmailProveedor
        {
            get
            {
                if(this._EmailProveedor == null)
                {
                    this._EmailProveedor = new ObservableCollection<EmailProveedor>();
                    foreach (EmailProveedor elemento in db.EmailProveedores.ToList())
                    {
                        this._EmailProveedor.Add(elemento);
                    }
                }
                return this._EmailProveedor;
            }
            set{ this._EmailProveedor = value; }
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
        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
                NotificarCambio("email");
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
        public bool IsReadOnlyEmail
        {
            get
            {
                return this._IsReadOnlyEmail;
            }
            set
            {
                this._IsReadOnlyEmail = value;
                NotificarCambio("IsReadOnlyEmail");
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
        public EmailProveedorModelView Instancia
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
        public EmailProveedor SelectEmailProveedor
        {
            get
            {
                return this._SelectEmailProveedor;
            }
            set
            {
                if(value != null)
                {
                    this._SelectEmailProveedor = value;
                    this.email = value.email;
                    this.codigoProveedor = value.codigoProveedor;
                    NotificarCambio("SelectEmailProveedor");
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
        public EmailProveedorModelView()
        {
            this.Titulo = "EmailProveedores";
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
                this.IsReadOnlyEmail = false;
                this.IsReadOnlyCodigoProveedor = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = false;
            }
            else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        EmailProveedor nuevo = new EmailProveedor();
                        nuevo.email = this.email;
                        nuevo.codigoProveedor = this.codigoProveedor;
                        db.EmailProveedores.Add(nuevo);
                        db.SaveChanges();
                        this.EmailProveedor.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente");
                        this.IsReadOnlyEmail= true;
                        this.IsReadOnlyCodigoProveedor = true;
                        this.IsEnabledAdd = true;
                        this.IsEnabledCancel = true;
                        this.IsEnabledDelete = true;
                        this.IsEnabledSave = true;
                        this.IsEnabledUpdate = true;
                        this.email = "";
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.EmailProveedor.IndexOf(this.SelectEmailProveedor);
                            var updateEmail = this.db.EmailProveedores.Find(this.SelectEmailProveedor.codigoEmail);
                            updateEmail.email = this.email;
                            updateEmail.codigoProveedor = this.codigoProveedor;
                            this.db.Entry(updateEmail).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.EmailProveedor.RemoveAt(posicion);
                            this.EmailProveedor.Insert(posicion, updateEmail);
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
                if (this.SelectEmailProveedor != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.EmailProveedores.Remove(this.SelectEmailProveedor);
                            db.SaveChanges();
                            this.EmailProveedor.Remove(this.SelectEmailProveedor);
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
                this.IsReadOnlyEmail = false;
                this.IsReadOnlyCodigoProveedor = false;
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyEmail = true;
                this.IsReadOnlyCodigoProveedor = true;
                this.IsEnabledAdd = true;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = true;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = true;
                this.email = "";
            }
        }
    }
}
