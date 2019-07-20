using ProyectoFinal.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoFinal.ModelView
{
    public class MainModelView : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private MainModelView _Instancia;

        public MainModelView Instancia { get; set; }

        public MainModelView()
        {
            this.Instancia = this;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Categoria"))
            {
                CategoriaView categoria = new CategoriaView();
                categoria.Show();
            }
            else if (parameter.Equals("TipoEmpaque"))
            {
                TipoEmpaqueView tipoEmpaque = new TipoEmpaqueView();
                tipoEmpaque.Show();
            }
            else if (parameter.Equals("Clientes"))
            {
                ClientesView clientes = new ClientesView();
                clientes.Show();
            }
            else if (parameter.Equals("Proveedores"))
            {
                ProveedoresView proveedores = new ProveedoresView();
                proveedores.Show();
            }else if (parameter.Equals("Productos"))
            {
                ProductosView productos = new ProductosView();
                productos.Show();
            }
        }
    }
}
