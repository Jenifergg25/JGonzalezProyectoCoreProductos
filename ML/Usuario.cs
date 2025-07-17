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
        [Required(ErrorMessage = "El campo Username es obligatorio.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El username debe tener entre 4 y 50 caracteres.")]
        [RegularExpression("^[a-zA-Z0-9_.@#$%-]*$", ErrorMessage = "El username solo permite letras, números y los caracteres _ . @ # $ % -")]
        public string? UserName { get; set; }//ATRIBUTO UserName ´´´


        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 a 50 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo permite letras y espacios")]
        public string? Nombre { get; set; }//ATRIBUTO Nombre ´´´


        [Required(ErrorMessage = "El campo Apellido paterno es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido paterno debe tener entre 3 a 50 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El apellido paterno solo permite letras y espacios")]
        public string? ApellidoPaterno { get; set; }//ATRIBUTO ApellidoPaterno ´´´


        [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido materno debe tener entre 3 a 50 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El apellido materno solo permite letras y espacios")]
        public string? ApellidoMaterno { get; set; }//ATRIBUTO ApellidoMaterno ´´´


        [Required(ErrorMessage = "El correo es obligatorio.")]
        [StringLength(250, ErrorMessage = "El correo no puede exceder los 250 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del correo no es válido.")]
        public string? Email { get; set; }//ATRIBUTO Email ´´´

        //poner minimo de caracteres
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres.")]
        public string? Password { get; set; }//ATRIBUTO Password ´´´


        [Required(ErrorMessage = "El campo sexo es obligatorio.")]
        [RegularExpression(@"^[FMO]$", ErrorMessage = "El sexo debe ser F, M u O.")]
        public string? Sexo { get; set; }//ATRIBUTO Sexo ´´´


        [Required(ErrorMessage = "El número de telefono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de telefono debe tener exactamente 10 dígitos del 0 al 9.")]
        public string? Telefono { get; set; }//ATRIBUTO Telefono ´´´


        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número celular debe tener exactamente 10 dígitos del 0 al 9.")]
        public string? Celular { get; set; }//ATRIBUTO Celular ´´´


        //public DateTime? FechaNacimiento { get; set; }//ATRIBUTO FechaNacimiento ´´´
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$", ErrorMessage = "La fecha debe tener el formato dd/mm/yyyy.")]
        public string? FechaNacimiento { get; set; }//ATRIBUTO FechaNacimiento ´´´


        [RegularExpression(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[0-9A-Z]{2}$", ErrorMessage = "La CURP no tiene un formato válido.")]
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
