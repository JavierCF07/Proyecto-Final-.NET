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
    public class TipoEmpaqueModelView : INotifyPropertyChanged, ICommand
    {
        enum ACCION
        {
            NINGUNO,
            NUEVO,
            GUARDAR,
            ACTUALIZAR,
        }
        #region Variables
        private ProyectoDataContext db = new ProyectoDataContext();
        private ACCION accion = ACCION.NINGUNO;
        private bool _IsReadOnlyDescription = true;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledCancel = true;
        private string _descripcion;
        private TipoEmpaqueModelView _Instancia;
        private TipoEmpaque _SelectTipoEmpaque;
        #endregion
        private ObservableCollection<TipoEmpaque> _TipoEmpaques;

        public ObservableCollection<TipoEmpaque> TipoEmpaques
        {
            get
            {
                if(this._TipoEmpaques == null)
                {
                    this._TipoEmpaques = new ObservableCollection<TipoEmpaque>();
                    foreach (TipoEmpaque elemento in db.TipoEmpaques.ToList())
                    {
                        this._TipoEmpaques.Add(elemento);
                    }
                }
                return this._TipoEmpaques;
            }
            set{ this._TipoEmpaques = value; }
        }

        #region Propiedades
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
        public bool IsReadOnlyDescription
        {
            get
            {
                return this._IsReadOnlyDescription;
            }
            set
            {
                this._IsReadOnlyDescription = value;
                NotificarCambio("IsReadOnlyDescription");
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
                this._IsEnabledSave = value;
                NotificarCambio("IsEnabledSave");
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

        public TipoEmpaqueModelView Instancia
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

        public TipoEmpaque SelectTipoEmpaque
        {
            get
            {
                return this._SelectTipoEmpaque;
            }
            set
            {
                if (value != null)
                {
                    this._SelectTipoEmpaque = value;
                    this.descripcion = value.descripcion;
                    NotificarCambio("SelectCategoria");
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

        public TipoEmpaqueModelView()
        {
            this.Titulo = "Tipo de Empaque";
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
                this.IsReadOnlyDescription = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = false;
                this.descripcion = "";
            }
            else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        TipoEmpaque nuevo = new TipoEmpaque();
                        nuevo.descripcion = this.descripcion;
                        db.TipoEmpaques.Add(nuevo);
                        db.SaveChanges();
                        this.TipoEmpaques.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente");
                        this.IsReadOnlyDescription = true;
                        this.IsEnabledAdd = true;
                        this.IsEnabledCancel = true;
                        this.IsEnabledDelete = true;
                        this.IsEnabledSave = true;
                        this.IsEnabledUpdate = true;
                        this.descripcion = "";
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.TipoEmpaques.IndexOf(this.SelectTipoEmpaque);
                            var updateTipoEmpaque = this.db.TipoEmpaques.Find(this.SelectTipoEmpaque.codigoEmpaque);
                            updateTipoEmpaque.descripcion = this.descripcion;
                            this.db.Entry(updateTipoEmpaque).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.TipoEmpaques.RemoveAt(posicion);
                            this.TipoEmpaques.Insert(posicion, updateTipoEmpaque);
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
                if (this.SelectTipoEmpaque != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.TipoEmpaques.Remove(this.SelectTipoEmpaque);
                            db.SaveChanges();
                            this.TipoEmpaques.Remove(this.SelectTipoEmpaque);
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
                this.IsReadOnlyDescription = false;
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyDescription = true;
                this.IsEnabledAdd = true;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = true;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = true;
                this.descripcion = "";
            }
        }
    }
}
