--Create database DBProyectoFinal
--go
--Use DBProyectoFinal
--go

Create table Categoria
(
	codigoCategoria int not null primary key identity(1,1),
	descripcion varchar(128) not null
)
go

Create table TipoEmpaque
(
	codigoEmpaque int not null primary key identity(1,1),
	descripcion varchar(128) not null
)

Create table Productos
(
	codigoProducto int not null primary key identity(1,1),
	codigoCategoria int not null,
	codigoEmpaque int not null,
	descripcion varchar(128) not null,
	precioUnitario decimal(10,2) not null,
	precioPorDocena decimal(10,2) not null,
	precioPorMayor decimal(10,2) not null,
	existencia int not null,
	imagen varchar(128),
	Constraint FK_Producto_Categoria foreign key (codigoCategoria) references Categoria(codigoCategoria),
	Constraint FK_Producto_TipoEmpaque foreign key (codigoEmpaque) references TipoEmpaque(codigoEmpaque)
)
go

Create table Inventario
(
	codigoInventario int not null primary key identity(1,1),
	codigoProducto int not null,
	fecha date not null,
	tipoRegistro varchar(1) not null,
	precio decimal(10,2) not null,
	entradas int not null,
	salidas int not null,
	Constraint FK_Inventario_Productos foreign key (codigoProducto) references Productos(codigoProducto)

)
go

Create table Proveedores
(
	codigoProveedor int not null primary key identity(1,1),
	nit varchar(64) not null,
	razonSocial varchar(128) not null,
	direccion varchar(128) not null,
	paginaWeb varchar(64),
	contactoPrincipal varchar(64)
)
go

Create table TelefonoProveedor
(
	codigoTelefono int not null primary key identity(1,1),
	numero varchar(32) not null,
	descripcion varchar(64) not null,
	codigoProveedor int not null,
	constraint FK_TelefonoProveedor_Proveedores foreign key (codigoProveedor) references Proveedores(codigoProveedor)
)
go

Create table EmailProveedor
(
	codigoEmail int not null primary key identity(1,1),
	email varchar(64) not null,
	codigoProveedor int not null,
	Constraint FK_EmailProveedor_Proveedores foreign key (codigoProveedor) references Proveedores(codigoProveedor)
)
go

Create table Clientes
(
	nit varchar(64) not null primary key,
	DPI varchar(64) not null,
	nombre varchar(128) not null,
	direccion varchar(128) not null
)
go

Create table EmailCliente
(
	codigoEmail int not null primary key identity(1,1),
	email varchar(128) not null,
	nit varchar(64) not null,
	Constraint FK_EmailCliente_Clientes foreign key (nit) references Clientes(nit)
)
go

Create table TelefonoCliente
(
	codigoTelefono int not null primary key identity(1,1),
	numero varchar(32) not null,
	descripcion varchar(128) not null,
	nit varchar(64) not null,
	Constraint FK_TelefonoCliente_Clientes foreign key (nit) references Clientes(nit)
)
go

Create table Compras
(
	idCompra int not null primary key identity(1,1),
	numeroDocumento int not null,
	codigoProveedor int not null,
	fecha date not null,
	total decimal(10,2) not null,
	Constraint FK_Compras_Proveedores foreign key (codigoProveedor) references Proveedores(codigoProveedor)
)
go

Create table DetalleCompra
(
	idDetalle int not null primary key identity(1,1),
	idCompra int not null,
	codigoProducto int not null,
	cantidad int not null,
	precio decimal(10,2) not null,
	Constraint FK_DetalleCompra_Compras foreign key (idCompra) references Compras(idCompra),
	Constraint FK_DetalleCompra_Productos foreign key (codigoProducto) references Productos(codigoProducto)
)
go

Create table Factura
(
	numeroFactura int not null primary key identity(1,1),
	nit varchar(64) not null,
	fecha date not null,
	total decimal(10,2) not null,
	Constraint FK_Factura_Clientes foreign key (nit) references Clientes(nit)
)
go

Create table DetalleFactura
(
	codigoDetalle int not null primary key identity(1,1),
	numeroFactura int not null,
	codigoProducto int not null,
	cantidad int not null,
	precio decimal(10,2) not null,
	descuento decimal(10,2),
	Constraint FK_DetalleFactura_Factura foreign key (numeroFactura) references Factura(numeroFactura),
	Constraint FK_DetalleFactura_Productos foreign key (codigoProducto) references Productos(codigoProducto)
)







