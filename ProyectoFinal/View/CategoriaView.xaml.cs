﻿using MahApps.Metro.Controls;
using ProyectoFinal.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoFinal.View
{
    /// <summary>
    /// Lógica de interacción para CategoriaView.xaml
    /// </summary>
    public partial class CategoriaView : MetroWindow
    {
        public CategoriaView()
        {
            InitializeComponent();
            CategoriaModelView modelo = new CategoriaModelView();
            this.DataContext = modelo;
        }
    }
}
