using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Interfaces.Reglas;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private IProductoDA _productoDA;
        private IProductoReglas _productoReglas;

        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas)
        {
            _productoDA = productoDA;
            _productoReglas = productoReglas;
        }

        public Task<Guid> Actualizar(Guid Id, ProductoRequest producto)
        {
            return _productoDA.Editar(Id, producto);
        }

        public Task<Guid> Agregar(ProductoRequest producto)
        {
            return _productoDA.Agregar(producto);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _productoDA.Eliminar(Id);
        }

        public Task<IEnumerable<ProductoResponse>> Obtener()
        {
            return _productoDA.Obtener();
        }

        public async Task<ProductoDetalle> ObtenerPorID(Guid Id)
        {
            var producto = await _productoDA.ObtenerPorId(Id);

            if (producto == null)
                return null;

            // Calcular precio en USD
            producto.PrecioDolares = await _productoReglas.CalcularPrecioUSD(producto.Precio);

            return producto;
        }
    }
}
