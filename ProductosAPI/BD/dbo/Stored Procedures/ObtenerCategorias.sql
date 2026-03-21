CREATE PROCEDURE [dbo].[ObtenerCategorias]
AS
BEGIN
    -- Evita conjuntos de resultados adicionales
    SET NOCOUNT ON;

    -- Selección de categorías filtradas por Id
    SELECT Id, Nombre
    FROM Categorias
END;