using System;
using System.Collections.Generic;

namespace DL;

public partial class ProductoView
{
    public int IdProducto { get; set; }

    public string Producto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public int? IdSubCategoria { get; set; }

    public string Subcategoria { get; set; } = null!;

    public int? IdCategoria { get; set; }

    public string Categoria { get; set; } = null!;
}
