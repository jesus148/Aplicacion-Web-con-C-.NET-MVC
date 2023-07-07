CREATE PROCEDURE [dbo].[spCrearProducto]
   @id      CHAR(10),
   @nombre      VARCHAR(50),
   @precio   float,
   @fecha    date,
    @idTipo   int,
	@Foto  VARCHAR(50)
 AS  
 BEGIN 
      INSERT INTO Producto(Id,nombre,precio,fecha,idtipo , foto)
      VALUES(@id, @nombre,@precio , @fecha, @idTipo,@Foto );
   END