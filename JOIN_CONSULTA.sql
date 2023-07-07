




use CL2





select c.Id,c.nombre, c.precio, c.fecha ,c.idTipo , t.tipo from Producto c inner join TipoProducto t
on c.idTipo = t.Id
            
           