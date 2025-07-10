using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        } 
        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Estado _estado;
        private readonly BL.Municipio _municipio;
        private readonly BL.Colonia _colonia;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Estado estado, BL.Municipio municipio, BL.Colonia colonia)
        {
            _usuario = usuario;
            _rol = rol;
            _estado = estado;
            _municipio = municipio;
            _colonia = colonia;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result result = _usuario.GetAll(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;

                ML.Result resultRol = _rol.GetAllEFSP();
                if (resultRol.Correct)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                }
            }
            return View(usuario);
        }
        [HttpGet]
        public IActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result resultRol = _rol.GetAllEFSP();
            ML.Result resultEstado = _estado.GetAllEFSP();
            usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
            usuario.Direccion.Colonia.Colonias = new List<object>();

            ML.Result result = new ML.Result();
            if (IdUsuario == null)
            {
                IdUsuario = 0;
            }
            if (IdUsuario > 0)
            {
                //GetById con metodo BL
                result = _usuario.GetByIdEFSP(IdUsuario.Value);

                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;

                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                    // Carga municipios
                    if (usuario.Direccion?.Colonia?.Municipio?.Estado != null && usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
                    {
                        ML.Result resultMunicipio = _municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        if (resultMunicipio.Correct)
                        {
                            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                        }
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
                    }
                    // Carga colonias
                    if (usuario.Direccion?.Colonia?.Municipio != null && usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
                    {
                        ML.Result resultColonia = _colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        if (resultColonia.Correct)
                        {
                            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                        }
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Colonias = new List<object>();
                    }

                }
            }

            else
            {
                if (resultRol.Correct)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                }

                if (resultEstado.Correct)
                {
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                    // MUNICIPIOS
                    if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
                    {
                        ML.Result resultMunicipio = _municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
                    }

                    // COLONIAS
                    if (usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
                    {
                        ML.Result resultColonia = _colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Colonias = new List<object>();
                    }
                }
            }
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Form()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            result = _usuario.DeleteEFSP(IdUsuario);
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public JsonResult GetMunicipioByEstado(int IdEstado)
        {
            ML.Result resultMunicipios = _municipio.GetByIdEstado(IdEstado);
            return Json(resultMunicipios);
        }
        [HttpGet]
        public JsonResult GetColoniasByMunicipio(int IdMunicipio)
        {
            ML.Result resultColonias = _colonia.GetByIdMunicipio(IdMunicipio);
            return Json(resultColonias);
        }

        //[HttpPost]
        //public JsonResult UpdateStatus(int IdUsuario, bool Status)
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.IdUsuario = IdUsuario;
        //    usuario.Status = Status;

        //    ML.Result resultStatus = BL.Usuario.UpdateStatus(usuario);
        //    return Json(new { success = resultStatus.Correct });
        //}
    }
}
