using System;
using System.Collections.Generic;

namespace SL_WebApi;

public partial class SubCategorium
{
    public int IdSubCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdCategoria { get; set; }

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
