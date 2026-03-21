
CREATE PROCEDURE [dbo].[ObtenerSubCategorias]
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre
    FROM SubCategorias
    WHERE IdCategoria = @IdCategoria;
END;