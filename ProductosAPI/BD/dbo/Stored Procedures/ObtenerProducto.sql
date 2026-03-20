CREATE PROCEDURE [dbo].[ObtenerProducto]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Producto.Id,
        Producto.Nombre,
        Producto.Descripcion,
        Producto.Precio,
        Producto.Stock,
        Producto.CodigoBarras,
        SubCategorias.Nombre AS SubCategoria,
        Categorias.Nombre AS Categoria
    FROM Categorias
    INNER JOIN SubCategorias ON Categorias.Id = SubCategorias.IdCategoria
    INNER JOIN Producto ON SubCategorias.Id = Producto.IdSubCategoria
    WHERE Producto.Id = @Id;
END