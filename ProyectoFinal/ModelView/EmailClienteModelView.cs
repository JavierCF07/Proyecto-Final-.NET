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
    class EmailClienteModelView : INotifyPropertyChanged, ICommand
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
        private bool _IsEnabledCancel = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledDelete = true;
        private bool _IsReadOnlyEmail = true;
        private bool _IsReadOnlyNit = true;
        private string _email;
        private string _nit;
        private EmailClienteModelView _Instancia;
        private EmailCliente _SelectEmailCliente;

        private ObservableCollection<EmailCliente> _EmailClientes;
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
        public ObservableCollection<EmailCliente> EmailClientes
        {
            get
            {
                if(this._EmailClientes == null)
                {
                    this._EmailClientes = new ObservableCollection<EmailCliente>();
                    foreach (EmailCliente elemento in db.EmailClientes.ToList())
                    {
                        this._EmailClientes.Add(elemento);
                    }
                }
                return this._EmailClientes;
            }
            set{ this._EmailClientes = value; }
        }
        #endregion
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
        public EmailClienteModelView Instancia
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
        public EmailCliente SelectEmailCliente
        {
            get
            {
                return this._SelectEmailCliente;
            }
            set
            {
                if(value != null)
                {
                    this._SelectEmailCliente = value;
                    this.email = value.email;
                    this.nit = value.email;
                    NotificarCambio("SelectEmailCliente");

                }
            }
        }
        public EmailClienteModelView()
        {
            this.Titulo = "EmailCliente";
            this.Instancia = this;
        }
        #endregion
        private void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
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
                this.IsReadOnlyNit = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = false;
                this.email = "";
                this.nit = "";
            }
            else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        EmailCliente nuevo = new EmailCliente();
                        nuevo.email = this.email;
                        nuevo.nit = this.nit;
                        db.EmailClientes.Add(nuevo);
                        db.SaveChanges();
                        this.EmailClientes.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente");
                        this.IsReadOnlyEmail = true;
                        this.IsReadOnlyNit = true;
                        this.IsEnabledAdd = true;
                        this.IsEnabledCancel = true;
                        this.IsEnabledDelete = true;
                        this.IsEnabledSave = true;
                        this.IsEnabledUpdate = true;
                        this.email = "";
                        this.nit = "";
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.EmailClientes.IndexOf(this.SelectEmailCliente);
                            var updateEmail = this.db.EmailClientes.Find(this.SelectEmailCliente.codigoEmail);
                            updateEmail.email = this.email;
                            updateEmail.nit = this.nit;
                            this.db.Entry(updateEmail).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.EmailClientes.RemoveAt(posicion);
                            this.EmailClientes.Insert(posicion, updateEmail);
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
                if (this.SelectEmailCliente != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.EmailClientes.Remove(this.SelectEmailCliente);
                            db.SaveChanges();
                            this.EmailClientes.Remove(this.SelectEmailCliente);
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
                this.IsReadOnlyEmail = false;
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyNit = true;
                this.IsReadOnlyEmail = true;
                this.IsEnabledAdd = true;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = true;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = true;
                this.nit = "";
                this.email = "";
            }
        }
    }
}
