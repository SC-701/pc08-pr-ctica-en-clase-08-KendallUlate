-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- Eliminar producto
CREATE PROCEDURE EliminarProducto
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Producto
    WHERE Id = @Id
END