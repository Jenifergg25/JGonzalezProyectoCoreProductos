using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        private readonly JgonzalezProgramacionNcapasContext _context;

        public Usuario(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetAll(ML.Usuario usuarioParametros)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                var parametros = new[]
                                {
                    new SqlParameter("@Nombre", usuarioParametros.Nombre),
                    new SqlParameter("@ApellidoPaterno", usuarioParametros.ApellidoPaterno),
                    new SqlParameter("@ApellidoMaterno", usuarioParametros.ApellidoMaterno),
                    new SqlParameter("@IdRol", usuarioParametros.Rol?.IdRol)
                };
                var query = _context.UsuarioGetAllViews.FromSqlRaw("UsuarioGetAllView @Nombre, @ApellidoPaterno, @ApellidoMaterno, @IdRol", parametros).ToList(); 
                if (query.Count > 0)
                {
                    foreach (var itemUsuario in query)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = itemUsuario.IdUsuario;
                        usuario.Nombre = itemUsuario.Nombre ?? "";
                        usuario.Telefono = itemUsuario.Telefono ?? "";
                        usuario.UserName = itemUsuario.UserName ?? "";
                        usuario.ApellidoPaterno = itemUsuario.ApellidoPaterno ?? "";
                        usuario.ApellidoMaterno = itemUsuario.ApellidoMaterno ?? "";
                        usuario.Email = itemUsuario.Email ?? "";
                        usuario.Password = itemUsuario.Password ?? "";
                        usuario.Sexo = itemUsuario.Sexo ?? "";
                        usuario.Celular = itemUsuario.Celular ?? "";
                        usuario.FechaNacimiento = itemUsuario.FechaNacimiento;
                        usuario.CURP = itemUsuario.Curp ?? "";
                        usuario.Rol.IdRol = (int)itemUsuario.IdRol;
                        usuario.Rol.Nombre = itemUsuario.Rol ?? "";
                        usuario.Status = (bool)itemUsuario.Status;
                        usuario.Imagen = itemUsuario.Imagen;
                        //usuario.ImagenBase64 = Convert.ToBase64String(itemUsuario.Imagen);
                        usuario.Direccion.IdDireccion = itemUsuario.IdDireccion.HasValue ? itemUsuario.IdDireccion.Value : 0;
                        usuario.Direccion.Calle = itemUsuario.Calle ?? "";
                        usuario.Direccion.NumeroInterior = itemUsuario.NumeroInterior ?? "";
                        usuario.Direccion.NumeroExterior = itemUsuario.NumeroExterior ?? "";

                        usuario.Direccion.Colonia.IdColonia = itemUsuario.IdColonia.HasValue ? itemUsuario.IdColonia.Value : 0;
                        usuario.Direccion.Colonia.Nombre = itemUsuario.Colonia ?? "";
                        usuario.Direccion.Colonia.CodigoPostal = itemUsuario.CodigoPostal ?? "";

                        usuario.Direccion.Colonia.Municipio.IdMunicipio = itemUsuario.IdMunicipio.HasValue ? itemUsuario.IdMunicipio.Value : 0;
                        usuario.Direccion.Colonia.Municipio.Nombre = itemUsuario.Municipio ?? "";

                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = itemUsuario.IdEstado.HasValue ? itemUsuario.IdEstado.Value : 0;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = itemUsuario.Estado ?? "";

                        result.Objects.Add(usuario);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "Sin resultados";
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result GetByIdEFSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
{
                    new SqlParameter("@IdUsuario",IdUsuario)
                };
                var query = _context.UsuarioGetAllViews.FromSqlRaw("UsuarioGetById @IdUsuario", parametros).AsEnumerable().FirstOrDefault();
                if (query != null)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();


                    usuario.IdUsuario = query.IdUsuario;
                    usuario.Nombre = query.Nombre ?? "";
                    usuario.Telefono = query.Telefono ?? "";
                    usuario.UserName = query.UserName ?? "";
                    usuario.ApellidoPaterno = query.ApellidoPaterno ?? "";
                    usuario.ApellidoMaterno = query.ApellidoMaterno ?? "";
                    usuario.Password = query.Password ?? "";
                    usuario.Email = query.Email ?? "";
                    usuario.Sexo = query.Sexo ?? "";
                    usuario.Celular = query.Celular ?? "";
                    usuario.FechaNacimiento = query.FechaNacimiento ?? "";
                    usuario.CURP = query.Curp ?? "";
                    usuario.Rol.IdRol = query.IdRol;
                    //usuario.Rol.IdRol = (int)itemUsuario.IdRol;
                    usuario.Rol.Nombre = query.Rol ?? "";
                    usuario.Status = (bool)query.Status;
                    usuario.Imagen = query.Imagen;
                    usuario.Direccion.IdDireccion = query.IdDireccion.HasValue ? query.IdDireccion.Value : 0;
                    usuario.Direccion.Calle = query.Calle ?? "";
                    usuario.Direccion.NumeroInterior = query.NumeroInterior ?? "";
                    usuario.Direccion.NumeroExterior = query.NumeroExterior ?? "";

                    usuario.Direccion.Colonia.IdColonia = query.IdColonia.HasValue ? query.IdColonia.Value : 0;
                    usuario.Direccion.Colonia.Nombre = query.Colonia ?? "";
                    usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal ?? "";

                    usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio.HasValue ? query.IdMunicipio.Value : 0;
                    usuario.Direccion.Colonia.Municipio.Nombre = query.Municipio ?? "";

                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado.HasValue ? query.IdEstado.Value : 0;
                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.Estado ?? "";

                    //boxing de todos los datos del usuario
                    result.Object = usuario;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Usuario no encontrado.";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result AddEFSP(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
                {
            new SqlParameter("@Nombre", Usuario.Nombre ?? (object)DBNull.Value),
            new SqlParameter("@Telefono", Usuario.Telefono ?? (object)DBNull.Value),
            new SqlParameter("@UserName", Usuario.UserName ?? (object)DBNull.Value),
            new SqlParameter("@ApellidoPaterno", Usuario.ApellidoPaterno ?? (object)DBNull.Value),
            new SqlParameter("@ApellidoMaterno", Usuario.ApellidoMaterno ?? (object)DBNull.Value),
            new SqlParameter("@Email", Usuario.Email ?? (object)DBNull.Value),
            new SqlParameter("@Password", Usuario.Password ?? (object)DBNull.Value),
            new SqlParameter("@Sexo", Usuario.Sexo ?? (object)DBNull.Value),
            new SqlParameter("@Celular", Usuario.Celular ?? (object)DBNull.Value),
            new SqlParameter("@FechaNacimiento", Usuario.FechaNacimiento ?? (object)DBNull.Value),
            new SqlParameter("@CURP", Usuario.CURP ?? (object)DBNull.Value),
            new SqlParameter("@IdRol", Usuario.Rol.IdRol),
            new SqlParameter("@Calle", Usuario.Direccion.Calle ?? (object)DBNull.Value),
            new SqlParameter("@NumeroInterior", Usuario.Direccion.NumeroInterior ?? (object)DBNull.Value),
            new SqlParameter("@NumeroExterior", Usuario.Direccion.NumeroExterior ?? (object)DBNull.Value),
            new SqlParameter("@IdColonia", (object?)Usuario.Direccion.Colonia.IdColonia ?? DBNull.Value),
            new SqlParameter
            {
                ParameterName = "@Imagen",
                SqlDbType = SqlDbType.VarBinary,
                Value = Usuario.Imagen != null ? (object)Usuario.Imagen : DBNull.Value
            }

        };

                var query = _context.Database.ExecuteSqlRaw(
                    "UsuarioAdd @Nombre, @Telefono, @UserName, @ApellidoPaterno, @ApellidoMaterno, " +
                    "@Email, @Password, @Sexo, @Celular, @FechaNacimiento, @CURP, @IdRol, " +
                    "@Calle, @NumeroInterior, @NumeroExterior, @IdColonia, @Imagen",
                    parametros);

                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Error al insertar";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public ML.Result UpdateEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var parametros = new[]
                {
            new SqlParameter("@IdUsuario", usuario.IdUsuario),
            new SqlParameter("@Nombre", usuario.Nombre ?? (object)DBNull.Value),
            new SqlParameter("@Telefono", usuario.Telefono ?? (object)DBNull.Value),
            new SqlParameter("@UserName", usuario.UserName ?? (object)DBNull.Value),
            new SqlParameter("@ApellidoPaterno", usuario.ApellidoPaterno ?? (object)DBNull.Value),
            new SqlParameter("@ApellidoMaterno", usuario.ApellidoMaterno ?? (object)DBNull.Value),
            new SqlParameter("@Email", usuario.Email ?? (object)DBNull.Value),
            new SqlParameter("@Password", usuario.Password ?? (object)DBNull.Value),
            new SqlParameter("@Sexo", usuario.Sexo ?? (object)DBNull.Value),
            new SqlParameter("@Celular", usuario.Celular ?? (object)DBNull.Value),
            new SqlParameter("@FechaNacimiento", usuario.FechaNacimiento ?? (object)DBNull.Value),
            new SqlParameter("@CURP", usuario.CURP ?? (object)DBNull.Value),
            new SqlParameter("@IdRol", usuario.Rol.IdRol),
            new SqlParameter("@Calle", usuario.Direccion.Calle ?? (object)DBNull.Value),
            new SqlParameter("@NumeroInterior", usuario.Direccion.NumeroInterior ?? (object)DBNull.Value),
            new SqlParameter("@NumeroExterior", usuario.Direccion.NumeroExterior ?? (object)DBNull.Value),
            new SqlParameter("@IdColonia", (object?)usuario.Direccion.Colonia.IdColonia ?? DBNull.Value),
            new SqlParameter
            {
                ParameterName = "@Imagen",
                SqlDbType = SqlDbType.VarBinary,
                Value = usuario.Imagen != null ? (object)usuario.Imagen : DBNull.Value
            }
        };

                var query = _context.Database.ExecuteSqlRaw(
                    "UsuarioUpdate @IdUsuario, @Nombre, @Telefono, @UserName, @ApellidoPaterno, @ApellidoMaterno, " +
                    "@Email, @Password, @Sexo, @Celular, @FechaNacimiento, @CURP, @IdRol, @Calle, @NumeroInterior, " +
                    "@NumeroExterior, @IdColonia, @Imagen",
                    parametros
                );

                result.Correct = query > 0;

                if (!result.Correct)
                {
                    result.ErrorMessage = "Error al actualizar el usuario";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result DeleteEFSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
                {
                    new SqlParameter("@IdUsuario",IdUsuario)
                };
                var query = _context.Database.ExecuteSqlRaw("UsuarioDelete @IdUsuario", parametros);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo eliminar el usuario";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

    }
}
