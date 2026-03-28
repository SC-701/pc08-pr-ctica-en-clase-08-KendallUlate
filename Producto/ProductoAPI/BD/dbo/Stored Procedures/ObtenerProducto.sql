-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- Obtener un producto por Id
CREATE PROCEDURE ObtenerProducto
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM Producto
    WHERE Id = @Id
END