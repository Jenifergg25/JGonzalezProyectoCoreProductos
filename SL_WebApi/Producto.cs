using System;
using System.Collections.Generic;

namespace SL_WebApi;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public int? IdSubCategoria { get; set; }

    public virtual SubCategorium? IdSubCategoriaNavigation { get; set; }
}
