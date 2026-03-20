CREATE PROCEDURE [dbo].[EditarProducto]
    @Id UNIQUEIDENTIFIER,
    @Nombre VARCHAR(200),
    @Descripcion VARCHAR(300),
    @Precio DECIMAL(18,2),
    @Stock INT,
    @CodigoBarras VARCHAR(50),
    @IdSubCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    UPDATE [dbo].[Producto]
       SET [Nombre] = @Nombre,
           [Descripcion] = @Descripcion,
           [Precio] = @Precio,
           [Stock] = @Stock,
           [CodigoBarras] = @CodigoBarras,
           [IdSubCategoria] = @IdSubCategoria
     WHERE Id = @Id;

    SELECT @Id;

    COMMIT TRANSACTION;
END