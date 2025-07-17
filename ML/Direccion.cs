using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [Required(ErrorMessage = "El campo Calle es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9ÁÉÍÓÚáéíóúÑñ\s.,-]+$", ErrorMessage = "La calle debe tener números, letras y . , o -")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El numero exterior debe tener entre 3 a 50 caracteres")]
        public string? Calle { get; set; }


        [Required(ErrorMessage = "El campo Numero exterior es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "El número exterior solo letras y números")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "El número exterior debe tener entre 1 a 20 caracteres")]
        public string? NumeroExterior { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "El número interior solo letras y números")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "El numero interior debe tener entre 1 a 20 caracteres")]
        public string? NumeroInterior { get; set; }


        public ML.Colonia? Colonia { get; set; } //Modelo Colonia (IdColonia)
    }
}
