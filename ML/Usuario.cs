using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario //CLASE Usuario en ML
    {
        public int IdUsuario { get; set; }//ATRIBUTO IdUsuario, esto permitira leer o asignar un valor a IdUsuario
        public string? UserName { get; set; }//ATRIBUTO UserName ´´´
        public string? Nombre { get; set; }//ATRIBUTO Nombre ´´´
        public string? ApellidoPaterno { get; set; }//ATRIBUTO ApellidoPaterno ´´´
        public string? ApellidoMaterno { get; set; }//ATRIBUTO ApellidoMaterno ´´´
        public string? Email { get; set; }//ATRIBUTO Email ´´´
        public string? Password { get; set; }//ATRIBUTO Password ´´´
        public string? Sexo { get; set; }//ATRIBUTO Sexo ´´´
        public string? Telefono { get; set; }//ATRIBUTO Telefono ´´´
        public string? Celular { get; set; }//ATRIBUTO Celular ´´´
        public string? FechaNacimiento { get; set; }//ATRIBUTO FechaNacimiento ´´´
        public string? CURP { get; set; }//ATRIBUTO CURP ´´´
        public bool Status { get; set; }
        public byte[]? Imagen { get; set; }
        public string? ImagenBase64 { get; set; }
        public List<object>? Usuarios { get; set; }
        public List<string> Errores { get; set; } = new List<string>();
        public ML.Rol? Rol { get; set; }//Modelo Rol (IdRol)
        public ML.Direccion? Direccion { get; set; }//Modelo Direccion(IdDireccion)
    }
}
