using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad nombre es obligatoria")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad descripción es requerida")]
        [StringLength(300, ErrorMessage = "La extensión de descripción debe ser " +
            "mayor a 10 y menor a 300 caracteres", MinimumLength = 10)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        public Decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [RegularExpression(@"^\d+$",
            ErrorMessage = "El código de barras solo debe contener números")]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        [Required(ErrorMessage = "La subcategoría es obligatoria")]
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string SubCategoria { get; set; }

        [StringLength(100)]
        public string Categoria { get; set; }
    }

    public class ProductoDetalle : ProductoResponse
    {
        public Decimal PrecioDolares { get; set; }
    }
}
