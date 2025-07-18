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
            producto.SubCategoria.Categoria.Categorias = resultCategorias.Correct ? resultCategorias.Objects : new List<object>();

            if (IdProducto != null && IdProducto > 0) 
            {
                ML.Result resultProducto = _producto.GetById(IdProducto.Value);
                if (resultProducto.Correct) 
                {
                    ML.Producto productos = (ML.Producto)resultProducto.Object;

                    productos.SubCategoria.Categoria.Categorias = producto.SubCategoria.Categoria.Categorias;

                    ML.Result resultSubCategorias = _subCategoria.GetByIdCategoria(productos.SubCategoria.Categoria.IdCategoria);
                    productos.SubCategoria.SubCategorias = resultSubCategorias.Correct ? resultSubCategorias.Objects : new List<object>();

                    producto = productos;
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
