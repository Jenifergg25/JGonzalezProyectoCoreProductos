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
            //Funcion GetAll con BL
            ML.Result result = _usuario.GetAll(usuario);

            /*
            //GetAll con SOAP
            usuario = GetAllSOAP(usuario);
            if (usuario == null)
            {
                usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
            }


            //GetAll con REST
            //result = GetAllREST();
            */

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;

                ML.Result resultRol = _rol.GetAllEFSP();
                if (resultRol.Correct)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                }
            }
            //ViewBag.Errores = "";
            //Session.Remove("Correctos");
            //Session.Remove("Errores");
            return View(usuario);
        }

        [HttpPost]
        public IActionResult GetAll(ML.Usuario usuario/*, string tipoArchivo, HttpPostedFileBase archivo, string guardarErrores, string cargarCorrectos*/)
        {
            usuario.Nombre = usuario.Nombre ?? "";
            usuario.ApellidoPaterno = usuario.ApellidoPaterno ?? "";
            usuario.ApellidoMaterno = usuario.ApellidoMaterno ?? "";
            usuario.Rol = usuario.Rol ?? new ML.Rol();

            usuario.Rol.IdRol = usuario.Rol.IdRol;


            usuario.Rol.IdRol = usuario.Rol.IdRol;
            ML.Result resultRol = _rol.GetAllEFSP();
            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }
            else
            {
                usuario.Rol.Roles = new List<object>();
            }

            //GetAll con capa BL
            ML.Result result = _usuario.GetAll(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>(); 
            }

            /*
            //GetAll con UsuarioReference de SOAP
            ML.Result result = new ML.Result();
            UsuarioReference.UsuarioServiceClient usuarioSOAP = new UsuarioReference.UsuarioServiceClient();
            var respuesta = usuarioSOAP.GetAll(usuario);
            if (respuesta.Correct)
            {
                result.Correct = respuesta.Correct;
                result.Objects = respuesta.Objects.ToList();
            }

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }

            //BusquedaAbierta con SOAP
            ML.Usuario usuarioResultado = GetAllSOAP(usuario);

            if (usuarioResultado != null)
            {
                usuario.Usuarios = usuarioResultado.Usuarios;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }

            //Busqueda abierta con REST
            ML.Result result = new ML.Result();
            result = BusquedaAbiertaREST(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            */
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
                        ML.Result resultMunicipio = _municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado ?? 0);
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
                        ML.Result resultColonia = _colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio ?? 0);
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
                        ML.Result resultMunicipio = _municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado ?? 0);
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
                    }

                    // COLONIAS
                    if (usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
                    {
                        ML.Result resultColonia = _colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio ?? 0);
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
        public IActionResult Form(ML.Usuario usuario, IFormFile? imgName)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.Clear();
                //TryValidateModel(usuario);

                ML.Result resultRol = _rol.GetAllEFSP();
                if (resultRol.Correct)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                }
                ML.Result resultEstado = _estado.GetAllEFSP();
                if (resultEstado.Correct)
                {
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                }
                // Carga municipios
                if (usuario.Direccion?.Colonia?.Municipio?.Estado != null && usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
                {
                    ML.Result resultMunicipio =_municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado ?? 0);
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
                    ML.Result resultColonia = _colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio ?? 0);
                    if (resultColonia.Correct)
                    {
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    }
                }
                else
                {
                    usuario.Direccion.Colonia.Colonias = new List<object>();
                }


                return View(usuario);
            }
            if (imgName != null && imgName.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imgName.CopyTo(memoryStream);
                    usuario.Imagen = memoryStream.ToArray();
                }
            }

            ML.Result result = new ML.Result();

            if (usuario.IdUsuario > 0)
            {
                result = _usuario.UpdateEFSP(usuario);
            }
            else
            {
                result = _usuario.AddEFSP(usuario);
            }

            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }

            if (usuario.Rol == null)
                usuario.Rol = new ML.Rol();

            if (usuario.Direccion == null)
                usuario.Direccion = new ML.Direccion();

            if (usuario.Direccion.Colonia == null)
                usuario.Direccion.Colonia = new ML.Colonia();

            if (usuario.Direccion.Colonia.Municipio == null)
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();

            if (usuario.Direccion.Colonia.Municipio.Estado == null)
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result resultRoles = _rol.GetAllEFSP();
            if (resultRoles.Correct)
            {
                usuario.Rol.Roles = resultRoles.Objects;
            }

            ML.Result resultEstados = _estado.GetAllEFSP();
            if (resultEstados.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
            }

            return View(usuario);
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
