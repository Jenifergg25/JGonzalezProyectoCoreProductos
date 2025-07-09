using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsuarioGetAllView
    {
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string? ApellidoMaterno { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Sexo { get; set; } = null!;

        public string? Celular { get; set; }

        public string? FechaNacimiento { get; set; }

        public string? Curp { get; set; }

        public int IdRol { get; set; }

        public string? Rol { get; set; }

        public int? IdDireccion { get; set; }

        public string? Calle { get; set; }

        public string? NumeroInterior { get; set; }

        public string? NumeroExterior { get; set; }

        public int? IdColonia { get; set; }

        public string? Colonia { get; set; }

        public string? CodigoPostal { get; set; }

        public int? IdMunicipio { get; set; }

        public string? Municipio { get; set; }

        public int? IdEstado { get; set; }

        public string? Estado { get; set; }

        public bool Status { get; set; }

        public byte[]? Imagen { get; set; }
    }
}
