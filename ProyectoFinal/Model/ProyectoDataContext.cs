using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProyectoFinal.Model
{
    public class ProyectoDataContext : DbContext
    {
        #region tablas base de datos
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TipoEmpaque> TipoEmpaques { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<EmailCliente> EmailClientes { get; set; }
        public DbSet<TelefonoCliente> TelefonoClientes { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<EmailProveedor> EmailProveedores { get; set; }
        public DbSet<TelefonoProveedor> TelefonoProveedores { get; set; }
        //public DbSet<Compras> Compras { get; set; }
        //public DbSet<DetalleCompra> DetalleCompras { get; set; }
        //public DbSet<Factura> Facturas { get; set; }
        //public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        //public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Productos> Productos { get; set; }
        #endregion
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Productos>()
                .ToTable("Productos")
                .HasKey(p => new { p.codigoProducto })
                .Property(p => p.descripcion)
                .IsRequired();


            //modelBuilder.Entity<Inventario>()
            //    .ToTable("Inventario")
            //    .HasKey(i => new { i.codigoInventario })
            //    .Property(i => i.tipoRegistro)
            //    .IsRequired();
            //modelBuilder.Entity<Inventario>()
            //    .HasRequired(i => i.Productos)
            //    .WithRequiredPrincipal(i => i.Inventario);

            modelBuilder.Entity<Categoria>()
                .ToTable("Categoria")
                .HasKey(c => new { c.codigoCategoria })
                .Property(c => c.descripcion)
                .IsRequired();

            modelBuilder.Entity<TipoEmpaque>()
               .ToTable("TipoEmpaque")
               .HasKey(e => new { e.codigoEmpaque })
               .Property(e => e.descripcion)
               .IsRequired();

            modelBuilder.Entity<Clientes>()
                .ToTable("Clientes")
                .HasKey(cl => new { cl.nit })
                .Property(cl => cl.nombre)
                .IsRequired();

            modelBuilder.Entity<TelefonoCliente>()
                .ToTable("TelefonoCliente")
                .HasKey(t => new { t.codigoTelefono })
                .Property(t => t.numero)
                .IsRequired();

            modelBuilder.Entity<EmailCliente>()
                .ToTable("EmailCliente")
                .HasKey(e => new { e.codigoEmail })
                .Property(e => e.email)
                .IsRequired();

            //    modelBuilder.Entity<Factura>()
            //        .ToTable("Factura")
            //        .HasKey(f => new { f.numeroFactura })
            //        .Property(f => f.nit)
            //        .IsRequired();
            //    modelBuilder.Entity<Factura>()
            //        .HasRequired(f => f.Clientes)
            //        .WithRequiredPrincipal(f => f.Factura);
            //    modelBuilder.Entity<Factura>()
            //        .HasRequired(f => f.DetalleFactura)
            //        .WithRequiredPrincipal(f => f.Factura);

            //    modelBuilder.Entity<DetalleFactura>()
            //        .ToTable("DetalleFactura")
            //        .HasKey(d => new { d.codigoDetalle })
            //        .Property(d => d.codigoProducto)
            //        .IsRequired();

            modelBuilder.Entity<Proveedores>()
                .ToTable("Proveedores")
                .HasKey(p => new { p.codigoProveedor })
                .Property(p => p.contactoPrincipal)
                .IsRequired();
                //modelBuilder.Entity<Proveedores>()
                //    .HasRequired(p => p.TelefonoProveedor)
                //    .WithRequiredPrincipal(p => p.Proveedores);
                //modelBuilder.Entity<Proveedores>()
                //    .HasRequired(p => p.EmailProveedor)
                //    .WithRequiredPrincipal(p => p.Proveedores);

                modelBuilder.Entity<TelefonoProveedor>()
                    .ToTable("TelefonoProveedor")
                    .HasKey(t => new { t.codigoTelefono })
                    .Property(t => t.numero)
                    .IsRequired();

                modelBuilder.Entity<EmailProveedor>()
                    .ToTable("EmailProveedor")
                    .HasKey(e => new { e.codigoEmail })
                    .Property(e => e.email)
                    .IsRequired();

            //    modelBuilder.Entity<Compras>()
            //        .ToTable("Compras")
            //        .HasKey(c => new { c.idCompra })
            //        .Property(c => c.numeroDocumento)
            //        .IsRequired();
            //    modelBuilder.Entity<Compras>()
            //        .HasRequired(c => c.Proveedores)
            //        .WithRequiredPrincipal(c => c.Compras);
            //    modelBuilder.Entity<Compras>()
            //        .HasRequired(c => c.DetalleCompra)
            //        .WithRequiredPrincipal(c => c.Compras);

            //    modelBuilder.Entity<DetalleCompra>()
            //        .ToTable("DetalleCompra")
            //        .HasKey(d => new { d.idDetalle })
            //        .Property(d => d.codigoProducto)
            //        .IsRequired();
        }
    }
}
