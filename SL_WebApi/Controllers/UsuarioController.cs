using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BL.Usuario _usuario;

        public UsuarioController(BL.Usuario usuario)
        {
            _usuario = usuario;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;


            ML.Result result = new ML.Result();
            result = _usuario.GetAll(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost]
        [Route("BusquedaAbierta")]
        public IActionResult BusquedaAbierta([FromBody] ML.Usuario usuario)
        {
            ModelState.Clear();
            ML.Result result = new ML.Result();
            result = _usuario.GetAll(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        //[HttpGet]
        //[Route("BusquedaAbierta")]
        //public IActionResult BusquedaAbierta(string? Nombre, string? ApellidoPaterno, string? ApellidoMaterno, int? IdRol)
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Rol = new ML.Rol();
        //    usuario.Nombre = Nombre ?? "";
        //    usuario.ApellidoPaterno = ApellidoPaterno ?? "";
        //    usuario.ApellidoMaterno = ApellidoMaterno ?? "";
        //    usuario.Rol.IdRol = IdRol ?? 0;
        //    ML.Result result = new ML.Result();
        //    result = _usuario.GetAll(usuario);
        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }
        //}
        [HttpGet]
        [Route("GetById/{idUusario}")]
        public IActionResult GetById(int idUusario)
        {
            ML.Result result = new ML.Result();
            result = _usuario.GetByIdEFSP(idUusario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            //if (usuario.ImagenBase64 != "")
            //{
            //    usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            //    usuario.ImagenBase64 = "";
            //}
            //else
            //{
            //    usuario.Imagen = null;
            //}
            usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            usuario.ImagenBase64 = "";

            result = _usuario.AddEFSP(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut]
        [Route("Update/{idUsuario}")]
        public IActionResult Update(int idUsuario, [FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = idUsuario;
            if (usuario.ImagenBase64 != "")
            {
                usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            }
            else
            {
                usuario.Imagen = null;
            }
            ML.Result result = new ML.Result();
            result = _usuario.UpdateEFSP(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete]
        [Route("Delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            result = _usuario.DeleteEFSP(idUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
