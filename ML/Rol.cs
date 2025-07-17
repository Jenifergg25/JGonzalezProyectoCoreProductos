using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        [Required(ErrorMessage = "El campo Rol es obligatorio, seleccione uno.")]
        [Range(1, 4, ErrorMessage = "Los roles disponibles van del 1 al 4")]
        public int IdRol { get; set; }
        public string? Nombre { get; set; }
        public List<object>? Roles { get; set; }
    }
}
