--Use DBProyectoFinal
--go
-----------------------------------------------------Categoria---------------------------------------------------
create procedure sp_AgregarCategoria(@descripcion varchar(128))
as
	Begin
		Insert into Categoria(descripcion) Values (@descripcion)
	End
go

create procedure sp_EliminarCategoria(@codigoCategoria int)
as
	Begin
		Delete From Categoria where codigoCategoria = @codigoCategoria
	End
go

create procedure sp_ActualizarCategoria(@codigoCategoria int, @descripcion varchar(128))
as
	Begin
		Update Categoria set descripcion = @descripcion where codigoCategoria = @codigoCategoria
	End
go

create procedure sp_ListarCategoria
as
	Begin
		Select * From Categoria
	End
go
---------------------------------------Tipo de Empaque------------------------------------------------------------------

create procedure sp_AgregarTipoEmpaque (@descripcion varchar(128))
as
	Begin
		Insert into TipoEmpaque(descripcion) Values (@descripcion)
	End
go

create procedure sp_EliminarTipoEmpaque(@codigoEmpaque int)
as
	Begin
		Delete from TipoEmpaque where codigoEmpaque = @codigoEmpaque
	End
go

create procedure sp_ActualizarTipoEmpaque(@codigoEmpaque int, @descripcion varchar(128))
as
	Begin
		Update TipoEmpaque set descripcion = @descripcion where codigoEmpaque = @codigoEmpaque
	End
go

create procedure sp_ListarTipoEmpaque
as
	Begin
		Select * from TipoEmpaque
	End
go
--------------------------------------------------Productos-----------------------------------------------------
create procedure sp_AgregarProductos(@codigoCategoria int, @codigoEmpaque int, @descripcion varchar(128), @precioUnitario decimal(10,2), @precioPorDocena decimal(10,2), @precioPorMayor decimal(10,2), @existencia int, @imagen varchar(128))
as
	Begin
		Insert into Productos(codigoCategoria,codigoEmpaque,descripcion,precioUnitario,precioPorDocena,precioPorMayor,existencia,imagen) Values(@codigoCategoria, @codigoEmpaque, @descripcion, @precioUnitario, @precioPorDocena, @precioPorMayor, @existencia, @imagen)
	End
go

create procedure sp_EliminarProducto(@codigoProducto int)
as
	Begin
		Delete from Productos where codigoProducto = @codigoProducto
	End
go

create procedure sp_ActualizarProducto(@codigoProducto int, @codigoCategoria int, @codigoEmpaque int, @descripcion varchar(128), @precioUnitario decimal(10,2), @precioPorDocena decimal(10,2), @precioPorMayor decimal(10,2), @existencia int, @imagen varchar(128))
as
	Begin
		Update Productos set codigoCategoria = @codigoCategoria, codigoEmpaque = @codigoEmpaque, descripcion = @descripcion, precioUnitario = @precioUnitario, precioPorDocena = @precioPorDocena, precioPorMayor = @precioPorMayor, existencia = @existencia, imagen = @imagen where codigoProducto = @codigoProducto
	End
go

create procedure sp_ListarProductos
as
	Begin
		Select * from Productos
	End
go
----------------------------------------------------------Inventario-----------------------------------------------
create procedure sp_AgregarInventario (@codigoProducto int, @fecha date, @tipoRegistro varchar(1), @precio decimal(10,2), @entradas int, @salidas int)
as
	Begin
		Insert into Inventario (codigoProducto, fecha, tipoRegistro, precio, entradas, salidas) Values (@codigoProducto, @fecha, @tipoRegistro, @precio, @entradas, @salidas)
	End
go

create procedure sp_EliminarInventario (@codigoInventario int)
as
	Begin
		Delete from Inventario where codigoInventario = @codigoInventario
	End
go

create procedure sp_UpdateInventario (@codigoInventario int, @codigoProducto int, @fecha date, @tipoRegistro varchar(1), @precio decimal(10,2), @entradas int, @salidas int)
as
	Begin
		Update Inventario set codigoProducto = @codigoProducto, fecha = @fecha, tipoRegistro = @tipoRegistro, precio = @precio, entradas = @entradas, salidas = @salidas where codigoInventario = @codigoInventario
	End
go

create procedure sp_ListarInventario
as
	Begin
		Select * from Inventario
	End
go

-------------------------------------------------------Proveedores-----------------------------------------------
create procedure sp_AgregarProveedores (@nit varchar(64), @razonSocial varchar(128), @direccion varchar(128), @paginaWeb varchar(64), @contactoPrincipal varchar(64))
as
	Begin
		Insert into Proveedores (nit, razonSocial, direccion, paginaWeb, contactoPrincipal) Values (@nit, @razonSocial, @direccion, @paginaWeb, @contactoPrincipal)
	End
go

create procedure sp_EliminarProveedores (@codigoProveedor int)
as
	Begin
		Delete from Proveedores where codigoProveedor = @codigoProveedor
	End
GO

create procedure sp_ActualizarProveedores (@codigoProveedor int, @nit varchar(64), @razonSocial varchar(128), @direccion varchar(128), @paginaWeb varchar(64), @contactoPrincipal varchar(64))
as
	Begin
		Update Proveedores set nit = @nit, razonSocial = @razonSocial, direccion = @direccion, contactoPrincipal = @contactoPrincipal where codigoProveedor = @codigoProveedor
	End
go

create procedure sp_ListarProveedores 
as
	Begin
		Select * from Proveedores
	End
go

------------------------------------------------------------TelefonoProveedor--------------------------------------
create procedure sp_AgregarTelefonoProveedor (@numero varchar(32), @descripcion varchar(64), @codigoProveedor int)
as
	Begin
		Insert into TelefonoProveedor (numero, descripcion, codigoProveedor) Values (@numero, @descripcion, @codigoProveedor)
	End
go

create procedure sp_EliminarTelefonoProveedor (@codigoTelefono int)
as
	Begin
		Delete from TelefonoProveedor where codigoTelefono = @codigoTelefono
	End
go

create procedure sp_ActualizarTelefonoProveedor (@codigoTelefono int, @numero varchar(32), @descripcion varchar(64), @codigoProveedor int)
as
	Begin
		Update TelefonoProveedor set numero = @numero, descripcion = @descripcion, codigoProveedor = @codigoProveedor where codigoTelefono = @codigoTelefono
	End
go

create procedure sp_ListarTelefonoProveedor
as
	Begin
		Select * from TelefonoProveedor
	End
go

----------------------------------------------------------EmailProveedor----------------------------------------
create procedure sp_AgregarEmailProveedor (@email varchar(64), @codigoProveedor int)
as
	Begin
		Insert into EmailProveedor (email, codigoProveedor) Values (@email, @codigoProveedor)
	End
go

create procedure sp_EliminarEmailProveedor (@codigoEmail int)
as
	Begin
		Delete from EmailProveedor where codigoEmail = @codigoEmail
	End
go

create procedure sp_ActualizarEmailProveedor (@codigoEmail int, @email varchar(64), @codigoProveedor int)
as
	Begin
		Update EmailProveedor set email = @email, codigoProveedor = @codigoProveedor where codigoEmail = @codigoEmail
	End
go

create procedure sp_ListarEmailProveedor
as
	Begin
		Select * from EmailProveedor
	End
go

------------------------------------------------------------Clientes---------------------------------------------
create procedure sp_AgregarClientes(@nit varchar(64), @DPI varchar(64), @nombre varchar(128), @direccion varchar(128))
as
	Begin
		Insert into Clientes (nit, DPI, nombre, direccion) Values (@nit, @DPI, @nombre, @direccion)
	End
GO

create procedure sp_EliminarClientes (@nit varchar(64))
as
	Begin
		Delete from Clientes where nit = @nit
	End
go

create procedure sp_ActualizarClientes (@nit varchar(64), @DPI varchar(64), @nombre varchar(128), @direccion varchar(128))
as
	Begin
		Update Clientes set DPI = @DPI, nombre = @nombre, direccion = @direccion where nit = @nit
	End
go

create procedure sp_ListarClientes
as
	Begin
		Select * from Clientes
	End
go

----------------------------------------------------EmailClientes-------------------------------------------------
create procedure sp_AgregarEmailClientes (@email varchar(128), @nit varchar(64))
as
	Begin
		Insert into EmailCliente (email, nit) Values (@email, @nit)
	End
go

create procedure sp_EliminarEmailClientes (@codigoEmail int)
as
	Begin
		Delete from EmailCliente where codigoEmail = @codigoEmail
	End
go

create procedure sp_ActualizarEmailClientes (@codigoEmail int, @email varchar(128), @nit varchar(64))
as
	Begin
		Update EmailCliente set email = @email, nit = @nit where codigoEmail = @codigoEmail
	End
go

create procedure sp_ListarEmailClientes
as
	Begin
		Select * from EmailCliente
	End
go

-------------------------------------------------TelefonoClientes----------------------------------------------
create procedure sp_AgregarTelefonoClientes(@numero varchar(32), @descripcion varchar(128), @nit varchar(64))
as
	Begin
		Insert into TelefonoCliente (numero, descripcion, nit) Values (@numero, @descripcion, @nit)
	End
go

create procedure sp_EliminarTelefonoClientes (@codigoTelefono int)
as
	Begin
		Delete from TelefonoCliente where codigoTelefono = @codigoTelefono
	End
go

create procedure sp_ActualizarTelefonoClientes (@codigoTelefono int, @numero varchar(32), @descripcion varchar(128), @nit varchar(64))
as
	Begin
		Update TelefonoCliente set numero = @numero, descripcion = @descripcion, nit = @nit where codigoTelefono = @codigoTelefono
	End
go

create procedure sp_ListarTelefonoClientes
as
	Begin
		Select * from TelefonoCliente
	End
go

----------------------------------------------Compras----------------------------------------------------
create procedure sp_AgregarCompras (@numeroDocumento int, @codigoProveedor int, @fecha date, @total decimal(10,2))
as
	Begin
		Insert into Compras (numeroDocumento, codigoProveedor, fecha, total) Values (@numeroDocumento, @codigoProveedor, @fecha, @total)
	End
go

create procedure sp_EliminarCompras (@idCompra int)
as
	Begin
		Delete from Compras where idCompra = @idCompra
	End
go

create procedure sp_ActualizarCompras (@idCompras int, @numeroDocumento int, @codigoProveedor int, @fecha date, @total decimal(10,2))
as
	Begin
		Update Compras set numeroDocumento = @numeroDocumento, codigoProveedor = @codigoProveedor, fecha = @fecha, total = @total where idCompra = @idCompras
	End

go
create procedure sp_ListarCompras
as
	Begin
		Select * from Compras
	End
go

--------------------------------------------------DetalleCompras-------------------------------------------
create procedure sp_AgregarDetalleCompra (@idCompra int, @codigoProducto int, @cantidad int, @precio decimal(10,2))
as
	Begin
		Insert into DetalleCompra (idCompra, codigoProducto, cantidad, precio) Values (@idCompra, @codigoProducto, @cantidad, @precio)
	End
go

create procedure sp_EliminarDetalleCompra (@idDetalle int)
as
	Begin
		Delete from DetalleCompra where idDetalle = @idDetalle
	End
go

create procedure sp_ActualizarDetalleCompra (@idDetalle int, @idCompra int, @codigoProducto int, @cantidad int, @precio decimal(10,2))
as
	Begin
		Update DetalleCompra set idCompra = @idCompra, codigoProducto = @codigoProducto, cantidad = @cantidad, precio = @precio where idDetalle = @idDetalle
	End
go

create procedure sp_ListarDetalleCompra
as
	Begin
		Select * from DetalleCompra
	End
go

-------------------------------------------------Factura-----------------------------------------------------
create procedure sp_AgregarFactura (@nit varchar(64),@fecha date, @total decimal(10,2))
as
	Begin
		Insert into Factura (nit, fecha, total) Values (@nit, @fecha, @total)
	End
go

create procedure sp_EliminarFactura (@numeroFactura int)
as
	Begin
		Delete from Factura where numeroFactura = @numeroFactura
	End
go

create procedure sp_ActualizarFactura (@numeroFactura int, @nit varchar(64), @fecha date, @total decimal(10,2))
as
	Begin
		Update Factura set nit = @nit, fecha = @fecha, total = @total where numeroFactura = @numeroFactura
	End
go

create procedure sp_ListarFactura
as
	Begin
		Select * from Factura
	End
go

------------------------------------------------DetalleFactura-----------------------------------------------
create procedure sp_AgregarDetalleFactura (@numeroFactura int, @codigoProducto int,	@cantidad int, @precio decimal(10,2), @descuento decimal(10,2))
as
	Begin
		Insert into DetalleFactura (numeroFactura, codigoProducto, cantidad,precio, descuento) Values (@numeroFactura, @codigoProducto, @cantidad, @precio, @descuento)
	End
go

create procedure sp_EliminarDetalleFactura(@codigoDetalle int)
as
	Begin
		Delete from DetalleFactura where codigoDetalle = @codigoDetalle
	End
go

create procedure sp_ActualizarDetalleFactura (@codigoDetalle int, @numeroFactura int, @codigoProducto int,	@cantidad int, @precio decimal(10,2), @descuento decimal(10,2))
as
	Begin
		Update DetalleFactura set numeroFactura = @numeroFactura, codigoProducto = @codigoProducto, cantidad = @cantidad, precio = @precio, descuento = @descuento where codigoDetalle = @codigoDetalle
	End
go

create procedure sp_ListarDetalleFactura
as
	Begin
		Select * from DetalleFactura
	End
go