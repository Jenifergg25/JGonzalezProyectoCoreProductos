using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
