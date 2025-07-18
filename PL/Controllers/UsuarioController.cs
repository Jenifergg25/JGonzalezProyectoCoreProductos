using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        private readonly string _usuarioEndPoint;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Estado estado, BL.Municipio municipio, BL.Colonia colonia, IConfiguration configuration)
        {
            _usuario = usuario;
            _rol = rol;
            _estado = estado;
            _municipio = municipio;
            _colonia = colonia;
            _usuarioEndPoint = configuration["ApiSettings:UsuarioEndPoint"];
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
            //ML.Result result = _usuario.GetAll(usuario);

            
            //GetAll con SOAP
            //usuario = GetAllSOAP(usuario);
            //if (usuario == null)
            //{
            //    usuario = new ML.Usuario();
            //    usuario.Rol = new ML.Rol();
            //}


            //GetAll con REST
            ML.Result result = GetAllREST();

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
        public IActionResult GetAll(ML.Usuario usuario)
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

            //Busqueda abierta con REST
            //ML.Result result = new ML.Result();
            //result = BusquedaAbiertaREST(usuario);


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
                //result = _usuario.GetByIdEFSP(IdUsuario.Value);

                //GetById con REST
                result = GetByIdREST(IdUsuario.Value);


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
                //Metodo Update consumido con BL
                //result = _usuario.UpdateEFSP(usuario);

                //Metodo Update consumido con REST
                result = UpdateREST(usuario);
            }
            else
            {
                //Metodo Add consumido con BL
                result = AddREST(usuario);

                //Metodo Add consumido con REST
                //result = _usuario.AddEFSP(usuario);
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
        [NonAction]
        private ML.Result GetAllREST()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string endPoint = _usuarioEndPoint.ToString();
                    //string endPoint = "http://localhost:5253/api/Usuario/";
                    client.BaseAddress = new Uri(endPoint);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseTask = client.GetAsync("GetAll");
                    responseTask.Wait();

                    HttpResponseMessage response = responseTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result = readTask.Result;
                        if (result.Objects != null)
                        {
                            var listaUsuarios = new List<ML.Usuario>();
                            foreach (var itemUsuario in result.Objects)
                            {
                                var json = itemUsuario.ToString();

                                ML.Usuario usuario = JsonConvert.DeserializeObject<ML.Usuario>(json);
                                listaUsuarios.Add(usuario);
                            }
                            result.Objects = listaUsuarios.Cast<object>().ToList();
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener usuarios";
                    }
                    /*
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener usuarios";
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        [NonAction]
        private ML.Result BusquedaAbiertaREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    string endPoint = _usuarioEndPoint.ToString();
                    client.BaseAddress = new Uri(endPoint);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var postTask = client.PostAsJsonAsync("BusquedaAbierta", usuario);
                    postTask.Wait();

                    HttpResponseMessage response = postTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result = readTask.Result;

                        var listaUsuarios = new List<ML.Usuario>();
                        foreach (var itemUsuario in result.Objects)
                        {
                            var json = itemUsuario.ToString();

                            ML.Usuario usuarios = JsonConvert.DeserializeObject<ML.Usuario>(json);
                            listaUsuarios.Add(usuarios);
                        }
                        result.Objects = listaUsuarios.Cast<object>().ToList();
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = $"HTTP Error: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        [NonAction]
        private ML.Result GetByIdREST(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    string endPoint = _usuarioEndPoint.ToString();
                    client.BaseAddress = new Uri(endPoint);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseTask = client.GetAsync($"GetById/{idUsuario}");
                    responseTask.Wait();

                    HttpResponseMessage response = responseTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result = readTask.Result;
                        if (result.Object != null)
                        {
                            string json = result.Object.ToString();
                            result.Object = JsonConvert.DeserializeObject<ML.Usuario>(json);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = $"HTTP Error: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        [NonAction]
        private ML.Result UpdateREST(ML.Usuario usuario)
        {
            if (usuario.Imagen == null)
            {
                usuario.ImagenBase64 = "";
            }
            else
            {
                usuario.ImagenBase64 = Convert.ToBase64String(usuario.Imagen);
                //usuario.Imagen = new byte[0];
            }
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string endPoint = _usuarioEndPoint.ToString();
                    client.BaseAddress = new Uri(endPoint);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var putTask = client.PutAsJsonAsync($"Update/{usuario.IdUsuario}", usuario);
                    putTask.Wait();

                    HttpResponseMessage response = putTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result = readTask.Result;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = $"Error HTTP: {response.StatusCode}";
                    }
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
        [NonAction]
        private ML.Result AddREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if (usuario.Imagen == null)
            {
                usuario.ImagenBase64 = "";
            }
            else
            {
                usuario.ImagenBase64 = Convert.ToBase64String(usuario.Imagen);
                usuario.Imagen = new byte[0];
            }
            //usuario.ImagenBase64 = Convert.ToBase64String(usuario.Imagen);
            //usuario.Imagen = new byte[0];

            try
            {
                using (var client = new HttpClient())
                {
                    string endPoint = _usuarioEndPoint.ToString();
                    client.BaseAddress = new Uri(endPoint);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var postTask = client.PostAsJsonAsync("Add", usuario);
                    postTask.Wait();

                    var response = postTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result = readTask.Result;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = $"Error HTTP: {response.StatusCode}";
                    }
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
