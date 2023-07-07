CREATE PROCEDURE [dbo].[spBorrarProducto]
	@id char(10)
AS
    Delete Producto
	where Id = @id