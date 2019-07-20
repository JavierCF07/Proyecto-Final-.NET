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
    class ProductosModelView : INotifyPropertyChanged, ICommand
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
        private bool _IsReadOnlyCodigoCategoria = true;
        private bool _IsReadOnlyTipoEmpaque = true;
        private bool _IsReadOnlyDescripcion = true;
        private bool _IsReadOnlyPrecioUnitario = true;
        private bool _IsReadOnlyPrecioPorDocena = true;
        private bool _IsReadOnlyPrecioPorMayor = true;
        private bool _IsReadOnlyExistencia = true;
        private bool _IsReadOnlyImagen = true;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledCancel = true;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledUpdate = true;
        private int _codigoCategoria;
        private int _codigoEmpaque;
        private string _descripcion;
        private decimal _precioUnitario;
        private decimal _precioPorDocena;
        private decimal _precioPorMayor;
        private int _existencia;
        private string _imagen;
        private ProductosModelView _Instancia;
        private Productos _SelectProductos;
        #endregion
        private ObservableCollection<Productos> _Productos;
        public ObservableCollection<Productos> Productos
        {
            get
            {
                if(this._Productos == null)
                {
                    this._Productos = new ObservableCollection<Productos>();
                    foreach (Productos elemento in db.Productos.ToList())
                    {
                        this._Productos.Add(elemento);
                    }
                }
                return this._Productos;
            }
            set { this._Productos = value; }
        }
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
        private ObservableCollection<TipoEmpaque> _TipoEmpaques;

        public ObservableCollection<TipoEmpaque> TipoEmpaques
        {
            get
            {
                if (this._TipoEmpaques == null)
                {
                    this._TipoEmpaques = new ObservableCollection<TipoEmpaque>();
                    foreach (TipoEmpaque elemento in db.TipoEmpaques.ToList())
                    {
                        this._TipoEmpaques.Add(elemento);
                    }
                }
                return this._TipoEmpaques;
            }
            set { this._TipoEmpaques = value; }
        }
        #region Propiedades
        public bool IsReadOnlyCodigoCategoria
        {
            get
            {
                return this._IsReadOnlyCodigoCategoria;
            }
            set
            {
                this._IsReadOnlyCodigoCategoria = value;
                NotificarCambio("IsReadOnlyCodigoCategoria");
            }
        }
        public bool IsReadOnlyTipoEmpaque
        {
            get
            {
                return this._IsReadOnlyTipoEmpaque;
            }
            set
            {
                this._IsReadOnlyTipoEmpaque = value;
                NotificarCambio("IsReadOnlyTipoEmpaque");
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
        public bool IsReadOnlyPrecioUnitario
        {
            get
            {
                return this._IsReadOnlyPrecioUnitario;
            }
            set
            {
                this._IsReadOnlyPrecioUnitario = value;
                NotificarCambio("IsReadOnlyPrecioUnitario");
            }
        }
        public bool IsReadOnlyPrecioPorDocena
        {
            get
            {
                return this._IsReadOnlyPrecioPorDocena;
            }
            set
            {
                this._IsReadOnlyPrecioPorDocena = value;
                NotificarCambio("IsReadOnlyPrecioPorDocena");
            }
        }
        public bool IsReadOnlyPrecioPorMayor
        {
            get
            {
                return this._IsReadOnlyPrecioPorMayor;
            }
            set
            {
                this._IsReadOnlyPrecioPorMayor = value;
                NotificarCambio("IsReadOnlyPrecioPorMayor");
            }
        }
        public bool IsReadOnlyExistencia
        {
            get
            {
                return this._IsReadOnlyExistencia;
            }
            set
            {
                this._IsReadOnlyExistencia = value;
                NotificarCambio("IsReadOnlyExistencia");
            }
        }
        public bool IsReadOnlyImagen
        {
            get
            {
                return this._IsReadOnlyImagen;
            }
            set
            {
                this._IsReadOnlyImagen = value;
                NotificarCambio("IsReadOnlyImagen");
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
        public int codigoCategoria
        {
            get
            {
                return this._codigoCategoria;
            }
            set
            {
                this._codigoCategoria = value;
                NotificarCambio("codigoCategoria");
            }
        }
        public int codigoEmpaque
        {
            get
            {
                return this._codigoEmpaque;
            }
            set
            {
                this._codigoEmpaque = value;
                NotificarCambio("codigoEmpaque");
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
        public decimal precioUnitario
        {
            get
            {
                return this._precioUnitario;
            }
            set
            {
                this._precioUnitario = value;
                NotificarCambio("precioUnitario");
            }
        }
        public decimal precioPorDocena
        {
            get
            {
                return this._precioPorDocena;
            }
            set
            {
                this._precioPorDocena = value;
                NotificarCambio("precioPorDocena");
            }
        }
        public decimal precioPorMayor
        {
            get
            {
                return this._precioPorMayor;
            }
            set
            {
                this._precioPorMayor = value;
                NotificarCambio("precioPorMayor");
            }
        }
        public int existencia
        {
            get
            {
                return this._existencia;
            }
            set
            {
                this._existencia = value;
                NotificarCambio("existencia");
            }
        }
        public string imagen
        {
            get
            {
                return this._imagen;
            }
            set
            {
                this._imagen = value;
                NotificarCambio("imagen");
            }
        }
        public ProductosModelView Instancia
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
        public Productos SelectProductos
        {
            get
            {
                return this._SelectProductos;
            }
            set
            {
                this._SelectProductos = value;
                NotificarCambio("SelectProductos");
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
        public ProductosModelView()
        {
            this.Titulo = "Productos";
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
                this.IsReadOnlyCodigoCategoria = false;
                this.IsReadOnlyTipoEmpaque = false;
                this.IsReadOnlyDescripcion = false;
                this.IsReadOnlyExistencia = false;
                this.IsReadOnlyImagen = false;
                this.IsReadOnlyPrecioPorDocena = false;
                this.IsReadOnlyPrecioPorMayor = false;
                this.IsReadOnlyPrecioUnitario = false;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledCancel = true;
                this.IsEnabledDelete = false;
                this.IsEnabledSave = true;
                this.IsEnabledUpdate = false;
                this.descripcion = "";
                this.precioUnitario = 0;
                this.precioPorMayor = 0;
                this.precioPorDocena = 0;
                this.existencia = 0;
                this.imagen = "";
            }
            else if (parameter.Equals("Save"))
            {
                switch (this.accion)
                {
                    case ACCION.NUEVO:
                        Productos nuevo = new Productos();
                        nuevo.codigoCategoria = this.codigoCategoria;
                        nuevo.codigoEmpaque = this.codigoEmpaque;
                        nuevo.descripcion = this.descripcion;
                        nuevo.precioUnitario = this.precioUnitario;
                        nuevo.precioPorMayor = this.precioPorMayor;
                        nuevo.precioPorDocena = this.precioPorDocena;
                        nuevo.existencia = this.existencia;
                        nuevo.imagen = this.imagen;
                        db.Productos.Add(nuevo);
                        db.SaveChanges();
                        this.Productos.Add(nuevo);
                        MessageBox.Show("Registro almacenado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.None);
                        this.IsReadOnlyCodigoCategoria = true;
                        this.IsReadOnlyTipoEmpaque = true;
                        this.IsReadOnlyPrecioUnitario = true;
                        this.IsReadOnlyPrecioPorMayor = true;
                        this.IsReadOnlyPrecioPorDocena = true;
                        this.IsReadOnlyDescripcion = true;
                        this.IsReadOnlyExistencia = true;
                        this.IsReadOnlyImagen = true;
                        this.descripcion = "";
                        this.imagen = "";
                        this.precioPorDocena = 0;
                        this.precioPorMayor = 0;
                        this.precioUnitario = 0;
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.Productos.IndexOf(this.SelectProductos);
                            var updateProductos = this.db.Productos.Find(this.SelectProductos.codigoProducto);
                            updateProductos.codigoCategoria = this.codigoCategoria;
                            updateProductos.codigoEmpaque = this.codigoEmpaque;
                            updateProductos.descripcion = this.descripcion;
                            updateProductos.precioPorDocena = this.precioPorDocena;
                            updateProductos.precioPorMayor = this.precioPorMayor;
                            updateProductos.precioUnitario = this.precioUnitario;
                            updateProductos.existencia = this.existencia;
                            updateProductos.imagen = this.imagen;
                            this.db.Entry(updateProductos).State = System.Data.Entity.EntityState.Modified;
                            this.db.SaveChanges();
                            this.Productos.RemoveAt(posicion);
                            this.Productos.Insert(posicion, updateProductos);
                            MessageBox.Show("Registro actualizado correctamente");
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
                if (this.SelectProductos != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if(respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Productos.Remove(this.SelectProductos);
                            db.SaveChanges();
                            this.Productos.Remove(this.SelectProductos);
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
                    MessageBox.Show("Debe seleccionar un registro", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
            }
            else if (parameter.Equals("Update"))
            {
                this.accion = ACCION.ACTUALIZAR;
                this.IsReadOnlyCodigoCategoria = false;
                this.IsReadOnlyDescripcion = false;
                this.IsReadOnlyExistencia = false;
                this.IsReadOnlyImagen = false;
                this.IsReadOnlyPrecioPorDocena = false;
                this.IsReadOnlyPrecioPorMayor = false;
                this.IsReadOnlyPrecioUnitario = false;
                this.IsReadOnlyTipoEmpaque = false;
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsReadOnlyCodigoCategoria = true;
                this.IsReadOnlyDescripcion = true;
                this.IsReadOnlyExistencia = true;
                this.IsReadOnlyImagen = true;
                this.IsReadOnlyPrecioPorDocena = true;
                this.IsReadOnlyPrecioPorMayor = true;
                this.IsReadOnlyPrecioUnitario = true;
                this.IsReadOnlyTipoEmpaque = true;
            }
        }
    }
}
