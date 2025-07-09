using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        } 
        private readonly BL.Usuario _usuario;

        public UsuarioController(BL.Usuario usuario)
        {
            _usuario = usuario;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form()
        {
            return View();
        }
    }
}
