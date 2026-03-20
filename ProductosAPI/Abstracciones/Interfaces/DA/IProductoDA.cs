using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;


namespace Abstracciones.Interfaces.DA
{
    public interface IProductoDA
    {

        Task<IEnumerable<ProductoResponse>> Obtener();

        Task<ProductoDetalle> ObtenerPorId(Guid id);

        Task<Guid> Agregar(ProductoRequest request);

        Task<Guid> Editar(Guid id, ProductoRequest request);

        Task<Guid> Eliminar(Guid id);



    }
}
