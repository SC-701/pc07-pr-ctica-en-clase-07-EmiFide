using Abstracciones.Modelos;


namespace Abstracciones.Interfaces.Flujo
{
    public interface IProductoFlujo
    {
        Task<IEnumerable<ProductoResponse>> Obtener();

        Task<ProductoDetalle> ObtenerPorID(Guid Id);

        Task<Guid> Agregar(ProductoRequest producto);

        Task<Guid> Actualizar(Guid Id, ProductoRequest producto);

        Task<Guid> Eliminar(Guid Id);
    }
}
