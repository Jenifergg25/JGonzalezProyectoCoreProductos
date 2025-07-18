using System;
using System.Collections.Generic;

namespace DL;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<SubCategorium> SubCategoria { get; set; } = new List<SubCategorium>();
}
