CREATE PROCEDURE [dbo].[AgregarProducto]
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

    INSERT INTO [dbo].[Producto]
        ([Id],
         [Nombre],
         [Descripcion],
         [Precio],
         [Stock],
         [CodigoBarras],
         [IdSubCategoria])
    VALUES
        (@Id,
         @Nombre,
         @Descripcion,
         @Precio,
         @Stock,
         @CodigoBarras,
         @IdSubCategoria);

    SELECT @Id;

    COMMIT TRANSACTION;
END