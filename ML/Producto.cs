using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El nombre debe tener entre 4 y 100 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo se permiten letras y espacios")]
        public string? Nombre { get; set; }
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Ingrese un precio válido (ej: 8 o 8.50)")]
        public decimal Precio { get; set; }
        public byte[]? Imagen { get; set; }
        public List<object>? Productos { get; set; }
        public ML.SubCategoria? SubCategoria { get; set; }
    }
}
