using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        private readonly BL.Producto _producto;
        private readonly BL.Categoria _categoria;
        private readonly BL.SubCategoria _subCategoria;
        public ProductoController(BL.Producto producto, BL.Categoria categoria, BL.SubCategoria subCategoria)
        {
            _producto = producto;
            _categoria = categoria;
            _subCategoria = subCategoria;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();

            producto.SubCategoria.IdSubCategoria = 0;
            producto.SubCategoria.Categoria.IdCategoria = 0;

            ML.Result resultCategoria = _categoria.GetAll();
            if (resultCategoria.Correct)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
            }

            producto.Productos = new List<object>();

            return View(producto);
        }

        [HttpPost]
        public IActionResult GetAll(ML.Producto producto)
        {
            producto.SubCategoria ??= new ML.SubCategoria();
            producto.SubCategoria.Categoria ??= new ML.Categoria();

            ML.Result resultCategoria = _categoria.GetAll();
            if (resultCategoria.Correct)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
            }
            if (producto.SubCategoria.Categoria.IdCategoria != 0)
            {
                ML.Result resultSub = _subCategoria.GetByIdCategoria(producto.SubCategoria.Categoria.IdCategoria);
                if (resultSub.Correct)
                {
                    producto.SubCategoria.SubCategorias = resultSub.Objects;
                }
            }
            if (producto.SubCategoria.IdSubCategoria != 0 && producto.SubCategoria.Categoria.IdCategoria != 0)
            {
                ML.Result result = _producto.GetAll(producto);
                if (result.Correct)
                {
                    producto.Productos = result.Objects;
                }
                else
                {
                    producto.Productos = new List<object>();
                }
            }
            else
            {
                producto.Productos = new List<object>();
            }

            return View(producto);
        }
        [HttpGet]
        public IActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();

            ML.Result resultCategorias = _categoria.GetAll();
            if (resultCategorias.Correct)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
            }
            else
            {
                producto.SubCategoria.Categoria.Categorias = new List<object>();
            }

            if (IdProducto != null && IdProducto > 0)
            {
                ML.Result resultProducto = _producto.GetById(IdProducto.Value);
                if (resultProducto.Correct && resultProducto.Object != null)
                {
                    producto = (ML.Producto)resultProducto.Object;

                    if (producto.SubCategoria.Categoria.IdCategoria > 0)
                    {
                        ML.Result resultSubCategorias = _subCategoria.GetByIdCategoria(producto.SubCategoria.Categoria.IdCategoria);
                        if (resultSubCategorias.Correct)
                        {
                            producto.SubCategoria.SubCategorias = resultSubCategorias.Objects;
                        }
                        else
                        {
                            producto.SubCategoria.SubCategorias = new List<object>();
                        }
                    }
                }
            }
            else
            {
                producto.SubCategoria.SubCategorias = new List<object>();
            }

            return View(producto);
        }

        [HttpGet]
        public JsonResult GetSubcategorias(int idCategoria)
        {
            ML.Result result = _subCategoria.GetByIdCategoria(idCategoria);
            return Json(result.Objects);
        }

    }
}
