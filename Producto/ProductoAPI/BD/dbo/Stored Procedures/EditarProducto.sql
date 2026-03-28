-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- Editar producto
CREATE PROCEDURE EditarProducto
    @Id UNIQUEIDENTIFIER,
    @IdSubCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(MAX),
    @Descripcion VARCHAR(MAX),
    @Precio DECIMAL(18,2),
    @Stock INT,
    @CodigoBarras VARCHAR(MAX)
AS
BEGIN
    UPDATE Producto
    SET 
        IdSubCategoria = @IdSubCategoria,
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        Precio = @Precio,
        Stock = @Stock,
        CodigoBarras = @CodigoBarras
    WHERE Id = @Id
END