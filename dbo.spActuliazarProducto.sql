CREATE PROCEDURE [dbo].[spActuliazarProducto]
   @id      CHAR(10),
   @nombre      VARCHAR(50),
   @precio   float,
   @fecha    date,
  @idTipo   int,
	@foto varchar(50)
AS
	update Producto set nombre=  @nombre, precio = @precio, 
	fecha = @fecha , idtipo = @idTipo , foto =  @foto  where  Id = @id