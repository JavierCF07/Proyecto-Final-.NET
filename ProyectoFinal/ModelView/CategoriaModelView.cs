using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProyectoFinal.Model;
namespace ProyectoFinal.ModelView
{
    public class CategoriaModelView : INotifyPropertyChanged, ICommand
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
        private bool _IsReadOnlyDescription = true;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledCancel = true;
        private string _descripcion;
        private CategoriaModelView _Instancia;
        private Categoria _SelectCategoria;
        #endregion
        private ObservableCollection<Categoria> _Categorias;

        public ObservableCollection<Categoria> Categorias
        {
            get
            {
                if (this._Categorias == null)
                {
                    this._Categorias = new ObservableCollection<Categoria>();
                    foreach (Categoria elemento in db.Categorias.ToList())
                    {
                        this._Categorias.Add(elemento);
                    }
                }
                return this._Categorias;
            }
            set { this._Categorias = value; }
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

        public CategoriaModelView Instancia
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

        public Categoria SelectCategoria
        {
            get
            {
                return this._SelectCategoria;
            }
            set
            {
                if(value != null)
                {
                    this._SelectCategoria = value;
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
        #region Constructor
        public CategoriaModelView()
        {
            this.Titulo = "Categorias";
            this.Instancia = this;
            
        }
        public string Titulo { get; set; }
        #endregion

       
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
                        Categoria nuevo = new Categoria();
                        nuevo.descripcion = this.descripcion;
                        db.Categorias.Add(nuevo);
                        db.SaveChanges();
                        this.Categorias.Add(nuevo);
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
                            int posicion = this.Categorias.IndexOf(this.SelectCategoria);
                            var updateCategoria = this.db.Categorias.Find(this.SelectCategoria.codigoCategoria);
                            updateCategoria.descripcion = this.descripcion;
                            this.db.Entry(updateCategoria).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.Categorias.RemoveAt(posicion);
                            this.Categorias.Insert(posicion, updateCategoria);
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
                if (this.SelectCategoria != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Categorias.Remove(this.SelectCategoria);
                            db.SaveChanges();
                            this.Categorias.Remove(this.SelectCategoria);
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
